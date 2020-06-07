using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookPata : MonoBehaviour
{
    public PataAI AIscript;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Boatbody"))
        {
            AIscript.enemy = true;
        }else if (col.gameObject.CompareTag("canon"))
        {
            AIscript.canon = true;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Boatbody"))
        {
            AIscript.enemy = false;
        }
        else if (col.gameObject.CompareTag("canon"))
        {
            AIscript.canon = false;
        }
    }
}
