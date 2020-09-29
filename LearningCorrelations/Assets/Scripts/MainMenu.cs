using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //reference for participant ID
    //public InputField participantID;
    public Text participantID;

    //references for text and buttons
    public Image tutimg1;
    public Image tutimg2;
    public Image tutimg3;
    public Image tutimg4;
    public Image tutimg5;
    public Image tutimg6;
    public Image instruct1;
    public Image instruct2;
    public Image instruct3;
    public Button backMain;
    public Button next;
    public Button backMainI;
    public Button nextI;
    public Button instruct;
    public Button play;
    

    public void PlayGame(){

        //participantID.text = Random.Range(1000, 2000).ToString();

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
            participantID.text = Random.Range(1000, 2000).ToString();
            play.interactable = true;
        }
    }


    public void toInstructions()
    {
        //go to the instruction scene
        int scene = SceneManager.GetActiveScene().buildIndex;

        if (scene == 0)
        {
            DataController.Instance.canPlay++;
            SceneManager.LoadScene(3);
        }
        else if (instruct1.enabled == true)
        {
            instruct1.enabled = false;
            instruct2.enabled = true;
        }
        else if (instruct2.enabled == true)
        {
            instruct2.enabled = false;
            instruct3.enabled = true;
            nextI.interactable = false;
            backMainI.interactable = true;
        }

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
        else if (tutimg1.enabled == true) {
            tutimg1.enabled = false;
            tutimg2.enabled = true;
        }
        else if (tutimg2.enabled == true) {
            tutimg2.enabled = false;
            tutimg3.enabled = true;
        }
        else if (tutimg3.enabled == true) {
            tutimg3.enabled = false;
            tutimg4.enabled = true;
        }
        else if (tutimg4.enabled == true)
        {
            tutimg4.enabled = false;
            tutimg5.enabled = true;
        }
        else if (tutimg5.enabled == true)
        {
            tutimg5.enabled = false;
            tutimg6.enabled = true;
            next.interactable = false;
            backMain.interactable = true;
            DataController.Instance.canPlay++;
        }
    }


}
