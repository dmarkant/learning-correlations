using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    [SerializeField] private Text scoreTxt;
    [SerializeField] private Text idTxt;

    // Start is called before the first frame update
    void Start()
    {
        //display end information
        idTxt.text = "ID: " + PlayerPrefs.GetString("participantID");
        scoreTxt.text = "Score: " + PlayerPrefs.GetInt("pScore").ToString();   
    }
}
