using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngineInternal;

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
        // Spawner based on the original set defualt spawner 
        // board= GameObject.Find("CompetitionManager/Canvas");
        GameObject pirateShip = Instantiate(PirateShipPrefab[0], SpawnPoints[0].position, SpawnPoints[0].rotation);
        GameObject pirateShip2 = Instantiate(PirateShipPrefab[1], SpawnPoints[1].position, SpawnPoints[1].rotation);
        GameObject pirateShip3 = Instantiate(PirateShipPrefab[2], SpawnPoints[2].position, SpawnPoints[2].rotation);

       
     
     

    }
    void Update()
    {
       // game stops and shows leaderboard
        if (GameTimer <= 0)
        {
            
             board.SetActive(true);
            Time.timeScale = 0F;
            Endgame();

        }
        // runs kill and end game script to input kills and death values
        //Endgame();
       
        healthBar = GameObject.Find("OsledShip(Clone)/PirateShip(Clone)/Canvas/Health").GetComponent<shiphealth>();
        healthBar1 = GameObject.Find("PataShip(Clone)/PirateShip(Clone)/Canvas/Health").GetComponent<shiphealth>();
        healthBar2 = GameObject.Find("JeroenShip(Clone)/PirateShip(Clone)/Canvas/Health").GetComponent<shiphealth>();
        // gets the health of the AIs 
       Osledhealth = healthBar.health;
       Patahealth = healthBar1.health;
       Jeroenhealth = healthBar2.health;
        // checks the AIs health and respawn on death and reset their health and cannons
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
        //change camera with a timer
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
        // set active timer
        camTimer -= Time.deltaTime;
        textbox = GameObject.Find("Canvas/Timer").GetComponent<Text>();
        GameTimer -=Time.deltaTime;
        textbox.text = "Timer"+"  " + Mathf.Round(GameTimer) + "  S" ;
  

    }
 
    void Endgame()
    {
        // calculate death and kills from all the AI scripts and displays them on the leader board
        Patakill = GameObject.Find("OsledShip(Clone)").GetComponent<OsledAI>().Patakill + GameObject.Find("JeroenShip(Clone)").GetComponent<JeroenAI>().PataKills;
        Jeroenkill = GameObject.Find("OsledShip(Clone)").GetComponent<OsledAI>().Jeroenkill + GameObject.Find("PataShip(Clone)").GetComponent<PataAI>().Jeroenkill;
        Osledkill = GameObject.Find("PataShip(Clone)").GetComponent<PataAI>().Osledkill + GameObject.Find("JeroenShip(Clone)").GetComponent<JeroenAI>().OsledKills;

        OsledDeaths = all[0].GetComponent<Text>();
            OsledKills = all[1].GetComponent<Text>();

            OsledDeaths.text = "" + OsledDeath;
            OsledKills.text = "" +Osledkill;

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
