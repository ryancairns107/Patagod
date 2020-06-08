using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PataAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform[] approach;
    public Mangerosled manager;
    public bool enemy;
    public bool canon;
    public bool rock;

    public GameObject CannonBallPrefab = null;
    public Transform CannonFrontSpawnPoint = null;
    public Transform CannonLeftSpawnPoint = null;
    public Transform CannonRightSpawnPoint = null;

    public LayerMask hmm;
    public float maxdis = 0f;

    public float check;

    public Transform TRY;

    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        manager = GameObject.Find("CompetitionManager").GetComponent<Mangerosled>();

    }


    void FixedUpdate()
    {

         approach[0] = GameObject.Find("OsledShip(Clone)").transform;
         approach[1] = GameObject.Find("RyanShip(Clone)").transform;
         approach[2] = GameObject.Find("JeroenShip(Clone)").transform;

         if (check == 0)
         {
             agent.SetDestination(approach[0].position);
             Debug.Log("FollowingOsled");
         }
         else if (check == 1)
         {
             agent.SetDestination(approach[1].position);
             Debug.Log("FollowingRyan");
         }
         else if (check == 2)
         {
             agent.SetDestination(approach[2].position);
             Debug.Log("FollowingJeroen");
         }


        StartCoroutine(__Idle());
        StartCoroutine(__Attack());
        StartCoroutine(__Defend());
        StartCoroutine(__RunAway());

        /*if (Physics.Raycast(transform.position, transform.forward, maxdis, hmm))
        {
            int rotLorR = Random.Range(1, 10);
            if (rotLorR == 1)
            {
                transform.Rotate(transform.up * 90);
            }
            if (rotLorR == 5)
            {
                transform.Rotate(transform.up * -90);
            }

        }*/

    }

     public IEnumerator __Idle()
     {
        for (var i = 0; i < approach.Length; i++)
        {

            if (manager.Osledhealth < manager.Ryanhealth && manager.Osledhealth < manager.Jeroenhealth)
            {
                check = 0;
            }
            else if (manager.Ryanhealth < manager.Osledhealth && manager.Ryanhealth < manager.Jeroenhealth)
            {
                check = 1;
            }
            else if (manager.Jeroenhealth < manager.Ryanhealth && manager.Jeroenhealth < manager.Osledhealth)
            {
                check = 2;
            } else
            {
                check = 3;
                agent.SetDestination(approach[i].position);
                Debug.Log("IDLE BITCH");
            }


        }

        yield return new WaitForFixedUpdate();
     }
    public IEnumerator __Attack()
    {
        if (enemy == true)
        {
            GameObject newInstance = Instantiate(CannonBallPrefab, CannonFrontSpawnPoint.position, CannonFrontSpawnPoint.rotation);
            CannonBallPrefab.name = "PataCannon";
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

}
