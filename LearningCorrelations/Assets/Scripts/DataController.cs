using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController : MonoBehaviour
{
    //reference for this instance
    public static DataController Instance;

    //data varibales
    public string participantID;
    public int condition;
    public int finalScore;
    public int r1Trials;
    public int r2Trials;
    public List<double> actualCorr;
    public List<double> userCorr;
    public List<double> corrDiff;

    //awake method makes this persisten object
    void Awake () {
        //code below makes this a persisten object
        if (Instance == null) {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this) {
            Destroy (gameObject);
        }
      }

    //getters and setters
    //public string getID () {
    //    return participantID;
   // }
    public void setID(string enteredID){
        participantID = enteredID;
    }

    public void setScore (int score) {
        finalScore = score;
    }

    public void setCondition (int cond) {
        if (cond == 10) {
            condition = 0;
        }
        else if (cond == 100) {
            condition = 1;
        }
    }

    public void setR1Trials (int trials1) {
        r1Trials = trials1;
    }

    public void setR2Trials (int trials2) {
        r2Trials = trials2;
    }

    public void setActCorr (double corr) {
        actualCorr.Add(corr);
    }

    public void setUserCorr (double guess) {
        userCorr.Add(guess);
    }

    public void setDiffs (double diff) {
        corrDiff.Add(diff);
    }
}
