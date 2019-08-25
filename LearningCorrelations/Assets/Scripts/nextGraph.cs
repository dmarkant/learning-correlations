using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class nextGraph : MonoBehaviour
{
    private Graph newGraph;
    private GameplayManager gameplayManager;

    public void next() {
        //references to other scripts
        newGraph = GameObject.FindObjectOfType<Graph>();
        gameplayManager = GameObject.FindObjectOfType<GameplayManager>();

        //destroy old points
        for (int i = 0; i < newGraph.points.Count; i++)
        {
            Destroy(newGraph.points[i]);
        }

        //get num of points on graph
        int points = newGraph.pointNum;

        //reset the x and y values
        newGraph.x.Clear();
        newGraph.y.Clear();

        //create new graph
        newGraph.showGraph(points);

        List<int> x = newGraph.x;
        List<int> y = newGraph.y;

        gameplayManager.updateCorrelation(x, y);

        gameplayManager.nextButton.SetActive(false);

        //reset text boxes
        gameplayManager.showDiff.text = "Difference: ";
        gameplayManager.showCorr.text = "Pearson Correlation: ";
        gameplayManager.showText.text = "Your Guess: ";
        
    }

}
