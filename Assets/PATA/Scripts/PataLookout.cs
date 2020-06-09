using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PataLookout : MonoBehaviour
{
    public PataAI AIscript;

    void FixedUpdate()
    {
        AIscript = GameObject.Find("PataShip(Clone)").GetComponent<PataAI>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Boatbody"))
        {
            AIscript.enemy = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Boatbody"))
        {
            AIscript.enemy = false;
        }
    }
}
