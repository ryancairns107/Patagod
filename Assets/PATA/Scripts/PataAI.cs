using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PataAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform[] approach;
    public bool enemy;
    public bool canon;
    public bool rock;

    public GameObject CannonBallPrefab = null;
    public Transform CannonFrontSpawnPoint = null;
    public Transform CannonLeftSpawnPoint = null;
    public Transform CannonRightSpawnPoint = null;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();


    }


    void FixedUpdate()
    {
        StartCoroutine(__Idle());
        StartCoroutine(__Attack());
        StartCoroutine(__Defend());
        StartCoroutine(__RunAway());
    }

    public IEnumerator __Idle()
    {
        for (var i = 0; i <= approach.Length; i++)
        {
            agent.destination = approach[i].position;
        }
        yield return new WaitForFixedUpdate();
    }
    public IEnumerator __Attack()
    {
        if (enemy == true)
        {
            GameObject newInstance = Instantiate(CannonBallPrefab, CannonFrontSpawnPoint.position, CannonFrontSpawnPoint.rotation);
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForFixedUpdate();
    }
    public IEnumerator __Defend()
    {
        yield return new WaitForFixedUpdate();
    }
    public IEnumerator __RunAway()
    {
        yield return new WaitForFixedUpdate();
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
