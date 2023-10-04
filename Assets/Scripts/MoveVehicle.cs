using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveVehicle : MonoBehaviour
{
    private Vector3 originPos;
    private Quaternion originRot;
    private GameObject indicatorClone;

    public int speed = 5;
    public GameObject indicator;

    // Start is called before the first frame update
    void Start()
    {
        originPos = gameObject.transform.position;
        originRot = gameObject.transform.rotation;

        indicatorClone = Instantiate(indicator,originPos, Quaternion.identity);
        //indicator.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(this.gameObject != null)
        {
            if(collision.gameObject.CompareTag("Vehicle"))
            {
                gameObject.SetActive(false);
                indicatorClone.SetActive(true);
                Invoke("OnInvoke", 3.0f);
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Vehicle Collision"))
        {
            gameObject.SetActive(false);
            indicatorClone.SetActive(true);
            Invoke("OnInvoke", 3.0f);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        //Destroy(other);//파티클 삭제
        Destroy(this.gameObject); //파티클에 맞으면 자동차 삭제
    }

    void OnInvoke()
    {
        gameObject.transform.position = originPos;
        gameObject.transform.rotation = originRot;

        gameObject.SetActive(true);
        indicatorClone.SetActive(false);
    }
}
