using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    public PataAI AIscript;
    //public float timer = 3f;
    public bool stopTime;

    void FixedUpdate()
    {
        AIscript = GameObject.Find("PataShip(Clone)").GetComponent<PataAI>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Boatbody") && col.name != "PataShip(Clone)" && AIscript.rotateAway == false)
        {
            AIscript.transform.Rotate(0, AIscript.currentRotation + 90, 0, Space.Self);
            AIscript.rotateAway = true;
            //AIscript.fixedRotate = true;

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
