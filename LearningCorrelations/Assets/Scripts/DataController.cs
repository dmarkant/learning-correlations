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
    public string getID () {
        return participantID;
    }
    public void setID(string enteredID){
        participantID = enteredID;
    }
}
