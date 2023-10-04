using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLauncher : MonoBehaviour
{
    public ParticleSystem particleLauncher; //������ particle
    public ParticleSystem splatterParticles; //ƨ��� particle
    public Gradient particleColorGradient; //particle�� ��
    public ParticleDecalPool splateDecalPool;
    public AudioSource hitSound;

    List<ParticleCollisionEvent> collisionEvents; //�迭

    public float rand;

    // Start is called before the first frame update
    void Start()
    {
        collisionEvents = new List<ParticleCollisionEvent>(); //�迭 �ʱ�ȭ
    }

    private void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(particleLauncher, other, collisionEvents); //��� ������ ������
        hitSound.transform.position = transform.position;
        hitSound.Play();
        for (int i=0;i<collisionEvents.Count; i++)
        {
            splateDecalPool.ParticleHit(collisionEvents[i], particleColorGradient);
            EmitAtLocation(collisionEvents[i]);

        }
    }

    void EmitAtLocation(ParticleCollisionEvent particleCollisionEvent) //�ε����� �� ��ġ ��ǥ
    {
        splatterParticles.transform.position = particleCollisionEvent.intersection;
        splatterParticles.transform.rotation = Quaternion.LookRotation(particleCollisionEvent.normal);
        //Quaternion.LookRotation : �ٶ󺸴� ��������(���� ����)

        ParticleSystem.MainModule psMain = splatterParticles.main; //splatterParticle�� ������ ������
        psMain.startColor = particleColorGradient.Evaluate(rand);

        splatterParticles.Emit(1); //�Ѹ�
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
