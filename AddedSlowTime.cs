using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddedSlowTime : MonoBehaviour
{
    
    void Update()
    {
       if (Input.GetKey(KeyCode.Q)) {
        Time.timeScale = 0.35f;
       } else {
        Time.timeScale = 1f;
       }
    }
}