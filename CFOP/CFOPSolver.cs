using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CubeXdotNET;

using Kociemba;
using UnityEngine.Windows;
using System;
using System.Linq;
using UnityEngine.UI;
// I am including this because the original creator had included the code of the visualizer in the Kociemba solver.




public class CFOPSolver : MonoBehaviour
{
    // Start is called before the first frame update

    private ReadCube readCube;
    private CubeState cubeState;

    void Start()
    {
        /*
       ReadCube readCube;
       CubeState cubeState;
       
       // obtains initial data from the cube upon startup.
       readCube = FindObjectOfType<ReadCube>();
       cubeState = FindObjectOfType<CubeState>();

       string moveString = new string(cubeState.GetStateString());

        //translatess the initial data into a format the CFOP solver can handle.

        FridrichSolver Solver2 = new FridrichSolver("gggggggggooooooooobbbbbbbbbrrrrrrrrryyyyyyyyywwwwwwwww");
        // this is the solved state of the rubix cube per the original solver's documentation.
        // We want it initalized as an untouched cube.
        // I am referring it to as solver2 because it is the second solver implemented.
        */

        readCube = FindObjectOfType<ReadCube>(); // obtains the data from the rubix cube -Elijah Gray 2/21/2023
        cubeState = FindObjectOfType<CubeState>();

    }

    public void Solver2()
    {

        AddedCounter.instance.ResetSteps(); // Added 2/28/23

        string solution = "";

        Debug.Log("function 2 called");

        string original = cubeState.GetStateString();
        Debug.Log("Original: " + original);

        //Get the State of the cube as a string
        
        string moveString = ""; // cubeState.GetStateString();
        moveString += cubeState.get_left(); // the visualizer has a function to get a face of the cube as a string. Most of the cube's faces's facelets are ordered the same so little change is needed for these ones
        moveString += cubeState.get_front(); // other than reordering them
        moveString += cubeState.get_right();
        moveString += cubeState.get_back();


        //CFOP is ordered in a weird way for these two faces. This is a rough way of
        //doing it, I just wanted to test
        // each facelet in the visualizer is stored sequentially top left to bottom right for every face
        // 6 3 0
        // 7 4 1
        // 8 5 2

        // the up and down face's facelets are ordered differently unlike the other faces, we are adding each individual facelet's data bit by bit.
        string up = cubeState.get_up();
        string handle_up = "";

        handle_up += up.ElementAt(8);
        handle_up += up.ElementAt(7);
        handle_up += up.ElementAt(6);
        handle_up += up.ElementAt(5);

        handle_up += up.ElementAt(4);

        handle_up += up.ElementAt(3);
        handle_up += up.ElementAt(2);
        handle_up += up.ElementAt(1);  
        handle_up += up.ElementAt(0);

        Debug.Log("resulting up string 1: " + handle_up);
        moveString += handle_up; // add the new up face's string to the string representation of the rubix cube.

        // DOWN is stored
        // 2 5 8
        // 1 4 7
        // 0 3 6

        // the up and down face's facelets are ordered differently unlike the other faces, we are adding each individual facelet's data bit by bit.
        string down = cubeState.get_down();
        string handle_down = "";

        handle_down += down[8];
        handle_down += down[7];
        handle_down += down[6];
        handle_down += down[5];  

        handle_down += down[4]; 

        handle_down += down[3];
        handle_down += down[2];
        handle_down += down[1];
        handle_down += down[0];

        //+ up[5] + up[8] + up[1] + up[4] + up[7] + up[0] + up[3] + up[6];
        Debug.Log("resulting up string 2: " + handle_down);

        moveString += handle_down; // add the new down face's string to the string representation of the rubix cube.


        Debug.Log("initial untranslated: " + moveString);

        //translate the Cube
        string translation = string_translation(moveString);
        Debug.Log("translated cube: " + translation);

        FridrichSolver cube_to_solve = new FridrichSolver(translation);

        cube_to_solve.Solve();
        Debug.Log("solved?: " + cube_to_solve.IsSolved);
        Debug.Log("Error code: " + cube_to_solve.ErrorCode);
        Debug.Log("output solution: " + cube_to_solve.Solution);


        solution = cube_to_solve.Solution; // obtains the solution from the CFOP solver

        //Convert the Solved Moves from a string to a list
        List<string> solutionList = StringToList(solution); // converts the CFOP solver's string into a list of moves.

        //string_translation2(solutionList);


        // converts the solution list into a set of moves that make sense for the visualizer as each use different colors for the faces.
        // for example, the CFOP solver views the green face as the front and the visualizer sees the orange face as the front.
        // -Elijah Gray 2/21/2023
        for(int i = 0; i < solutionList.Count; i++)
        {
            switch (solutionList[i].First())
            {
                case 'L':
                    solutionList[i] = solutionList[i].Replace(solutionList[i][0], 'B');
                    //Debug.Log("L -> B");
                    break;

                case 'F':
                    solutionList[i] = solutionList[i].Replace(solutionList[i][0], 'L');
                   //Debug.Log("F -> L");
                    break;

                case 'R':
                    solutionList[i] = solutionList[i].Replace(solutionList[i][0], 'F');
                    //Debug.Log("R -> F");
                    break;

                case 'B':
                    solutionList[i] = solutionList[i].Replace(solutionList[i][0], 'R');
                    //Debug.Log("B -> R");
                    break;

                default:
                    break;

            }
        }


        // debug for checking list elements

        /*
        string output_test = "";

        for (int i = 0; i < solutionList.Count; i++)
        {
            output_test += " " + solutionList[i];
        }
        Debug.Log("Move list: " + output_test);
        */


        Debug.Log("after state of solved cube: " + getString(cube_to_solve.Cube));


        //Automate the List
        Automate.moveList = solutionList;

    }


        // converts the string into a list of steps to be used by the visualizer rotating automaton
        List<string> StringToList(string solution)
        {
            List<string> solutionList = new List<string>(solution.Split(new string[] { " " }, System.StringSplitOptions.RemoveEmptyEntries));
            return solutionList;
        }


    // translates the visualizer facelets into a format the CFOP solver uses
    // -Elijah Gray 2/21/2023
    string string_translation(string to_translate)
        {
            string translated_string = "";

            for (int i = 0; i < to_translate.Length; i++)
            {

                switch (to_translate.ElementAt(i))
                {
                    case 'U':
                        translated_string += "y";
                        break;

                    case 'D':
                        translated_string += "w";
                        break;

                    case 'L':
                        translated_string += "g";
                        break;

                    case 'R':
                        translated_string += "b";
                        break;

                    case 'F':
                        translated_string += "o";
                        break;

                    case 'B':
                        translated_string += "r";
                        break;

                    default:
                        break;

                }
            }

            return translated_string;
        }


    // function instructions taken from geek 4 geeks -Elijah 2/21/2023
    string getString(char[] arr)
    {
        // string() is a used to 
        // convert the char array
        // to string
        string s = new string(arr);

        return s;
    }

    /// unused part
    void string_translation2(List<string> solutionList)
    {

        // -Elijah Gray 2/16/2023
        // goes through the solution list and translates them to make sense as each of the solvers represent their faces differently, so a front face turn for the CFOP solver means something different for the
        // visualizer's solver.
        for (int i = 0; i < solutionList.Count; i++)
        {
            switch (solutionList[i][0])
            {
                case 'L':
                    solutionList[i].Replace(solutionList[i][0], 'F');
                    break;

                case 'F':
                    solutionList[i].Replace(solutionList[i][0], 'L');
                    break;

                case 'R':
                    solutionList[i].Replace(solutionList[i][0], 'B');
                    break;

                case 'B':
                    solutionList[i].Replace(solutionList[i][0], 'R');
                    break;

                default:
                    break;

            }
        }

        //debug to test if list elements were being replaced
        /*
        string output_test = "";

        for (int i = 0; i < solutionList.Count; i++)
        {
            output_test += " " + solutionList[i];
        }
        Debug.Log("Move list modified: " + output_test);
        */


    }




}




