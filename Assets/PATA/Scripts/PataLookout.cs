using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PataLookout : MonoBehaviour
{
    public PataAI AIscript;
    public GameObject myShip;
    public GameObject self;
    public bool doItOnce;


    void FixedUpdate()
    {
        AIscript = GameObject.Find("PataShip(Clone)").GetComponent<PataAI>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Boatbody") && col.name != "PataShip(Clone)" && self.name == "Sphere front")
        {
            AIscript.enemy = true;
            
        }else if (col.CompareTag("Boatbody") && col.name != "PataShip(Clone)" && self.name == "Sphere left")
        {
            AIscript.defendLeft = true;
        }
        else if (col.CompareTag("Boatbody") && col.name != "PataShip(Clone)" && self.name == "Sphere right")
        {
            AIscript.defendRight = true;
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
        if (col.CompareTag("Boatbody") && col.name != "PataShip(Clone)" && self.name == "Sphere front")
        {
            AIscript.enemy = false;
        }
        else if (col.CompareTag("Boatbody") && col.name != "PataShip(Clone)" && self.name == "Sphere left")
        {
            AIscript.defendLeft= false;
        }
        else if (col.CompareTag("Boatbody") && col.name != "PataShip(Clone)" && self.name == "Sphere right")
        {
            AIscript.defendRight = false;
        }
        else if (col.CompareTag("canon") && col.name != "PataCannonBall(Clone)" && doItOnce == true)
        {
            AIscript.resetIdle = false;
            doItOnce = false;
        }
    }

}

