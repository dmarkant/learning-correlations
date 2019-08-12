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

    public void updateCorrelation(List<int> x, List<int> y){
        //values for pearson correlation coefficient
        double sum_X = 0, sum_Y = 0;
        double meanx = 0, meany = 0, devsqrdX = 0, devsqrdY = 0, combxy = 0;
        int n = x.Count;

        //get the sums
        for (int i = 0; i < n; i++){
             //sum of elements of array X. 
            sum_X = sum_X + x[i];

            // sum of elements of array Y. 
            sum_Y = sum_Y + y[i];

        }

        //get the means
        meanx = sum_X / n;
        meany = sum_Y / n;

        //get the deviations
        for (int i = 0; i < n; i++)
        {
            //calc dev
            double devX = x[i] - meanx;
            double devY = y[i] - meany;
            
            //get squared devs
            devsqrdX = (devX * devX) + devsqrdX;
            devsqrdY = (devY * devY) + devsqrdY;

            //get combined devs
            combxy = (devX * devY) + combxy;
        }

        //pearson correlation equation
        double corr = (double)combxy / Math.Sqrt(devsqrdX * devsqrdY);

        //output correlation - round to 2 decimal places
        corr = (double) Math.Round(corr, 2, MidpointRounding.AwayFromZero);
        showCorr.text = "Pearson Correlation: " + corr.ToString();

    }


}
