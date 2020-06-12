using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public Animator[] anim;
    public GameObject[] canon;



    // private bool checkR;

    void Start()
    {
        anim[0] = canon[0].GetComponent<Animator>();
        anim[1] = canon[1].GetComponent<Animator>();
        anim[2] = canon[2].GetComponent<Animator>();
        anim[3] = canon[3].GetComponent<Animator>();
        anim[4] = canon[4].GetComponent<Animator>();
        anim[5] = canon[5].GetComponent<Animator>();
        anim[6] = canon[6].GetComponent<Animator>();
        anim[7] = canon[7].GetComponent<Animator>();
        anim[8] = canon[8].GetComponent<Animator>();
        anim[9] = canon[9].GetComponent<Animator>();
        anim[10] = canon[10].GetComponent<Animator>();
        anim[11] = canon[11].GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        //cam = GameObject.Find("Bk cam");
        // sidecam = GameObject.Find("cam");
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
            
         
            
        }
        anim[0].SetBool("Shoot", true); 
        anim[1].SetBool("Shoot", true); 
        anim[2].SetBool("Shoot", true); 
        anim[3].SetBool("Shoot", true); 
        
        rounds += 1;
        yield return new WaitForFixedUpdate();
        cam.SetActive(true);
        yield return new WaitForSeconds(8);
        cam.SetActive(false);
        anim[0].SetBool("Shoot", false);
        anim[1].SetBool("Shoot", false);
        anim[2].SetBool("Shoot", false);
        anim[3].SetBool("Shoot", false);
    }

    public IEnumerator __FireLeft(float power)
    {
        GameObject newInstance = Instantiate(CannonBallPrefab, CannonLeftSpawnPoint.position, CannonLeftSpawnPoint.rotation);
        CannonBallPrefab.name = "OsledCannon";
        anim[4].SetBool("Shoot", true);
        anim[5].SetBool("Shoot", true);
        anim[6].SetBool("Shoot", true);
        anim[7].SetBool("Shoot", true);
        yield return new WaitForFixedUpdate();
        rounds += 1;
        sidecam.SetActive(true);
        yield return new WaitForSeconds(8);
        sidecam.SetActive(false);
        anim[4].SetBool("Shoot", false);
        anim[5].SetBool("Shoot", false);
        anim[6].SetBool("Shoot", false);
        anim[7].SetBool("Shoot", false);
    }

    public IEnumerator __FireRight(float power)
    {
        GameObject newInstance = Instantiate(CannonBallPrefab, CannonRightSpawnPoint.position, CannonRightSpawnPoint.rotation);
        CannonBallPrefab.name = "OsledCannon";
        anim[8].SetBool("Shoot", true);
        anim[9].SetBool("Shoot", true);
        anim[10].SetBool("Shoot", true);
        anim[11].SetBool("Shoot", true);
        yield return new WaitForFixedUpdate();
      
        rounds += 1;
        sidecam.SetActive(true);
        yield return new WaitForSeconds(8);
        sidecam.SetActive(false);
        anim[8].SetBool("Shoot", false);
        anim[9].SetBool("Shoot", false);
        anim[10].SetBool("Shoot", false);
        anim[11].SetBool("Shoot", false);
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
