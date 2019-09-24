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

        //check proficiency
        ProficiencyCheck(gameplayManager, newGraph);

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

    //checks to see if player has gotten correlation value correct 20 times (currently not in a row, easy change)
    public void ProficiencyCheck(GameplayManager gp, Graph ng){
        //check if proficiency = 20
        if (gp.proficiency == 20) {
            //if current level is 10 points, switch to 100
            if (ng.pointNum == 10){
                ng.pointNum = 100;
            }
            //if current level is 100 points, switch to 10
            else if (ng.pointNum == 100){
                ng.pointNum = 10;
            }
        }
        else if (gp.proficiency >= 40) {
            //load end scene
            SceneManager.LoadScene(2);
        }
    }

}
