using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EteverseManager : MonoBehaviour
{
    public Transform target;
    public bool dashCollision;
    public AudioSource footstepSound;
    public NavMeshAgent agent;

    private float maxSpeed;
    private Vector3 destination;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        destination = agent.destination;

        animator = GetComponent<Animator>();
        maxSpeed = agent.speed;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(destination, target.position) > 1.0f)
        {
            destination = target.position;
            agent.destination = destination;
        }

        if(GameManager.Instance.gameResult != gameResult.None)
        {
            agent.speed = 0;
            animator.SetBool("IsCaught", true);
            footstepSound.Stop();

        }
        
    }

    private void OnParticleCollision(GameObject other)
    {
        agent.speed--;
        if(agent.speed < 2)
        {
            agent.speed = 2;
        }
        StartCoroutine(ParticleCollisionRoutine());
    }

    private IEnumerator ParticleCollisionRoutine()
    {
        while(agent.speed < maxSpeed-1)
        {
            yield return new WaitForSeconds(1);
            agent.speed++;
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    //중간 부분까지 가면 Manager의 속도 10
    //    if(other.CompareTag("Manager Collision"))
    //    {
    //        agent.speed = 12.0f;
    //        dashCollision = true;
    //    }
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        agent.speed = 0f;
    //        transform.LookAt(collision.transform.position);
    //        animator.SetBool("IsCaught", true);
    //    }
    //}
}
