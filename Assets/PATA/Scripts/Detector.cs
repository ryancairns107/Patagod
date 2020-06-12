using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    public PataAI AIscript;
    public float timer = 3f;
    public bool stopTime;

    void FixedUpdate()
    {
        AIscript = GameObject.Find("PataShip(Clone)").GetComponent<PataAI>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Boatbody") && col.name != "PataShip(Clone)" && timer > 0f)
        {
            AIscript.agent.SetDestination(new Vector3(0, 0, 0));
        }
    }

}
