﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kociemba;


public class SolveTwoPhase : MonoBehaviour
{
    private ReadCube readCube;
    private CubeState cubeState;
    private bool doOnce = true;

    // Start is called before the first frame update
    void Start()
    {
        readCube = FindObjectOfType<ReadCube>();
        cubeState = FindObjectOfType<CubeState>();

    }

    // Update is called once per frame
    void Update()
    {
        if(CubeState.started && doOnce)
        {
            doOnce = false;
            Solver();
        }
    }

    public void Solver()
    {

        AddedCounter.instance.ResetSteps();              // Added 2/16/23

        Debug.Log("function called");

        readCube.ReadState();

        //Get the State of the cube as a string
        string moveString = cubeState.GetStateString();

        Debug.Log(moveString);

        //Solve the Cube

        string info = "";

        //First Time Build the Tables
        //string solution = SearchRunTime.solution(moveString, out info, buildTables: true);

        //Every other time
        string solution = Search.solution(moveString, out info);


        //Convert the Solved Moves from a string to a list
        List<string> solutionList = StringToList(solution);



        // I am doing a test to just get a feel of C#
        string output_test = "";

        for(int i = 0; i < solutionList.Count; i++)
        {
            AddedCounter.instance.AddStep();                // Added 2/16/23
            output_test += " " + solutionList[i];
        }
        Debug.Log("Move list: " + output_test);
        // my code above 

        //Automate the List
        Automate.moveList = solutionList;
    }

    List<string> StringToList(string solution)
    {
        List<string> solutionList = new List<string>(solution.Split(new string[] { " " }, System.StringSplitOptions.RemoveEmptyEntries));
        return solutionList;
    }
}
