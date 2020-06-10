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
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        moved = chose();
        currentHealth = 100;
        healthBar.SetMaxHealth(maxHealth);
        particles = GetComponentInChildren<ParticleSystem>();
        particles.Stop();
        cannons = GameObject.Find("OsledShip(Clone)/PirateShip(Clone)/Lookout/Sphere").GetComponent<Lookoutosled>();

        textdeath = GameObject.Find("OsledShip(Clone)/PirateShip(Clone)/Canvas/death/deathcount").GetComponent<Text>();

        _distanitionG = GameObject.FindWithTag("Health").transform;
        _distanitionB = GameObject.FindWithTag("Ammo").transform;

        //  transform.rotation = Quaternion.LookRotation(moved);
    }
    void Update()
    {

        cannons = GameObject.Find("OsledShip(Clone)/PirateShip(Clone)/Lookout/Sphere").GetComponent<Lookoutosled>();
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        textdeath = GameObject.Find("OsledShip(Clone)/PirateShip(Clone)/Canvas/death/deathcount").GetComponent<Text>();
        textdeath.text = "" + death;

        if (currentHealth <= 20f && gotoammoo == false && _distanitionG != null)
        {
            gotohealth = true;
            _navMeshAgent.enabled = true;
            setdis();
        }
        if (_distanitionG == null || currentHealth >= 21f)
        {
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
        /*if (currentHealth <= 0)
        {
            //gameObject.tag = "Destroyed";
            movespeed = 0f;
            rotspeed = 0f;
            moveforce = 0f;
            RotationSpeed = 0f;
            particles.Play();


        }*/
        if (ischeck == false)
        {
            StartCoroutine(check());
            // StartCoroutine(__TurnLookoutRight(90));
        }
      


       rb.velocity = moved * moveforce;
        if (Physics.Raycast(transform.position, transform.forward, maxdis, hmm))
        {
            // moved = chose();
           // transform.rotation = Quaternion.LookRotation(moved);
            int rotLorR = Random.Range(0,1 );
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
        if (iswalk == false)
        {
            StartCoroutine(wandr());
        }
        if (isrotr == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * rotspeed);
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

    void setdis()
    {
        if (gotohealth == true)
        {

            _distanitionG = GameObject.FindWithTag("Health").transform;
            Vector3 targetve = _distanitionG.transform.position;
            _navMeshAgent.SetDestination(targetve);
            if (targetve == null)
            {
                _navMeshAgent.enabled = false;
            }

        }


    }
    void gotoammo()
    {
        if (gotoammoo == true)
        {
            _distanitionB = GameObject.FindWithTag("Ammo").transform;
            Vector3 targetve = _distanitionB.transform.position;
            _navMeshAgent.SetDestination(targetve);
            if (targetve == null)
            {
                _navMeshAgent.enabled = false;
            }
        }


    }

    Vector3 chose()
    {
        System.Random ran = new System.Random();
        int i = ran.Next(0, 1);
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
        int rotime = Random.Range(1, 3);
        int rotwait = Random.Range(1, 3);
        int rotLorR = Random.Range(1, 10);
        int walkwait = Random.Range(3, 10);
        int Walktime = Random.Range(1, 5);

        iswand = true;

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
    void TakeDamage(int damage)
    {
        currentHealth += damage;

        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            currentHealth = 100;
            death += 1;
        }
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

        if (other.gameObject.tag == "Health")
        {
            currentHealth += healthback;
            _navMeshAgent.enabled = false;
        }
        if (other.gameObject.tag == "Ammo")
        {
            cannons.cannons += 10;
            _navMeshAgent.enabled = false;
        }




    }



}
