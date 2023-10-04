using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerController : MonoBehaviour
{
    private float delay = 3.3f;
    private float speed = 7.0f;
    private float turnSpeed = 80.0f;
    private bool isRunning;
    private bool isCollision;
    private bool isDead;

    private Rigidbody playerRb;
    private Collider playerCd;
    private Animator playerAnim;
    private Coroutine powerupCountDown;
    private Vector3 playerMovement;
    private Vector3 playerRotation;
    private Vector3 fb = new Vector3(0, 0, 1);
    private Vector3 lr = new Vector3(0, 1, 0);

    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround;
    public bool doubleSpeedUsed = false;
    public ParticleLauncher particleLauncher;
    public CameraController cameraController;
    //powerup
    public PowerUpType currentPowerUp = PowerUpType.None;
    public ParticleSystem dashParticle;
    public ParticleSystem healParticle;
    public AudioSource shootSound;
    public AudioSource dashSound;
    public AudioSource jumpSound;
    public AudioSource healSound;
    public AudioSource crushSound;

    //private float verticalInput;
    //private float horizontalInput;

    //public bool doubleJumpUsed = false;
    //public float doubleJumpForce;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerCd = GetComponent<Collider>();
        playerAnim = GetComponent<Animator>();
        if(particleLauncher == null )
        {

            particleLauncher = GetComponent<ParticleLauncher>();
        }
        Physics.gravity *= gravityModifier;
    }

    private void Update()
    {
        if(cameraController.transCamera)
        {
            playerAnim.SetInteger("WeaponType_int", 1);
            Shoot();
        }
        else
        {
            playerAnim.SetInteger("WeaponType_int", -1);
            playerAnim.SetBool("Shoot_b", false);
        }

        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (!isCollision)
        {
            Jump();
        }
        //if (verticalInput != 0 || horizontalInput != 0)
        //{
        if (!isCollision && !isDead)
        {
            Move();
        }
        //}
        //else
        //{
        //    playerAnim.Play("Idle");
        //}

        //powerup
        if (currentPowerUp == PowerUpType.Dash)
        {
            Dash();
        }

      
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            // Debug.Log("Win!!");
            GameManager.Instance.Win();
        }
        else if (collision.gameObject.CompareTag("Manager"))
        {
            isDead = true;
            GameManager.Instance.GameOver();
            transform.LookAt(collision.transform.position);    
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 3);
        }
        else if (collision.gameObject.CompareTag("Special"))
        {
            //Debug.Log("Unity Master!!");
            GameManager.Instance.Special();
        }

        else
        {
            if (collision.gameObject.CompareTag("Vehicle") && !isCollision)
            {
                crushSound.Play();
                isCollision = true;
                StartCoroutine(VehicleCollisionCountdownRoutine());
            }

        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            isOnGround = true;
        }

        if (other.CompareTag("PowerUp"))
        {
            currentPowerUp = other.gameObject.GetComponent<PowerUp>().powerUpType;
            //particle on
            Destroy(other.gameObject);

            if (currentPowerUp == PowerUpType.HP)
            {
                GameManager.Instance.player.Heal();
                healSound.Play();
                //Debug.Log("회복! HP : " + GameManager.instance.player.hp);
                //ui
                healParticle.Play();
            }
            else if(currentPowerUp == PowerUpType.Dash)
            {
                dashParticle.Play();
            }

            if(powerupCountDown != null)
            {
                StopCoroutine(powerupCountDown);
            }
            powerupCountDown = StartCoroutine(PowerupCountdownRoutine());

        }
        //중간 부분까지 가면 Manager의 속도 10
        if (other.CompareTag("Manager Collision"))
        {
            GameManager.Instance.manager.agent.speed = 12.0f;
            GameManager.Instance.manager.dashCollision = true;
        }

    }


    private IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(3);

        if (currentPowerUp == PowerUpType.Dash)
        {
            doubleSpeedUsed = false;
            speed = 7.0f;
            //playerAnim.SetFloat("Speed_Multiplier", 1.0f);
            playerAnim.SetFloat("Speed_Multiplier", 1.0f);
        }

        if (healParticle.isPlaying)
        {
            healParticle.Stop();

        }
        if (dashParticle.isPlaying)
        {
            dashParticle.Stop();

        }

        currentPowerUp = PowerUpType.None;
        //particle off
    }
    private IEnumerator VehicleCollisionCountdownRoutine()
    {
        isRunning = false;
        playerAnim.SetBool("IsRunning", isRunning);
        //playerAnim.SetBool("Death_b", true);
        playerAnim.SetInteger("DeathType_int", 2);

        GameManager.Instance.player.TakeDamage();
        //Debug.Log("HP : " + GameManager.instance.player.hp);

        //애니메이션 일어나는 시간
        yield return new WaitForSeconds(delay);
        // playerAnim.SetBool("Death_b", false);
        playerAnim.SetInteger("DeathType_int", -1);
        isCollision = false;
        isRunning = true;
    }

    public void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        float v = verticalInput * Time.deltaTime;
        float h = horizontalInput * Time.deltaTime;

        bool hasHorizontalInput = !Mathf.Approximately(horizontalInput, 0f);
        bool hasVerticalInput = !Mathf.Approximately(verticalInput, 0f);

        isRunning = hasHorizontalInput || hasVerticalInput;

        playerAnim.SetBool("IsRunning", isRunning);

        playerMovement = fb * v * speed;
        playerRotation = lr * h * turnSpeed;

        transform.Translate(playerMovement);
        transform.Rotate(playerRotation);
    }

    public void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            jumpSound.Play();
            //doubleJumpUsed = false;

        }
        else
        {
            playerAnim.SetBool("Jump_b", false);
        }
        //else if(Input.GetKeyDown(KeyCode.Space) && !isOnGround && !doubleJumpUsed)
        //{
        //    doubleJumpUsed = true;
        //    playerRb.AddForce(Vector3.up * doubleJumpForce, ForceMode.Impulse);
        //    playerAnim.Play("Running_Jump", 3, 0f);
        //}
    }

    public void Dash()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            doubleSpeedUsed = true;
            speed = 14.0f;
            //playerAnim.SetFloat("Speed_Multiplier", 2.0f);
            //transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * (speed*2));
            playerAnim.SetFloat("Speed_Multiplier", 2.0f);
            if (!dashSound.isPlaying)
            {
                dashSound.Play();

            }
        }
        else if (doubleSpeedUsed)
        {
            doubleSpeedUsed = false;
            speed = 7.0f;
            //playerAnim.SetFloat("Speed_Multiplier", 1.0f);
            playerAnim.SetFloat("Speed_Multiplier", 1.0f);
        }
    }

    public void Shoot()
    {
        playerAnim.SetBool("Shoot_b", true);
        if (Input.GetButtonDown("Fire1"))
        {
            particleLauncher.ShootParticle();
            shootSound.Play();
        }
    }

}
