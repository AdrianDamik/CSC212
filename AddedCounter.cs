using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Created 2/16/23
// This work is based on the following video: https://www.youtube.com/watch?v=YUcvy9PHeXs

public class AddedCounter : MonoBehaviour
{

    public static AddedCounter instance;

    public Text counterText;
    int counter = 0;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        counterText.text = counter.ToString() + " Steps";
    }

    public void AddStep()
    {
        counter += 1;
        counterText.text = counter.ToString() + " Steps";
    }

    public void ResetSteps()
    {
        counter = 0;
    }
}
