using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PataLookout : MonoBehaviour
{
    public PataAI AIscript;
    //public bool doItOnce;
    //public float timer = 1f;

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
        } /*else if (col.CompareTag("canon") && col.name != "PataCannonBall(Clone)" && doItOnce == true)
        {
            AIscript.runAway = false;
            doItOnce = false;
        }*/
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("canon") && col.name != "PataCannonBall(Clone)" /*&& doItOnce == false*/)
        {
            AIscript.runAway = true;
            //timer -= Time.fixedDeltaTime;
        }
        else
        {
            AIscript.runAway = false;
        }
    }
}

      /*  } else if (col.CompareTag("canon") && timer > 0f)
        {
            AIscript.runAway = true;
            timer -= Time.fixedDeltaTime;
        } else if (timer< 0f)
        {
            AIscript.runAway = false;
        }*/
