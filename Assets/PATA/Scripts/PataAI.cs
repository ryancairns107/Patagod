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

    public GameObject CannonBallPrefab = null;
    public Transform CannonFrontSpawnPoint = null;
    public Transform CannonLeftSpawnPoint = null;
    public Transform CannonRightSpawnPoint = null;
    public float cannonBallAmount = 20f;
    public bool shotFired;
    public float reload = 5f;

    public float check;

    public Vector3 IdlePosition;
    public bool resetIdle;
    public float loopIdle = 3f;
    


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        manager = GameObject.Find("CompetitionManager").GetComponent<Mangerosled>();

        CannonBallPrefab = GameObject.Find("CannonBall");
        CannonFrontSpawnPoint = GameObject.Find("PataShip(Clone)/FrontSpawnPoint").transform;
        CannonLeftSpawnPoint = GameObject.Find("PataShip(Clone)/LeftSpawnPoint").transform;
        CannonRightSpawnPoint = GameObject.Find("PataShip(Clone)/RightSpawnPoint").transform;
    }


    void FixedUpdate()
    {
         approach[0] = GameObject.Find("OsledShip(Clone)").transform;
         approach[1] = GameObject.Find("JeroenShip(Clone)").transform;
        //approach[2] = GameObject.Find("RyanShip(Clone)").transform;

        // timer for reloading 
        if (shotFired == true && reload > 0f)
            {
                reload -= Time.fixedDeltaTime;
                
            }
            else if (reload < 0f)
            {
                shotFired = false;
                reload = 5f;
            }

            // timer for Idle position
            if (resetIdle == true && loopIdle > 0f)
            {
                loopIdle -= Time.fixedDeltaTime;
                
            }
            else if (loopIdle < 0f)
            {
                resetIdle = false;
                loopIdle = 3f;
            }


            StartCoroutine(__Idle());
            StartCoroutine(__Attack());
            StartCoroutine(__Defend());
            StartCoroutine(__RunAway());

    }

     public IEnumerator __Idle()
     {
        for (var i = 0; i < approach.Length; i++)
        {
            if (manager.Osledhealth <  manager.Jeroenhealth)
            {
                agent.SetDestination(approach[0].position);
                Debug.Log("Pata Following Osled");
                check = 0;
            }
            else if (manager.Jeroenhealth < manager.Osledhealth)
            {
                agent.SetDestination(approach[1].position);
                Debug.Log("Pata Following Jeroen");
                check = 1;
            }
           /* else if (manager.Jeroenhealth <  manager.Osledhealth)
            {
                agent.SetDestination(approach[2].position);
                Debug.Log("Pata Following Jeroen");
                check = 2;
            } */else if (resetIdle == false)
            {
                check = 3;

                    IdlePosition = new Vector3(Random.Range(-200.0f, 200.0f), 0, Random.Range(-200.0f, 200.0f));
                    agent.SetDestination(IdlePosition);
                    Debug.Log("Pata IDLE");

                resetIdle = true;
            }
        }

        yield return new WaitForFixedUpdate();
     }
    public IEnumerator __Attack()
    {
        if (enemy == true && cannonBallAmount != 0f && shotFired == false)
        {
            GameObject newInstance = Instantiate(CannonBallPrefab, CannonFrontSpawnPoint.position, CannonFrontSpawnPoint.rotation);
            CannonBallPrefab.name = "CannonBall";
            cannonBallAmount -= 1f;
            shotFired = true;

        }else if (enemy == true && cannonBallAmount == 0f)
        {

        }
        else
        {

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
