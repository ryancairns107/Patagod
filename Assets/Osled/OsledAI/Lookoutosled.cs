using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Lookoutosled : MonoBehaviour
{
    public bool shoot = false;
    public float ischeck ;
    public GameObject lookout;
    //public bool ischeckLs = false;
    public GameObject CannonBallPrefab = null;
    public Transform CannonBackSpawnPoint = null;
    public Transform CannonLeftSpawnPoint = null;
    public Transform CannonRightSpawnPoint = null;
    public float rounds;
    public float maxrounds =1f;
    public float cannons;
    public GameObject cam;
    public GameObject sidecam;
 


    // private bool checkR;

    void Start()
    {
    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        cam = GameObject.Find("Bk cam");
        sidecam = GameObject.Find("cam");
        if (cannons == 50f)
        {
            
        }
        ischeck = lookout.GetComponent<Transform>().localRotation.eulerAngles.y;

        if (rounds >= maxrounds)
        {
            shoot = false;
            rounds = 0;
        }
        if (shoot == true && ischeck ==180 )
        {
            // __FireFront(1);
            cannons -= 1;
            StartCoroutine(__FireFront(0));
            
        }
        if (shoot == true && ischeck == 90)
        {
           // __FireRight(1);
            StartCoroutine(__FireRight(0));
            cannons -= 1;
        }
        if (shoot == true && ischeck == 270)
        {
           // __FireLeft(1);
            StartCoroutine(__FireLeft(0));
            cannons -= 1;
        }
        if (shoot == false)
        {
            StartCoroutine(__DoNothing());
        }
    }
  
    public IEnumerator __DoNothing()
    {
        yield return new WaitForFixedUpdate();
    }

    public IEnumerator __FireFront(float power)
    {
        GameObject newInstance = Instantiate(CannonBallPrefab, CannonBackSpawnPoint.position, CannonBackSpawnPoint.rotation);
        if(CannonBackSpawnPoint.position == CannonBackSpawnPoint.position)
        {
            
            CannonBallPrefab.name = "OsledCannon";
            cam.SetActive(true);
        }
        
        rounds += 1;
        yield return new WaitForFixedUpdate();
        cam.SetActive(false);
    }

    public IEnumerator __FireLeft(float power)
    {
        GameObject newInstance = Instantiate(CannonBallPrefab, CannonLeftSpawnPoint.position, CannonLeftSpawnPoint.rotation);
        CannonBallPrefab.name = "OsledCannon";
        sidecam.SetActive(true);
        yield return new WaitForFixedUpdate();
        rounds += 1;
        sidecam.SetActive(false);
    }

    public IEnumerator __FireRight(float power)
    {
        GameObject newInstance = Instantiate(CannonBallPrefab, CannonRightSpawnPoint.position, CannonRightSpawnPoint.rotation);
        CannonBallPrefab.name = "OsledCannon";
        sidecam.SetActive(true);
        yield return new WaitForFixedUpdate();
        sidecam.SetActive(false);
        rounds += 1;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Boatbody")
        {
            if(cannons>=1)
            {
               
                shoot = true;

                Debug.Log("I see U RRRRR");
            }
           
        }
    
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Boatbody")
        {
            shoot = false;
            Debug.Log("bye RRRRR");
        }
    }
}
