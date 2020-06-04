using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PataAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public int x;
    public int z;
    public bool ifMoving;

    void Start()
    {
       agent = GetComponent<NavMeshAgent>();
       agent.destination = new Vector3(0, 0, 0);
    }

    
 /*   void FixedUpdate()
    {
        if (ifMoving == true)
        {
            StartCoroutine(__Movement());
            agent.destination = new Vector3(x, 0, z);
        }
    }

    public IEnumerator __Movement()
    {

        for (x = 0; x <= 500; x++)
        {

           yield return x;
        }

        for (z = 0; z <= 500; z++)
        {
            yield return z;
        }

      

        yield return new WaitForFixedUpdate();

        ifMoving = false;

    }*/
}
