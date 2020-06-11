using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PataLookout : MonoBehaviour
{
    public PataAI AIscript;
    public bool doItOnce;
   // public float timer = 5f;

    void FixedUpdate()
    {
        AIscript = GameObject.Find("PataShip(Clone)").GetComponent<PataAI>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Boatbody"))
        {
            AIscript.enemy = true;

        } else if (col.CompareTag("canon") && col.name != "PataCannonBall(Clone)" && doItOnce == false)
        {
            AIscript.runAway = true;
            doItOnce = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Boatbody"))
        {
            AIscript.enemy = false;
        } else if (col.CompareTag("canon") && col.name != "PataCannonBall(Clone)" && doItOnce == true)
        {
            AIscript.runAway = false;
            doItOnce = false;
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
