using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jeroenAI : MonoBehaviour
{
    public GameObject thisShip;
    public GameObject TargetShip;
    public float sphereSize = 100;

    void Update()
    {
        RaycastHit hit;
        if (Physics.SphereCast(thisShip.transform.position, sphereSize, transform.forward, out hit, 1))
        {
            if(hit.transform.gameObject.tag == "Boatbody")
            {
                TargetShip = hit.transform.gameObject;
            }
            else
            {
                TargetShip = null;
            }
        }
        if (TargetShip != null)
        {

        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(thisShip.transform.position, sphereSize);
    }
}
