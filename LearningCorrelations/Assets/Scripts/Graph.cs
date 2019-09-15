using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Graph : MonoBehaviour
{
    //reference for graph object
    public RectTransform graphContainer;
    //reference for point sprite
    public Sprite pointSprite;
    //number of points being displayed
    //public int[] levels = new int[] {10, 100};
    // public int randIndex = Random.Range(0, levels.Length);
    public int pointNum = 0;//levels[randIndex];

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
        int pointNum = levels[randIndex];

        //grab gameplay manager
        gameplayManager = GameObject.FindObjectOfType<GameplayManager>();

        //grabing the graph object
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();

        //plot points on the graph
        showGraph(pointNum);

        //find correlation
        gameplayManager.updateCorrelation(x, y);
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

    //method setting location for each point on the graph (takes in int - number of desired points)
    public void showGraph(int num){
        //get the width and height of the graph to set ranges of location
        int graphHeight = (int)graphContainer.sizeDelta.y;
        int graphWidth = (int)graphContainer.sizeDelta.x;

        //loop through the number of desired points
        for (int i = 0; i < num; i++){
            //set a random location for each point
            int xPosition = Random.Range(2, graphWidth - 2);
            int yPosition = Random.Range(2, graphHeight - 2);

            //create each point with new random location
            CreatePoints(new Vector2(xPosition, yPosition));
            
            //add points to list
            x.Add(xPosition);
            y.Add(yPosition);
        }
    }

}
