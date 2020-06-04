using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedup : MonoBehaviour
{
    // Start is called before the first frame update
  void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Time.timeScale = 2F;
        }
        if (Input.GetKey(KeyCode.S))
        {
            Time.timeScale = 0.5F;
        }
       
       
    }
}
