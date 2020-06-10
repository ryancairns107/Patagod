using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
  
    public OsledAI Osledkill;
    public OsledAI health;
    void Start()
    {
        Osledkill = GameObject.Find("OsledShip(Clone)").GetComponent<OsledAI>();
    }

  
    void FixedUpdate()
    {
        health = GameObject.Find("OsledShip(Clone)").GetComponent<OsledAI>();
        transform.Translate(new Vector3(0f, 0f, 500 * Time.fixedDeltaTime), Space.Self);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "OsledPirateShip"&& health.currentHealth <=4)
        {
            Osledkill.kill += 1;
        }
        if (other.gameObject.tag == "wall")
        {
            Destroy(gameObject);
         //   Debug.Log("destroycvannon");
        }

    }
     void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            Destroy(gameObject);
          //  Debug.Log("destroycvannon");
        }
    }

}
