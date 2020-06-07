using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
  
    public Aistest Osledkill;
    public Aistest health;
    void Start()
    {
        Osledkill = GameObject.Find("OsledShip(Clone)").GetComponent<Aistest>();
    }

  
    void FixedUpdate()
    {
        health = GameObject.Find("OsledShip(Clone)").GetComponent<Aistest>();
        transform.Translate(new Vector3(0f, 0f, 500 * Time.fixedDeltaTime), Space.Self);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "OsledPirateShip"&& health.currentHealth <=4)
        {
            Osledkill.kill += 1;
        }
      
    }
   
}
