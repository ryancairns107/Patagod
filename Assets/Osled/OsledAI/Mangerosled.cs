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
    public float OsledDeath;
    public float PataDeath;
    //public float RyanDeath;
    public float JeroenDeath;
    public float GameTimer;
    public GameObject board;


    // Start is called before the first frame update
    void Start()
    {
       // board= GameObject.Find("CompetitionManager/Canvas");

        for (int i = 0; i < 4; i++)
        {
            GameObject pirateShip = Instantiate(PirateShipPrefab[i], SpawnPoints[i].position, SpawnPoints[i].rotation);
         
           
        }
     
      //  healthBar3 = GameObject.Find("RyanShip(Clone)/PirateShip(Clone)/Canvas/Health").GetComponent<shiphealth>();

    }
    void Update()
    {
        if (GameTimer <= 0)
        {
            board.SetActive(true);
            Time.timeScale = 0F;
        }
        healthBar = GameObject.Find("OsledShip(Clone)/PirateShip(Clone)/Canvas/Health").GetComponent<shiphealth>();
        healthBar1 = GameObject.Find("PataShip(Clone)/PirateShip(Clone)/Canvas/Health").GetComponent<shiphealth>();
        healthBar2 = GameObject.Find("JeroenShip(Clone)/PirateShip(Clone)/Canvas/Health").GetComponent<shiphealth>();
        Osledhealth = healthBar.health;
        Patahealth = healthBar1.health;
        Jeroenhealth = healthBar2.health;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        GameTimer -=Time.deltaTime;
    
       // Ryanhealth = healthBar3.health;
        if (Osledhealth <=0)
        {
            OsledDeath+=1;
            Osledhealth = 100;
        }
        if (Patahealth <= 0)
        {
            PataDeath += 1;
            Patahealth = 100;
        }
        if (Jeroenhealth <= 0)
        {
           JeroenDeath += 1;
            Jeroenhealth = 100;
        }
       /* if (Ryanhealth <= 0)
        {
           // RyanDeath += 1;
            Ryanhealth = 100;
        }*/
     

    }
  
}
