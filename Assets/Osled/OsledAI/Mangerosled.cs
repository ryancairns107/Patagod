using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Mangerosled : MonoBehaviour
{
    public GameObject[] PirateShipPrefab ;
    public Transform[] SpawnPoints = null;
    public shiphealth healthBar;
    public shiphealth healthBar1;
    public shiphealth healthBar2;
  //  public shiphealth healthBar3;
    public float Osledhealth;
    public float Patahealth;
    public float Jeroenhealth;
  //  public float Ryanhealth;
    public int OsledDeath;
    public int Osledkill;
    public int PataDeath;
    public int Patakill;

    //public float RyanDeath;
    public int JeroenDeath;
    public int Jeroenkill;

    public float GameTimer;
    public float camTimer;
    public GameObject board;
    
    public bool canrespawn;
    public Text textbox;
    public Text OsledDeaths;
    public Text OsledKills;

    public Text PataDeaths;
    public Text PataKills;

    public Text JeroenDeahts;
    public Text JeroenKills;
    public GameObject[] all;

    public GameObject cam;
    public GameObject cam2;


    // Start is called before the first frame update
    void Start()
    {
        // board= GameObject.Find("CompetitionManager/Canvas");
        GameObject pirateShip = Instantiate(PirateShipPrefab[0], SpawnPoints[0].position, SpawnPoints[0].rotation);
        GameObject pirateShip2 = Instantiate(PirateShipPrefab[1], SpawnPoints[1].position, SpawnPoints[1].rotation);
        GameObject pirateShip3 = Instantiate(PirateShipPrefab[2], SpawnPoints[2].position, SpawnPoints[2].rotation);

       
     
      //  healthBar3 = GameObject.Find("RyanShip(Clone)/PirateShip(Clone)/Canvas/Health").GetComponent<shiphealth>();

    }
    void Update()
    {
       
        if (GameTimer <= 0)
        {
            
             board.SetActive(true);
            Time.timeScale = 0F;
            

        }
        Endgame();
        killing();
        healthBar = GameObject.Find("OsledShip(Clone)/PirateShip(Clone)/Canvas/Health").GetComponent<shiphealth>();
        healthBar1 = GameObject.Find("PataShip(Clone)/PirateShip(Clone)/Canvas/Health").GetComponent<shiphealth>();
        healthBar2 = GameObject.Find("JeroenShip(Clone)/PirateShip(Clone)/Canvas/Health").GetComponent<shiphealth>();
       Osledhealth = healthBar.health;
       Patahealth = healthBar1.health;
       Jeroenhealth = healthBar2.health;

        if (Osledhealth <= 0)
        {
            int I = Random.Range(0,3);
            Debug.Log("fuck i respawned with now caps" +
                "ahha");
            GameObject.Find("OsledShip(Clone)").transform.position = SpawnPoints[I].transform.position;

            OsledDeath += 1;
            healthBar.health = 100;
            healthBar.SetHealth(100);

        }
        if (Patahealth <= 0)
        {
            int I = Random.Range(0, 3);
            GameObject.Find("PataShip(Clone)").transform.position = SpawnPoints[I].transform.position;

            PataDeath += 1;
            healthBar1.health = 100;
            healthBar1.SetHealth(100);


        }
        if (Jeroenhealth <= 0)
        {

            JeroenDeath += 1;
            
            int I = Random.Range(0, 3);
            GameObject.Find("JeroenShip(Clone)").transform.position = SpawnPoints[I].transform.position;
            healthBar2.health = 100;
            healthBar2.SetHealth(100);
        }

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (camTimer <=0)
        {
            
            camTimer = 20;
        }
        if (camTimer >= 6)
        {
            cam2.SetActive(false);
            cam.SetActive(true);

        }
        if (camTimer <=5 && camTimer >=1)
        {
            cam.SetActive(false);
            cam2.SetActive(true);
           
           
        }
        camTimer -= Time.deltaTime;
        textbox = GameObject.Find("Canvas/Timer").GetComponent<Text>();
        GameTimer -=Time.deltaTime;
        textbox.text = "Timer"+"  " + Mathf.Round(GameTimer) + "  S" ;
  

    }
    void killing()
    {
       

    }
    void Endgame()
    {
        Patakill = GameObject.Find("OsledShip(Clone)").GetComponent<OsledAI>().Patakill;
        Jeroenkill = GameObject.Find("OsledShip(Clone)").GetComponent<OsledAI>().Jeroenkill;

        OsledDeaths = all[0].GetComponent<Text>();
            OsledKills = all[1].GetComponent<Text>();

            OsledDeaths.text = "" + OsledDeath;
            OsledKills.text = "" ;

            PataDeaths = all[2].GetComponent<Text>();
            PataKills = all[3].GetComponent<Text>();

            PataDeaths.text = "" + PataDeath;
            PataKills.text = ""+ Patakill;

            JeroenDeahts = all[4].GetComponent<Text>();
            JeroenKills = all[5].GetComponent<Text>();

            JeroenDeahts.text = "" + JeroenDeath;
            JeroenKills.text = ""+ Jeroenkill;
    }

   
}
