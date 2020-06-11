using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JeroenLookout : MonoBehaviour
{
    public JeroenAI JeroenAI;
    public GameObject JeroenShip;

    public void OnTriggerStay(Collider other)
    {
        if (other.transform.gameObject.tag == "Boatbody" && other.transform.gameObject != JeroenShip)
        { 
        if (JeroenAI.Targets.Contains(other.transform.gameObject) == false)
        {
            JeroenAI.Targets.Add(other.transform.gameObject);
        }
    }
    }
    public void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < JeroenAI.Targets.Count; i++)
        {
            if (other.transform.gameObject == JeroenAI.Targets[i])
            {
                JeroenAI.Targets.RemoveAt(i);
            }
        }
    }
}
