﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // private bool checkR;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ischeck = lookout.GetComponent<Transform>().localRotation.eulerAngles.y;

        if (rounds >= 3)
        {
            shoot = false;
            rounds = 0;
        }
        if (shoot == true && ischeck ==180 )
        {
          // __FireFront(1);
           StartCoroutine(__FireFront(0));
            
        }
        if (shoot == true && ischeck == 90)
        {
           // __FireRight(1);
            StartCoroutine(__FireRight(0));
        }
        if (shoot == true && ischeck == 270)
        {
           // __FireLeft(1);
            StartCoroutine(__FireLeft(0));
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
        yield return new WaitForFixedUpdate();
        rounds += 1;
    }

    public IEnumerator __FireLeft(float power)
    {
        GameObject newInstance = Instantiate(CannonBallPrefab, CannonLeftSpawnPoint.position, CannonLeftSpawnPoint.rotation);
        yield return new WaitForFixedUpdate();
        rounds += 1;
    }

    public IEnumerator __FireRight(float power)
    {
        GameObject newInstance = Instantiate(CannonBallPrefab, CannonRightSpawnPoint.position, CannonRightSpawnPoint.rotation);
        yield return new WaitForFixedUpdate();
        rounds += 1;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Boatbody")
        {
            
            shoot = true;
            Debug.Log("I see U RRRRR");
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
