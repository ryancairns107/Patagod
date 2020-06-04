using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mangerosled : MonoBehaviour
{
    public GameObject PirateShipPrefab = null;
    public Transform[] SpawnPoints = null;

    private List<Pirateshiposledcontroller> pirateShips = new List<Pirateshiposledcontroller>();

    // Start is called before the first frame update
    void Start()
    {
        OslledbaseAI[] aiArray = new OslledbaseAI[] {
            new OsledAI(),
            new OsledAI(),
            new OsledAI(),
            new OsledAI()
        };

        for (int i = 0; i < 4; i++)
        {
            GameObject pirateShip = Instantiate(PirateShipPrefab, SpawnPoints[i].position, SpawnPoints[i].rotation);
            Pirateshiposledcontroller pirateShipController = pirateShip.GetComponent<Pirateshiposledcontroller>();
            pirateShipController.SetAI(aiArray[i]);
            pirateShips.Add(pirateShipController);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (var pirateShip in pirateShips)
            {
                pirateShip.StartBattle();
            }
        }
    }
}
