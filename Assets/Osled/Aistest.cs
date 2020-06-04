
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aistest : MonoBehaviour
{
    public int D;
    public int maxHealth = 100;
    public int currentHealth;
  

    public shiphealth healthBar;

    public GameObject[] sails = null;
    public float movespeed = 3f;
    public float rotspeed = 100f;

    private bool iswand = false;
    private bool isrotl = false;
    private bool isrotr = false;
    private bool iswalk = false;
    private bool ischeck = false;
    public bool ischeckR = false;
    public bool ischeckL = false;
    public LayerMask hmm;
    public float maxdis = 0f;
    public Vector3 moved;
    private Rigidbody rb;
    public float moveforce = 0f;
    public GameObject Lookout = null;
    private float RotationSpeed = 180.0f;
    private ParticleSystem particles ;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moved = chose();
        currentHealth = 100;
        healthBar.SetMaxHealth(maxHealth);
        particles = GetComponentInChildren<ParticleSystem>();
        particles.Stop();


        //  transform.rotation = Quaternion.LookRotation(moved);
    }

    // Update is called once per frame

   
    void FixedUpdate()
    {

       if (currentHealth <= 0)
        {
            gameObject.tag = "Destroyed";
            movespeed = 0f;
            rotspeed = 0f;
            moveforce = 0f;
            RotationSpeed = 0f;
            particles.Play();

        }
        if (ischeck == false)
        {
            StartCoroutine(check());
           // StartCoroutine(__TurnLookoutRight(90));
        }
    


        rb.velocity = moved * moveforce;
        if (Physics.Raycast(transform.position, transform.forward, maxdis, hmm))
        {
            moved = chose();
            transform.rotation = Quaternion.LookRotation(moved);
        }
            if (iswalk == false)
        {
            StartCoroutine(wandr());
        }
        if (isrotr == true)
        {
            transform.Rotate(transform.up * Time.deltaTime *rotspeed);
        }
        if (isrotl == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * -rotspeed);
        }
        if (iswalk == true)
        {
            transform.position += transform.forward * movespeed * Time.deltaTime;
        }
    }
    Vector3 chose()
    {
        System.Random ran = new System.Random();
        int i = ran.Next(0, 2);
        Vector3 Teep = new Vector3();
        if (i == 0)
        {
           
            if (isrotr == true)
            {
                Teep = transform.right;
                transform.Rotate(transform.up * Time.deltaTime * rotspeed);
            }
            // Teep = transform.forward;
            //  Teep = transform.right;
            // transform.Rotate(transform.up * Time.deltaTime * rotspeed);
        }
        else if (i == 1)
        {
            if (isrotl == true)
            {
                Teep = -transform.right;
                transform.Rotate(transform.up * Time.deltaTime * -rotspeed);
            }
           
           
            // Teep = -transform.forward;
        }
        else if (i == 2)
        {
           // Teep = transform.right;
           // transform.Rotate(transform.up * Time.deltaTime * rotspeed);
        }
        
        return Teep;




    }
   
    IEnumerator check()
    {
        int lookingRorL = Random.Range(1, 3);
        int lookwait = Random.Range(3, 10);
        int looktime = Random.Range(1, 5);

        ischeck = true;

        yield return new WaitForSeconds(lookwait);
        ischeck = true;
        yield return new WaitForSeconds(looktime);
        ischeck = false;
        
        if (lookingRorL == 1)
        {
          
            ischeckR = true;
            if (ischeckR == true)
            {
                // StartCoroutine(check());
                StartCoroutine(__TurnLookoutRight(90));
                ischeckL = false;
            }

            yield return new WaitForSeconds(looktime);
            
        }
        if (lookingRorL == 2)
        {
            ischeckL = true;
            if (ischeckL == true)
            {
                // StartCoroutine(check());
                StartCoroutine(__TurnLookoutLeft(90));
                ischeckR = false;
            }
            yield return new WaitForSeconds(looktime);
           // ischeckL = false;
        }
        iswand = false;
    }
    IEnumerator wandr()
    {
        int rotime = Random.Range(1, 5);
        int rotwait = Random.Range(3, 10);
        int rotLorR = Random.Range(1, 3);
        int walkwait = Random.Range(3,10);
        int Walktime = Random.Range(1, 5);

        iswand = true;

        yield return new WaitForSeconds(walkwait);
        iswalk = true;
        yield return new WaitForSeconds(Walktime);
        iswalk = false;
        yield return new WaitForSeconds(rotwait);
        if (rotLorR== 1){
            isrotr = true;
            yield return new WaitForSeconds(rotime);
            isrotr = false;
        }
        if (rotLorR == 2)
        {
            isrotl = true;
            yield return new WaitForSeconds(rotime);
            isrotl = false;
        }
        iswand = false;
    }
    public IEnumerator __TurnLookoutLeft(float angle)
    {
        
        int numFrames = (int)(angle / (RotationSpeed * Time.fixedDeltaTime));
        for (int f = 0; f < numFrames; f++)
        {
            Lookout.transform.Rotate(0f, -RotationSpeed * Time.fixedDeltaTime, 0f);

            yield return new WaitForFixedUpdate();
           
        }
      
    }

    public IEnumerator __TurnLookoutRight( float angle)
    {
        
        int numFrames = (int)(angle / (RotationSpeed * Time.fixedDeltaTime));
        for (int f = 0; f < numFrames; f++)
        {
            Lookout.transform.Rotate(0f, RotationSpeed * Time.fixedDeltaTime, 0f);

            yield return new WaitForFixedUpdate();
            
        }
     
    }
    void TakeDamage(int damage)
    {
        currentHealth += damage;

        healthBar.SetHealth(currentHealth);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "canon")
        {
            TakeDamage(D);
        }
        if (other.gameObject.tag == "Boatbody")
        {

            TakeDamage(D);
            Debug.Log("braceee");
        }
    }
  

}
