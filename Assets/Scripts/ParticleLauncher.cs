using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLauncher : MonoBehaviour
{
    public ParticleSystem particleLauncher; //나가는 particle
    public ParticleSystem splatterParticles; //튕기는 particle
    public Gradient particleColorGradient; //particle의 색
    public ParticleDecalPool splateDecalPool;
    public AudioSource hitSound;

    List<ParticleCollisionEvent> collisionEvents; //배열

    public float rand;

    // Start is called before the first frame update
    void Start()
    {
        collisionEvents = new List<ParticleCollisionEvent>(); //배열 초기화
    }

    private void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(particleLauncher, other, collisionEvents); //모든 정보를 가져옴
        hitSound.transform.position = transform.position;
        hitSound.Play();
        for (int i=0;i<collisionEvents.Count; i++)
        {
            splateDecalPool.ParticleHit(collisionEvents[i], particleColorGradient);
            EmitAtLocation(collisionEvents[i]);

        }
    }

    void EmitAtLocation(ParticleCollisionEvent particleCollisionEvent) //부딪혔을 때 위치 좌표
    {
        splatterParticles.transform.position = particleCollisionEvent.intersection;
        splatterParticles.transform.rotation = Quaternion.LookRotation(particleCollisionEvent.normal);
        //Quaternion.LookRotation : 바라보는 방향으로(접선 방향)

        ParticleSystem.MainModule psMain = splatterParticles.main; //splatterParticle의 정보를 가져옴
        psMain.startColor = particleColorGradient.Evaluate(rand);

        splatterParticles.Emit(1); //뿌림
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetButton("Fire1"))
    //    {
    //        ParticleSystem.MainModule psMain = particleLauncher.main;
    //        psMain.startColor = particleColorGradient.Evaluate(Random.Range(0f, 1f));

    //        particleLauncher.Emit(1);
    //    }
    //}

    public void ShootParticle()
    {
        ParticleSystem.MainModule psMain = particleLauncher.main;
        rand = Random.Range(0f, 1f);
        psMain.startColor = particleColorGradient.Evaluate(rand);

        particleLauncher.Emit(1);
    }
}
