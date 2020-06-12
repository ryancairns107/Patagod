using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnitems : MonoBehaviour
{
   
    public GameObject[] items;
   
    public float timer = 10;
 
    void Start()
    {
      
    }


    void Update()
    {

        // spawns items after they have all been used after a 10 seconds timer
     
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

}
