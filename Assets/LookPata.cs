using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookPata : MonoBehaviour
{
    public PataAI AIscript;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Boatbody"))
        {
           AIscript.agent.destination = new Vector3(50, 0, 0);
        }
    }
}
