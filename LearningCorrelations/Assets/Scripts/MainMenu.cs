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
        DataController.Instance.participantID = participantID.text;

        //set condition
        int condition = Random.Range(0, 2);
        DataController.Instance.setCondition(condition);

        //create trial sequence
        DataController.Instance.createTrialSequence();

        //Load next scene
        SceneManager.LoadScene(3);
    }

    public void LeaveInstructions()
    {
        SceneManager.LoadScene(1);
    }
}
