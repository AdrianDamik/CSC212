using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddedSlowTime : MonoBehaviour
{
    
    bool isPaused = false;

    void Update()
    {
       
       // adjust game speed
       if (Input.GetKeyDown(KeyCode.Alpha2)) {           // very slow 
        Time.timeScale = 0.25f;

       } else if (Input.GetKeyDown(KeyCode.Alpha3)) {    // slow 
        Time.timeScale = 0.5f;

       } else if (Input.GetKeyDown(KeyCode.Alpha4)) {    // fast 
        Time.timeScale = 1.5f;

       } else if (Input.GetKeyDown(KeyCode.Alpha5)) {    // very fast 
        Time.timeScale = 1.75f;

       } else if (Input.GetKeyDown(KeyCode.Alpha1)) {    // normal speed
        Time.timeScale = 1f;
        }

        // pause the game
        if (Input.GetKeyDown(KeyCode.P)) { // pause
          if (isPaused == false) {
            Time.timeScale = 0f;
            isPaused = true;

          } else if (isPaused == true) {        // resume
            Time.timeScale = 1f;
            isPaused = false;
          }
        }
    }
}