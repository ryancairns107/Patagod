using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookatcamera : MonoBehaviour
{
    public Transform cam;
    public GameObject cameraa;

    void Start()
    {
        cameraa = GameObject.Find("Main Camera");
        cam = cameraa.transform ;

    }
    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}

