using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompetitionManager : MonoBehaviour
{
    public GameObject PirateShipPrefab = null;
    public Transform[] SpawnPoints = null;

    private List<PirateShipController> pirateShips = new List<PirateShipController>();

    // Start is called before the first frame update
    void Start()
    {
        BaseAI[] aiArray = new BaseAI[] {
            new IljaAI(), 
            new PondAI(), 
            new PondAI(), 
            new PondAI()
        };

        for (int i = 0; i < 4; i++)
        {
            GameObject pirateShip = Instantiate(PirateShipPrefab, SpawnPoints[i].position, SpawnPoints[i].rotation);
            PirateShipController pirateShipController = pirateShip.GetComponent<PirateShipController>();
            pirateShipController.SetAI(aiArray[i]);
            pirateShips.Add(pirateShipController);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            foreach (var pirateShip in pirateShips) {
                pirateShip.StartBattle();
            }
        }
    }
}
