using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class OsledAI : MonoBehaviour
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
    private float RotationSpeed = 180f;
    private ParticleSystem particles;
    public int death;
    public int Jeroenkill;
    public int Patakill;
    public int kill;
    public Text textdeath;
    public Text textkill;
    public int healthback;
    [SerializeField]
    Transform _distanitionG;
    [SerializeField]
    Transform _distanitionB;
    NavMeshAgent _navMeshAgent;
    public Lookoutosled cannons;
    public bool gotoammoo;
    public bool gotohealth;
    public bool escapeandfire;
    public GameObject[] items;
    
   // on start the ship collects data for the ship rigidbody and  the health and ammo items and sets health to be 100
    void Start()
    {
        items[0] = GameObject.Find("bottleLarge (1)");
        items[1] = GameObject.Find("bottle (1)");
        items[2] = GameObject.Find("bottleLarge");
        items[3] = GameObject.Find("bottle");
        rb = GetComponent<Rigidbody>();
        moved = chose();
        currentHealth = 100;
        healthBar.SetMaxHealth(maxHealth);
        // get partical effect and set it to disabled
        particles = GetComponentInChildren<ParticleSystem>();
        particles.Stop();
        // get how many cannons the ship carries from LookOut script
        cannons = GameObject.Find("OsledShip(Clone)/PirateShip(Clone)/Lookout/Sphere").GetComponent<Lookoutosled>();
        // get the text canvas to input the death and kills gotten
        textdeath = GameObject.Find("OsledShip(Clone)/PirateShip(Clone)/Canvas/death/deathcount").GetComponent<Text>();
        // set a distanation of ammo and health 
        _distanitionG = GameObject.FindWithTag("Health").transform;
        _distanitionB = GameObject.FindWithTag("Ammo").transform;

        
    }
    void Update()
    {
        // change visuals depending on the health of the ship to turn on and off sails, and start partical effects
        if (currentHealth <= 80f&& currentHealth >= 60f)
        {
            sails[0].SetActive(false);
        }
        if (currentHealth <= 59f && currentHealth >= 40f)
        {
            sails[1].SetActive(false);
        }
        if (currentHealth <= 39f)
        {
            particles.Play();
            sails[2].SetActive(false);
        }

        // get the navmesh component to enable it and disable it if needed
        cannons = GameObject.Find("OsledShip(Clone)/PirateShip(Clone)/Lookout/Sphere").GetComponent<Lookoutosled>();
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        textdeath = GameObject.Find("OsledShip(Clone)/PirateShip(Clone)/Canvas/death/deathcount").GetComponent<Text>();
        textdeath.text = "" + death;
        // give actions to the AI when the health and ammo and in a certain value then enables nav mesh depending on the situation
        if (currentHealth <= 20f && gotoammoo == false && _distanitionG != null)
        {
            movespeed = 90;
            gotohealth = true;
            _navMeshAgent.enabled = true;
            setdis();
        }
        if (_distanitionG == null || currentHealth >= 21f)
        {
            movespeed = 60;
            _navMeshAgent.enabled = false;
            gotohealth = false;
        }
        if (_distanitionB == null || currentHealth >= 21f)
        {
            _navMeshAgent.enabled = false;
            gotoammoo = false;
        }
        if (cannons.cannons <= 10f && gotohealth == false)
        {
            _navMeshAgent.enabled = true;
            gotoammoo = true;
            gotoammo();
        }
        // start the IEnomunator if a specific bool is set to true or false
        if (  escapeandfire == true)
        {
          
            cannons.__FireFront(4);
          
        }
       
        if (ischeck == false)
        {
            StartCoroutine(check());
            
        }
      

        // every time the hit ray cast sees an object infront of it like wall or rocks, it will avoid it on a random value , where it choses to go left or right or 180 degrees
       rb.velocity = moved * moveforce;
        if (Physics.Raycast(transform.position, transform.forward, maxdis, hmm))
        {
         
            int rotLorR = Random.Range(0,2 );
            
            if (rotLorR == 0)
            {
                transform.Rotate(transform.up * 90);
            }
            if (rotLorR == 1)
            {
                transform.Rotate(transform.up * -90);
            } else
            {
                transform.Rotate(transform.up * 180);
            }


        }

        // states of the AI. The AI on defult starts with wandering then in the IEnoumenator, it has a range value to randomly select where to wants to go next and wither if it should rotate or not
        if (iswalk == false )
        {
            StartCoroutine(wandr());
        }
        if (isrotr == true )
        {
            transform.Rotate(transform.up * Time.deltaTime * rotspeed);
        }
        if (isrotl == true )
        {
            transform.Rotate(transform.up * Time.deltaTime * -rotspeed);
        }
        if (iswalk == true )
        {
            transform.position += transform.forward * movespeed * Time.deltaTime;
        }
        // insuring that the health doesnt go above 100
        if (currentHealth > 100)
        {
           
            currentHealth = 100;
          


        }
    }
    // the void funtion below checks if the health is low and what is the situation of the navmesh then moves to a health bottle if avilable other wise it will be wandering
    void setdis()
    {
        if (gotohealth == true && (items[1].activeSelf ==true || items[3].activeSelf == true))
        {

            _distanitionG = GameObject.FindWithTag("Health").transform;
           
            Vector3 targetve = _distanitionG.transform.position;
            if (targetve != null)
            {
                _navMeshAgent.SetDestination(targetve);
            }
            
            if (targetve == null)
            {
                gotohealth = true;
                _navMeshAgent.enabled = false;
            }

        }


    }
    // the void funtion below checks if the health is low and what is the situation of the navmesh then moves to a ammo chest if avilable other wise it will be wandering
    void gotoammo()
    {
        if (gotoammoo == true &&(items[0].activeSelf == true || items[2].activeSelf == true))
        {
            _distanitionB = GameObject.FindWithTag("Ammo").transform;
            Vector3 targetve = _distanitionB.transform.position;
            if (targetve != null)
            {
                _navMeshAgent.SetDestination(targetve);
            }
            if (targetve == null)
            {
                gotoammoo = false;
                _navMeshAgent.enabled = false;
            }
        }


    }
    // chose, is where the AI choses wither to rotate or not accourding to range randomizer
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
          
        }
        else if (i == 1)
        {
            if (isrotl == true)
            {
                Teep = -transform.right;
                transform.Rotate(transform.up * Time.deltaTime * -rotspeed);
            }


            
        }
   

        return Teep;




    }
    // check is a checker for wither the ship should rotate or not and wait for a specific amount of seconds based on range 
    IEnumerator check()
    {
        int lookingRorL = Random.Range(1, 3);
        int lookwait = Random.Range(1, 5);
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
               
                StartCoroutine(__TurnLookoutLeft(90));
                ischeckR = false;
            }
            yield return new WaitForSeconds(looktime);
           
        }
        iswand = false;
    }
    //wander is the defult state where the ship randomly selects where to go and how it should move
    IEnumerator wandr()
    {
        int rotime = Random.Range(1, 3);
        int rotwait = Random.Range(1, 3);
        int rotLorR = Random.Range(1, 10);
        int walkwait = Random.Range(3, 10);
        int Walktime = Random.Range(1, 5);

        iswand = true;
        // the shit waits for few seconds then disable walk to allow rotation
        yield return new WaitForSeconds(walkwait);
        iswalk = true;
        yield return new WaitForSeconds(Walktime);
        iswalk = false;
        yield return new WaitForSeconds(rotwait);
        if (rotLorR == 1)
        {
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
        yield return new WaitForFixedUpdate();
    }

    // based on Ilias AI script turnlooks are used. However, the are called in update in lookoutscript 
    public IEnumerator __TurnLookoutLeft(float angle)
    {

        int numFrames = (int)(angle / (RotationSpeed * Time.fixedDeltaTime));
        for (int f = 0; f < numFrames; f++)
        {
            Lookout.transform.Rotate(0f, -RotationSpeed * Time.fixedDeltaTime, 0f);

            yield return new WaitForFixedUpdate();

        }

    }

    public IEnumerator __TurnLookoutRight(float angle)
    {

        int numFrames = (int)(angle / (RotationSpeed * Time.fixedDeltaTime));
        for (int f = 0; f < numFrames; f++)
        {
            Lookout.transform.Rotate(0f, RotationSpeed * Time.fixedDeltaTime, 0f);

            yield return new WaitForFixedUpdate();

        }

    }
    //take damage function, is activated on trigger enter and sets how much damage a ship can take
    void TakeDamage(int damage)
    {
        currentHealth += damage;

        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            GameObject.Find("OsledShip(Clone)/PirateShip(Clone)/Lookout/Sphere").GetComponent<Lookoutosled>().cannons = 20;
            currentHealth = 100;
            death += 1;
            sails[0].SetActive(true);
            sails[1].SetActive(true);
            sails[2].SetActive(true);
            particles.Stop();
          

        }
    }
    // triggers for the ship that would case it to take damage, heal or refill ammo

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "canon")
        {
            TakeDamage(D);
        }
        if (other.gameObject.tag == "Rock")
        {
          //  TakeDamage(D);
        }
        if (other.gameObject.name == "JeroenShip(Clone)")
        {

           TakeDamage(D);
            Debug.Log("braceee");
            escapeandfire = true;
        }
        if (other.gameObject.name == "PataShip(Clone)")
        {

            TakeDamage(D);
            Debug.Log("braceee");
            escapeandfire = true;
        }

        if (other.gameObject.tag == "Health" && currentHealth<=99)
        {
            currentHealth += healthback;
            healthBar.health += healthback;
            _navMeshAgent.enabled = false;
        }
        if (other.gameObject.tag == "Ammo")
        {
            cannons.cannons += 10;
            _navMeshAgent.enabled = false;
        }
        if((other.gameObject.name == "PataCannonBall(Clone)" || other.gameObject.name == "PataShip(Clone)") && currentHealth <= 20)
        {
            Patakill += 1;
        }
        if ((other.gameObject.tag == "JereonCannonBall(Clone)" || other.gameObject.name == "JeroenShip(Clone)") && currentHealth <= 20)
        {
            Jeroenkill += 1;
        }




    }



}
