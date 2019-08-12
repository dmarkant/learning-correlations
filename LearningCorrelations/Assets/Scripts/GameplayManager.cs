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
        int sum_X = 0, sum_Y = 0, sum_XY = 0;
        int squareSum_X = 0, squareSum_Y = 0;
        int n = x.Count;

        //loop through all points
        for (int i = 0; i < n; i++){
            // sum of elements of array X. 
            sum_X = sum_X + x[i];

            // sum of elements of array Y. 
            sum_Y = sum_Y + y[i];

            // sum of X[i] * Y[i]. 
            sum_XY = sum_XY + x[i] * y[i];

            // sum of square of array elements. 
            squareSum_X = squareSum_X + x[i] * x[i];
            squareSum_Y = squareSum_Y + y[i] * y[i];
        }


        // use formula for calculating correlation coefficient. 
        double corr = (double)(n * sum_XY - sum_X * sum_Y) /
              (Math.Sqrt(n * squareSum_X - sum_X * sum_X) *
               Math.Sqrt(n * squareSum_Y - sum_Y * sum_Y));
        //double corr = (double)(n * sum_XY - sum_X * sum_Y)
        //              / Math.Sqrt((n * squareSum_X - sum_X * sum_X)
        //                  * (n * squareSum_Y - sum_Y * sum_Y));

        print(corr);

        showCorr.text = "Pearson Correlation: " + Math.Round(corr, 2).ToString();

    }


}
