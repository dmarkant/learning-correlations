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
    public int pointNum = 10;

    //when graph component starts up
    void Awake(){
        //grabing the graph object
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
        //plot points on the graph
        showGraph(pointNum);
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
    }

    //method setting location for each point on the graph (takes in int - number of desired points)
    public void showGraph(int num){
        //get the width and height of the graph to set ranges of location
        float graphHeight = graphContainer.sizeDelta.y;
        float graphWidth = graphContainer.sizeDelta.x;
        //loop through the number of desired points
        for (int i = 0; i < num; i++){
            //set a random location for each point
            float xPosition = Random.Range(0, graphWidth);
            float yPosition = Random.Range(0, graphHeight);
            //create each point with new random location
            CreatePoints(new Vector2(xPosition, yPosition));
        }
    }


    // Start is called before the first frame update
   // void Start(){
        
   // }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
