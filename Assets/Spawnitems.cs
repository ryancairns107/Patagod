using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnitems : MonoBehaviour
{
    public GameObject[] PirateShipPrefab;
    public Transform[] SpawnPoints = null;

    public GameObject healthbottle;
    public GameObject ammobottle;
    public GameObject healthbottle1;
    public GameObject ammobottle1;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject pirateShip = Instantiate(PirateShipPrefab[i], SpawnPoints[i].position, SpawnPoints[i].rotation);


        }
    }

    // Update is called once per frame
    void Update()
    {
        healthbottle = GameObject.Find("bottleLarge(Clone)");
        ammobottle = GameObject.Find("bottle(Clone)");
        healthbottle1 = GameObject.Find("bottle(Clone)");
        ammobottle1 = GameObject.Find("bottleLarge(Clone)");
        if (healthbottle == null && healthbottle1 == null && ammobottle == null && ammobottle1 == null )
        {
            spawn();
        }
    }
   public void spawn()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject pirateShip = Instantiate(PirateShipPrefab[i], SpawnPoints[i].position, SpawnPoints[i].rotation);


        }
    }
}
