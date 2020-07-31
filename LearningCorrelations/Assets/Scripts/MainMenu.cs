using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //reference for participant ID
    public InputField participantID;

    //references for text and buttons
    public Text tuttxt1;
    public Text tuttxt2;
    public Text tuttxt3;
    public Text tuttxt4;
    public Button backMain;
    public Button next;
    public Button instruct;
    public Button play;
    

    public void PlayGame(){
        //ensure id has been entered
        if (participantID.text != "") {
            //save ID 
            DataController.Instance.participantID = participantID.text;

            //set condition
            int condition = Random.Range(0, 2);
            DataController.Instance.setCondition(condition);

            //set file path
            DataController.Instance.setPath("Assets/Files/" + participantID.text + ".csv");

            //initial save
            DataController.Instance.initSave();

            //create trial sequence
            DataController.Instance.createTrialSequence();

            //Load next scene
            SceneManager.LoadScene(1);
        }
    }

    void Awake()
    {
        //check if the tutorial and instructions have been viewed
        if (DataController.Instance.canPlay >= 2)
        {
            play.interactable = true;
        }
    }

    public void toInstructions()
    {
        //go to the instruction scene
        DataController.Instance.canPlay++;
        SceneManager.LoadScene(3); 
    }

    public void toMain ()
    {
        //go back to the main menu
        SceneManager.LoadScene(0);
    }

    public void nextTutoral()
    {
        //go to tutorial 
        int scene = SceneManager.GetActiveScene().buildIndex;

        if (scene == 0){
            SceneManager.LoadScene(4);
        }
        //cycle through the text objects
        else if (tuttxt1.enabled == true) {
            tuttxt1.enabled = false;
            tuttxt2.enabled = true;
        }
        else if (tuttxt2.enabled == true) {
            tuttxt2.enabled = false;
            tuttxt3.enabled = true;
        }
        else if (tuttxt3.enabled == true) {
            tuttxt3.enabled = false;
            tuttxt4.enabled = true;
            next.interactable = false;
            backMain.interactable = true;
            DataController.Instance.canPlay++;
        }
    }
}
