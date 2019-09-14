using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //reference for participant ID
    public InputField participantID;

    public void PlayGame(){
        //save ID 
        PlayerPrefs.SetString("participantID", participantID.text);

        //Load next scene
        SceneManager.LoadScene(1);
    }
}
