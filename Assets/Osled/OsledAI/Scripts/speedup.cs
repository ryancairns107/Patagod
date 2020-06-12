using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedup : MonoBehaviour
{
    // on button press speed up the game
  void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Time.timeScale += 2F;
        }
        if (Input.GetKey(KeyCode.S))
        {
            Time.timeScale = 1F;
        }
       
       
    }
}
