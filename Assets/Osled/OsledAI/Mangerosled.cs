using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mangerosled : MonoBehaviour
{
    public GameObject[] PirateShipPrefab ;
    public Transform[] SpawnPoints = null;

  

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
    
    }
}
