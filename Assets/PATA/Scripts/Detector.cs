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

        if (stopTime == true && timer > 0f)
        {
            stopTime = false;
            timer -= Time.fixedDeltaTime;

        }
        else if (timer < 0f)
        {
            timer = 3f;
        }

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Boatbody") && col.name != "PataShip(Clone)" && timer >0f)
        {
            AIscript.rotateAway = true;
            stopTime = true;

        }
    }

    /*void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Boatbody") && col.name != "PataShip(Clone)")
        {
            AIscript.rotateAway = false;
        }
    }*/

}
