using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class Graph : MonoBehaviour
{
    //reference for graph object
    public RectTransform graphContainer;

    //reference for point sprite
    public Sprite pointSprite;

    public int samplesize;

    //array of points (z-scores)
    public List<float> z_x = new List<float>();
    public List<float> z_y = new List<float>();

    //array of points to calculate correlation
    public List<int> x = new List<int>();
    public List<int> y = new List<int>();

    //reference gameplay manage
    private GameplayManager gameplayManager;

    //create list to do thing with points
    public List<GameObject> points = new List<GameObject>();

    //new graph
    private Graph newGraph;

    //when graph component starts up
    void Awake(){

        // get settings for this trial and load dataset from file
        samplesize = DataController.Instance.getTrialSamplesize();
        double r = DataController.Instance.getTrialCorrelation();
        int datasetIndex = UnityEngine.Random.Range(0, 100);
        LoadDataset(samplesize, r, datasetIndex);

        //grab gameplay manager
        gameplayManager = GameObject.FindObjectOfType<GameplayManager>();

        //grabing the graph object
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();

        //plot points on the graph
        showGraph();

        //find correlation
       // gameplayManager.updateCorrelation(x, y);

    }

    //Load dataset of points with desired samplesize and correlation
    public void LoadDataset(int samplesize, double r, int datasetIndex)
    {
        string r_str = String.Format("{0:0.0}", r);
        string datasetPath = string.Format("Assets/Files/datasets/dataset_r={0}_n={1}_ind={2}.txt", r_str, samplesize, datasetIndex);
        print(datasetPath);
        DataController.Instance.setGraphIndex(datasetIndex.ToString());
        string[] lines = File.ReadAllLines(datasetPath);
        string[] coord;
        foreach (var line in lines)
        {
            coord = line.Split(' ');
            float z_x_i = float.Parse(coord[0]);
            float z_y_i = float.Parse(coord[1]);
            z_x.Add(z_x_i);
            z_y.Add(z_y_i);
        }
    }

    //Creates circle for each of the dots
    public void CreatePoints(Vector2 anchoredPosition){
        //creates game object (point)
        GameObject gameObject = new GameObject("point", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = pointSprite;

        //grab reference to the rect transform
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();

        //set anchored position
        rectTransform.anchoredPosition = anchoredPosition;

        //sizes of point
        rectTransform.sizeDelta = new Vector2(9, 9);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);

        //add points to list to mess with later
        points.Add(gameObject);
    }

    public void Reset()
    {
        //destroy old points
        for (int i = 0; i < points.Count; i++)
        {
            Destroy(points[i]);
        }

        //reset the x and y values
        z_x.Clear();
        z_y.Clear();
        x.Clear();
        y.Clear();

    }


    //method setting location for each point on the graph (takes in int - number of desired points)
    public void showGraph(){
        //get the width and height of the graph to set ranges of location
        int graphHeight = (int)graphContainer.sizeDelta.y;
        int graphWidth = (int)graphContainer.sizeDelta.x;
        int margin = 4;

        //loop through the number of desired points
        for (int i = 0; i < samplesize; i++){

            // convert z-scores to coordinates on graph
            double range = 7;
            int xPosition = (int)(margin + (graphWidth-margin) * ((z_x[i] + range/2) / range));
            int yPosition = (int)(margin + (graphHeight-margin) * ((z_y[i] + range/2) / range));

            //create each point with new random location
            CreatePoints(new Vector2(xPosition, yPosition));
            
            //add points to list
            x.Add(xPosition);
            y.Add(yPosition);
        }
    }

    //new graphh code
    public void next()
    {
        //references to other scripts
        newGraph = GameObject.FindObjectOfType<Graph>();
        //gameplayManager = GameObject.FindObjectOfType<GameplayManager>();

        //destroy old points
        newGraph.Reset();
        DataController.Instance.incrementTrial();
        reachedMaxTrial();

        //create new graph
        newGraph.samplesize = DataController.Instance.getTrialSamplesize();
        double r = DataController.Instance.getTrialCorrelation();
        //decimal r = -0.3m;
        int datasetIndex = UnityEngine.Random.Range(0, 100);
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
        gameplayManager.submitButton.SetActive(true);

        //reset slider
        gameplayManager.guessInput.value = 10F;

        //set trial num
        gameplayManager.showID.text = DataController.Instance.trial + "/" + DataController.Instance.getTrialMax();
    }

    //checks to see if player has reach the max number of trials
    public void reachedMaxTrial() {
        if (DataController.Instance.trial >= DataController.Instance.getTrialMax()) {
            SceneManager.LoadScene(2);
        }
    }

}
