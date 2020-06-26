using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Graph : MonoBehaviour
{
    //reference for graph object
    public RectTransform graphContainer;

    //reference for point sprite
    public Sprite pointSprite;

    //number of points being displayed
    //public int[] levels = new int[] {10, 100};
    // public int randIndex = Random.Range(0, levels.Length);
    public int pointNum = 0;
    public int condition = 5;
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

    //when graph component starts up
    void Awake(){
        //points
        int[] levels = new int[] {10, 100};
        int randIndex = Random.Range(0, levels.Length);
        //int pointNum = levels[randIndex];

        //load dataset from file
        samplesize = 100;
        decimal r = -0.1m;
        int datasetIndex = Random.Range(0, 100);
        LoadDataset(samplesize, r, datasetIndex);

        //grab gameplay manager
        gameplayManager = GameObject.FindObjectOfType<GameplayManager>();

        //grabing the graph object
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();

        //plot points on the graph
        showGraph();

        //find correlation
        gameplayManager.updateCorrelation(x, y);

        //save condition (only do this once)
        //DataController.Instance.setCondition(samplesize);

    }

    //Load dataset of points with desired samplesize and correlation
    public void LoadDataset(int samplesize, decimal r, int datasetIndex)
    {
        string datasetPath = string.Format("Assets/Files/datasets/dataset_r={0}_n={1}_ind={2}.txt", r, samplesize, datasetIndex);
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
        rectTransform.sizeDelta = new Vector2(11, 11);
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
            int xPosition = (int)(margin + (graphWidth-margin) * ((z_x[i] + 3) / 6));
            int yPosition = (int)(margin + (graphHeight-margin) * ((z_y[i] + 3) / 6));

            //create each point with new random location
            CreatePoints(new Vector2(xPosition, yPosition));
            
            //add points to list
            x.Add(xPosition);
            y.Add(yPosition);
        }
    }

}
