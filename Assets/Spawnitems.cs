using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnitems : MonoBehaviour
{
   // public GameObject[] PirateShipPrefab;
   // public Transform[] SpawnPoints = null;
    public GameObject[] items;
   // public GameObject healthbottle;
   // public GameObject ammobottle;
    //public GameObject healthbottle1;
    //public GameObject ammobottle1;
    public float timer = 10;
    // Start is called before the first frame update
    void Start()
    {
      /*  for (int i = 0; i < 4; i++)
        {
            GameObject pirateShip = Instantiate(PirateShipPrefab[i], SpawnPoints[i].position, SpawnPoints[i].rotation);


        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
      //  healthbottle = GameObject.Find("bottleLarge(Clone)");
      //  ammobottle = GameObject.Find("bottle(Clone)");
       // healthbottle1 = GameObject.Find("bottle(Clone)");
       // ammobottle1 = GameObject.Find("bottleLarge(Clone)");
        if (items[0].activeSelf== false && items[1].activeSelf == false && items[2].activeSelf == false && items[3].activeSelf == false )
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                //spawn();
                items[0].SetActive(true);
                items[1].SetActive(true);
                items[2].SetActive(true);
                items[3].SetActive(true);
                timer = 10;

            }
            
        }
    }
 /*  public void spawn()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject pirateShip = Instantiate(PirateShipPrefab[i], SpawnPoints[i].position, SpawnPoints[i].rotation);


        }
    }*/
}
