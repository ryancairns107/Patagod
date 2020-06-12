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
    public bool defendLeft;
    public bool defendRight;
    public bool rotateAway;
    public bool runAway;
    public float check;

    public GameObject CannonBallPrefab = null;
    public Transform CannonFrontSpawnPoint = null;
    public Transform CannonLeftSpawnPoint = null;
    public Transform CannonRightSpawnPoint = null;

    public float cannonBallAmount = 20f;
    public bool shotFired;
    public float reload = 5f;

    public Vector3 IdlePosition;
    public bool resetIdle;
    public float loopIdle = 3f;

    public shiphealth health;
    public int currentHeath = 100;

    public float currentRotation;
    public Vector3 changePos;

    public GameObject moreAmmo = null;
    public GameObject moreHealth = null;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        manager = GameObject.Find("CompetitionManager").GetComponent<Mangerosled>();

    }


    void FixedUpdate()
    {
         agent.updateRotation = true;
         currentRotation = transform.rotation.y;

         approach[0] = GameObject.Find("OsledShip(Clone)").transform;
         approach[1] = GameObject.Find("JeroenShip(Clone)").transform;
         health = GameObject.Find("PataShip(Clone)/PirateShip(Clone)/Canvas/Health").GetComponent<shiphealth>();

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

    void HealthCounter(int num)
    {
        currentHeath += num;

        health.SetHealth(currentHeath);

        if (currentHeath <= 0)
        {
            currentHeath = 100;
            //death += 1;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Ammo"))
        {
            cannonBallAmount += 10f;
        }
        else if (col.CompareTag("Health"))
        {
            HealthCounter(20);
        }
        else if (col.CompareTag("canon") && col.name != "PataCannonBall(Clone)")
        {
            HealthCounter(-20);
        }
    }

    public IEnumerator __Idle()
     {
        for (var i = 0; i < approach.Length; i++)
        {
            if (manager.Osledhealth < manager.Jeroenhealth)
            {
                agent.SetDestination(approach[0].position);
                Debug.Log("Pata Following Osleds Ass");
                check = 0;

                if (rotateAway == true)
                {
                    agent.transform.Rotate(0, currentRotation +90, 0, Space.Self);
                    changePos = gameObject.transform.position + new Vector3(0, 0, 100);
                    agent.SetDestination(changePos);

                }
            }
            else if (manager.Jeroenhealth < manager.Osledhealth)
            {
                agent.SetDestination(approach[1].position);
                Debug.Log("Pata Following Slow Jeroen");
                check = 1;

                if (rotateAway == true)
                {
                    agent.transform.Rotate(0, currentRotation + 90, 0, Space.Self);
                    changePos = gameObject.transform.position + new Vector3(0, 0, 100);
                    agent.SetDestination(changePos);
                }
            }

            else if (resetIdle == false)
            {
                check = 3;

                agent.acceleration = 50f;
                agent.speed = 50f;
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
            CannonBallPrefab.name = "PataCannonBall";
            cannonBallAmount -= 1f;
            shotFired = true;

        }
        else if (defendLeft == true && cannonBallAmount != 0f && shotFired == false)
        {
            GameObject newInstance = Instantiate(CannonBallPrefab, CannonLeftSpawnPoint.position, CannonLeftSpawnPoint.rotation);
            CannonBallPrefab.name = "PataCannonBall";
            cannonBallAmount -= 1f;
            shotFired = true;

        }
        else if (defendRight == true && cannonBallAmount != 0f && shotFired == false)
        {
            GameObject newInstance = Instantiate(CannonBallPrefab, CannonRightSpawnPoint.position, CannonRightSpawnPoint.rotation);
            CannonBallPrefab.name = "PataCannonBall";
            cannonBallAmount -= 1f;
            shotFired = true;

        }

        yield return new WaitForFixedUpdate();
    }
    public IEnumerator __Defend()
    {
        if (defendLeft == true || defendRight == true || enemy == true && cannonBallAmount <= 5f)
        {
            moreAmmo = GameObject.FindWithTag("Health");
            agent.SetDestination(moreAmmo.transform.position);
            Debug.Log("PATA getting ammo");
            if (moreAmmo == null)
            {

            }
        }
        else if (defendLeft == true || defendRight == true || enemy == true && currentHeath <= 10f)
        {
            moreHealth = GameObject.FindWithTag("Ammo");
            agent.SetDestination(moreHealth.transform.position);
            Debug.Log("PATA getting health");
        }

        yield return new WaitForSeconds(3f);
    }
    public IEnumerator __RunAway()
    {
        if (runAway == true && currentHeath <= 20f)
        {
            agent.acceleration = 200f;
            agent.speed = 150f;
            agent.transform.Rotate(0, currentRotation + 90, 0, Space.Self);
            changePos = gameObject.transform.position + new Vector3(0, 0, 100);
            agent.SetDestination(changePos);
            runAway = false;
            Debug.Log("get away f**kers");

        }

        yield return new WaitForSeconds(0.01f);
    }

}
