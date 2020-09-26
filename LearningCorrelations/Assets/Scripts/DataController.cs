using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataController : MonoBehaviour
{
    //reference for this instance
    public static DataController Instance;

    //general settings (get these from somewhere else?)
    public static int maxTrials = 180;
    public static int trialsPerBlock = 20;
    public static int numBlocks = maxTrials / trialsPerBlock;

    public static int[] samplesizes = new int[] { 10, 50, 100 };

    //data variables
    public string participantID;
    public int condition;
    public int finalScore;
    public int trial = 0;
    public List<int> sampleSize;
    public List<double> actualCorr;
    public List<double> userCorr;
    public List<double> corrDiff;
    public string graphIndex;

    //main menu condition
    public int canPlay = 0;

    //saving stuff
    string path = "";

   
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

    public void incrementTrial()
    {
        trial++;
    }

    // create sequence of trials based on condition
    public void createTrialSequence()
    {
        var rnd = new System.Random();

        // randomize correlations within each block
        for (int block = 0; block < (maxTrials / trialsPerBlock); block++)
        {

            //create new set of correlations
            List<float> correlations = new List<float>();
            var rng = Enumerable.Range(-10, 21);
            foreach (float n in rng)
            {
                correlations.Add(n / 10);
            }
            Shuffler.Shuffle(correlations, rnd);

            for (int trial = 0; trial < trialsPerBlock; trial++)
            {
                actualCorr.Add(correlations[trial]);
            }

        }

        // assign sample size to each trial based on condition
        for (int trial = 0; trial < maxTrials; trial++)
        {
            if (condition == 0)
            {
                int ind = (trial / trialsPerBlock) % 3;
                sampleSize.Add(samplesizes[ind]);
            } else
            {
                sampleSize.Add(samplesizes[trial % 3]);
            }

        }

    }

    //saving mehtods
    public void initSave() {
        using (System.IO.StreamWriter file = new System.IO.StreamWriter(path, true)) {
            file.WriteLine(participantID + "," + condition + "," + maxTrials + "," + trialsPerBlock + "," + numBlocks);
        }
    }

    public void logData (string userguess, string diff) {
        using (System.IO.StreamWriter file = new System.IO.StreamWriter(path, true))
        {
            file.WriteLine(trial + "," + getTrialSamplesize() + "," + graphIndex + "," + getTrialCorrelation() + "," + userguess + "," + diff + "," + finalScore);
        }
    }

    public void recordEndingNum(string finalNum) {
        using (System.IO.StreamWriter file = new System.IO.StreamWriter(path, true))
        {
            file.WriteLine(finalNum);
        }
    }


    //getters and setters 
    public void setID(string enteredID){
        participantID = enteredID;
    }

    public void setScore (int score) {
        finalScore = score;
    }

    public void setCondition (int cond) {
        condition = cond;
    }
    public double getTrialCorrelation(){
        return actualCorr[trial];
    }

    public int getTrialSamplesize(){
        return sampleSize[trial];
    }

    public int getTrialMax() {
        return maxTrials;
    }

    public void setPath (string filePath){
        path = filePath;
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

    public void setGraphIndex (string index)
    {
        graphIndex = index;
    }

}

//shuffler class
public static class Shuffler
{
    public static void Shuffle<T>(this IList<T> list, System.Random rnd)
    {
        for (var i = list.Count; i > 0; i--)
            list.Swap(0, rnd.Next(0, i));
    }

    public static void Swap<T>(this IList<T> list, int i, int j)
    {
        var temp = list[i];
        list[i] = list[j];
        list[j] = temp;
    }
}

