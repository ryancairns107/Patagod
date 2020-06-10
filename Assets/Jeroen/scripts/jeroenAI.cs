using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JeroenAI : MonoBehaviour
{
    public GameObject thisShip;
    public GameObject TargetShip;
    public float sphereSize;
    Quaternion desiredRotation;
    public float angle;

    void Update()
    {
        Debug.Log(desiredRotation);
        RaycastHit hit;
        if (Physics.SphereCast(thisShip.transform.position, 500, transform.forward, out hit, 1))
        {
            if (hit.transform.gameObject.tag == "Boatbody" && hit.transform.gameObject != this.gameObject)
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
            Vector3 posRelative = TargetShip.transform.position - transform.position;
            desiredRotation = Quaternion.LookRotation(posRelative);
            desiredRotation *= Quaternion.Euler(0, -90, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, 0.5f * Time.deltaTime);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 500);
    }
}
