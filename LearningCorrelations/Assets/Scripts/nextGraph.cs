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
        newGraph.Reset();

        //check proficiency
        //ProficiencyCheck(gameplayManager, newGraph);
        reachedMaxTrial();

        //create new graph
        newGraph.samplesize = DataController.Instance.getTrialSamplesize();
        double r = DataController.Instance.getTrialCorrelation();
        //decimal r = -0.3m;
        int datasetIndex = Random.Range(0, 100);
        newGraph.LoadDataset(newGraph.samplesize, r, datasetIndex);
        newGraph.showGraph();

        List<int> x = newGraph.x;
        List<int> y = newGraph.y;
        //gameplayManager.updateCorrelation(x, y);

        gameplayManager.nextButton.SetActive(false);

        //reset text boxes
        gameplayManager.showDiff.text = "Difference: ";
        gameplayManager.showCorr.text = "Pearson Correlation: ";
        gameplayManager.showText.text = "Your Guess: ";
        
    }

    //checks to see if player has gotten correlation value correct 20 times (currently not in a row, easy change)
    public void reachedMaxTrial(){//ProficiencyCheck() {GameplayManager gp, Graph ng){
        //check if proficiency = 20
       // if (gp.proficiency == 20) {
            //if current level is 10 points, switch to 100
            //if (ng.pointNum == 10){
            //    ng.pointNum = 100;
            //}
            //if current level is 100 points, switch to 10
            //else if (ng.pointNum == 100){
            //    ng.pointNum = 10;
            //}
        //}
       // else if (gp.proficiency >= 40) {
            //load end scene
         //   SceneManager.LoadScene(2);
       // }
       if (DataController.Instance.trial >= DataController.Instance.getTrialMax()){
            SceneManager.LoadScene(2);
        }
    }

}
