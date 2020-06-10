using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyontake : MonoBehaviour
{
    public GameObject Osled;
    public GameObject Pata;
   // public GameObject Ryan;
    public GameObject Jeroen;

 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Osled = GameObject.Find("OsledShip(Clone)");
        Pata = GameObject.Find("PataShip(Clone)");
      //  Ryan = GameObject.Find("RyanShip(Clone)");
        Jeroen = GameObject.Find("JeroenShip(Clone)");
  

}
     void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Osled)
        {
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
      
       
        if (other.gameObject == Pata)
        {
            gameObject.SetActive(false);
            //  Destroy(gameObject);
        }
        if (other.gameObject == Jeroen)
        {
            gameObject.SetActive(false);
            //  Destroy(gameObject);
        }
    }
}
