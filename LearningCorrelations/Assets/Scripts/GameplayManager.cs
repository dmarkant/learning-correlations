using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
//using std;

public class GameplayManager : MonoBehaviour
{
    //reference for text box
    public Text showCorr;
    public Text showText;
    public Text showDiff;
    public Text showScore;
    public Text sliderValue;

    //test id transfer
    public Text showID;

    //reference for input field
    //public InputField guessInput;
    public Slider guessInput;

    //reference for next button
    public GameObject nextButton;

    //variables to save
    double userGuess = 0;
    double corr = 0;
    double guessDiff = 0;
    int score = 0;

    //audio variables
    public AudioSource audioSource;
    public AudioClip correctAudio;
    public AudioClip incorrectAudio;

    //start method to display id 
    void Start(){
        showID.text = DataController.Instance.participantID;
        audioSource = GetComponent<AudioSource>();
    }

    //update method to use enter instead of clicking the submit button
    void Update(){
        //hit enter to do same action as clicking submit
        if (Input.GetKeyDown(KeyCode.Return))
        {
            guessCalculation();
            enableButton();
        }

        //update displayed correlation under slider
        userGuess = (double.Parse(guessInput.value.ToString()) - 10)/10;
        sliderValue.text = userGuess.ToString();
        //sliderValue.text = guessInput.value.ToString();
    }
    
    //show entered guess and calculate difference
    public void guessCalculation(){
        //ensure user entered answer then run code
        if (/*guessInput.text*/guessInput.value.ToString() != "") {

            //round the correlation, otherwise it can have many places after the decimal
            corr = (double) Math.Round(DataController.Instance.getTrialCorrelation(), 2, MidpointRounding.AwayFromZero);
            userGuess = (double.Parse(guessInput.value.ToString()) - 10) / 10;

            //userGuess = (double)(guessInput.value - 10) / 10;
            showText.text = "Your Guess: " + userGuess.ToString();

            //showText.text = "Your Guess: " + /*guessInput.text*/guessInput.value.ToString();

            showCorr.text = "Pearson Correlation: " + corr.ToString();

            //userGuess = double.Parse(/*guessInput.text*/guessInput.value.ToString());

            guessDiff = userGuess - corr;

            showDiff.text = "Difference: " + guessDiff.ToString();

            calcScore(guessDiff, corr);
        }

        //save data
        DataController.Instance.setUserCorr(userGuess);
        DataController.Instance.setDiffs(guessDiff);
    }

    //enables next button
    public void enableButton(){
        //able to select next button if user entered answer
        if (guessInput.value.ToString() != "") {
            nextButton.SetActive(true);
        }
    }

    //determines score based on user response
    public void calcScore(double diff, double correlation)
    {
        //determine score increase or decrease
        if (diff == 0)
        {
            score = score + 4;
            //play sound
            audioSource.PlayOneShot(correctAudio, .75f);
        }
        else if (diff <= .05 & diff >= -.05)
        {
            score = score + 2;
            //play sound
            audioSource.PlayOneShot(correctAudio, .75f);
        }
        else if (diff >= .3 | diff <= -.3)
        {
            score = score - 2;
            //play sound
            audioSource.PlayOneShot(incorrectAudio, .75f);
        }
        else
        {
            score = score + 0;
            //play sound
            audioSource.PlayOneShot(incorrectAudio, .75f);
        }

        //show score
        showScore.text = "SCORE: " + score.ToString();
        
        //save score and trials to use across scenes
        DataController.Instance.setScore(score);
        DataController.Instance.logData(userGuess.ToString(), guessDiff.ToString());
    }

////Method to calculate the correlation value
    //public void updateCorrelation(List<int> x, List<int> y){

    //    //values for pearson correlation coefficient
    //    double sum_X = 0, sum_Y = 0;
    //    double meanx = 0, meany = 0, devsqrdX = 0, devsqrdY = 0, combxy = 0;
    //    int n = x.Count;

    //    //get the sums
    //    for (int i = 0; i < n; i++){
    //         //sum of elements of array X. 
    //        sum_X = sum_X + x[i];

    //        // sum of elements of array Y. 
    //        sum_Y = sum_Y + y[i];

    //    }

    //    //get the means
    //    meanx = sum_X / n;
    //    meany = sum_Y / n;

    //    //get the deviations
    //    for (int i = 0; i < n; i++)
    //    {
    //        //calc dev
    //        double devX = x[i] - meanx;
    //        double devY = y[i] - meany;
            
    //        //get squared devs
    //        devsqrdX = (devX * devX) + devsqrdX;
    //        devsqrdY = (devY * devY) + devsqrdY;

    //        //get combined devs
    //        combxy = (devX * devY) + combxy;
    //    }

    //    //pearson correlation equation
    //    corr = (double)combxy / Math.Sqrt(devsqrdX * devsqrdY);

    //    //output correlation - round to 2 decimal places
    //    corr = (double) Math.Round(corr, 2, MidpointRounding.AwayFromZero);

    //    print(corr);

    //}
}
