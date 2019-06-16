using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulusManager : MonoBehaviour
{
    //Computes the number of real and virtual distractors
    //Places a real and virtual target in different places each trial
    //Assumes the GameManager governs round of play

    //Target position on grid {0 to 24}
    public int realTargetPos;
    public int vrTargetPos;

    //Distractors
    public int realStartingValue;//start with zero
    public int vrStartingValue;

    public int realNumDistractors;//from 0 to 4
    public int vrNumDistractors;//from 0 to 4

    static public int previousRealDistractors;//holds values from previous trials
    static public int previousVrDistractors;

    //Counters
    public bool targetsPlaced = false;
    public bool realDistractorsPlaced = false;
    public bool vrDistractorsPlaced = false;
    public bool valuesPrinted = false;

    //Performance for last trial
    public bool realTrialPerformance;
    public bool vrTrialPerformance;

    void Start()
    {
        //Retrieve previous trial performance from GameManager
        realTrialPerformance = true;//UPDATE THIS
        vrTrialPerformance = true;//UPDATE THIS
        
        //Initialized targets to zero
        realTargetPos = 0;
        vrTargetPos = 0;

        //Retrieve previous values or initialize
        if (previousRealDistractors > 0)
        {
            realNumDistractors = previousRealDistractors;
        }
        else
        {
            realNumDistractors = realStartingValue;
        }

        if (previousVrDistractors > 0)
        {
            vrNumDistractors = previousVrDistractors;
        }
        else
        {
            vrNumDistractors = vrStartingValue;
        }
    }

    void Update()
    {
        //Determine target position for this trial
        if (!targetsPlaced)
        {
            PlaceTargets();
        }

        //Determine number of real world distractors for this trial
        if (!realDistractorsPlaced)
        {
            realNumDistractors = RealDistractors(realNumDistractors, realTrialPerformance);
        }

        //Determine number of virtual disctractors for this trial
        if (!vrDistractorsPlaced)
        {
            vrNumDistractors = VirtualDistractors(vrNumDistractors, vrTrialPerformance);
        }

        //Print values
        if (!valuesPrinted)
        {
            PrintValues(realNumDistractors, vrNumDistractors);
        }

        //Save current values to persistent variable

    }

    void PlaceTargets()
    {
        while (realTargetPos == vrTargetPos)
        {
            realTargetPos = Mathf.RoundToInt(Random.Range(0, 24));
            vrTargetPos = Mathf.RoundToInt(Random.Range(0, 24));
        }
        targetsPlaced = true;
    }

    public int RealDistractors(int _realNumDist, bool _trialPerf)
    {
        //Update number of distractors
        if (_trialPerf){//Correct. Add distractors
            if(_realNumDist < 4)
            {
                _realNumDist++;
            }

        }
        else //Incorrect. Subtract distractors
        {
            if (_realNumDist > 0)
            {
                _realNumDist--;
            }
        }

        realDistractorsPlaced = true;
        return _realNumDist;
    }

    public int VirtualDistractors(int _vrNumDist, bool _trialPerf)
    {
        //Update number of distractors
        if (_trialPerf)
        {//Correct. Add distractors
            if (_vrNumDist < 4)
            {
                _vrNumDist++;
            }

        }
        else //Incorrect. Subtract distractors
        {
            if (_vrNumDist > 0)
            {
                _vrNumDist--;
            }
        }

        vrDistractorsPlaced = true;
        return _vrNumDist;
    }

    void PrintValues(int _realNumDist, int _vrNumDist)
    {
        Debug.Log("Number of Real Distractors = " + _realNumDist);
        Debug.Log("Number of VR Distractors = " + _vrNumDist);
    }
}
