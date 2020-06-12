using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PataLookout : MonoBehaviour
{
    public PataAI AIscript;
    public bool doItOnce;

    void FixedUpdate()
    {
        AIscript = GameObject.Find("PataShip(Clone)").GetComponent<PataAI>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Boatbody") && col.name != "PataShip(Clone)")
        {
            AIscript.enemy = true;

        }
        else if (col.CompareTag("canon") && col.name != "PataCannonBall(Clone)" && doItOnce == false)
        {
            AIscript.runAway = true;
            doItOnce = true;
            AIscript.resetIdle = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Boatbody") && col.name != "PataShip(Clone)")
        {
            AIscript.enemy = false;
        }
        else if (col.CompareTag("canon") && col.name != "PataCannonBall(Clone)" && doItOnce == true)
        {
            AIscript.resetIdle = false;
            doItOnce = false;
        }
    }

   /* private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("canon") && col.name != "PataCannonBall(Clone)")
        {
            AIscript.runAway = true;
            //timer -= Time.fixedDeltaTime;
        }
        else
        {
            AIscript.runAway = false;
        }
    }*/
}

      /*  } else if (col.CompareTag("canon") && timer > 0f)
        {
            AIscript.runAway = true;
            timer -= Time.fixedDeltaTime;
        } else if (timer< 0f)
        {
            AIscript.runAway = false;
        }*/
