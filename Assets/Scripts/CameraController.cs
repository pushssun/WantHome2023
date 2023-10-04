using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    //camera
    public bool transCamera;

    public Camera mainCamera;
    public Camera shootCamera;
    public GameObject shootUI;

    // Start is called before the first frame update
    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = GetComponent<Camera>();

        }
        if (shootCamera == null)
        {
            shootCamera = GetComponent<Camera>();

        }
        if (shootUI == null)
        {

            shootUI = GetComponent<GameObject>();
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            transCamera = !transCamera;
            mainCamera.gameObject.SetActive(!transCamera);
            shootCamera.gameObject.SetActive(transCamera);
            shootUI.gameObject.SetActive(transCamera);
        }
    }
}
