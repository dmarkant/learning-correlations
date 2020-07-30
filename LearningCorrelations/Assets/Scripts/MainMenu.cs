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

        //set file path
        DataController.Instance.setPath("Assets/Files/" + participantID.text + ".csv");

        //initial save
        DataController.Instance.initSave();

        //create trial sequence
        DataController.Instance.createTrialSequence();

        //Load next scene
        SceneManager.LoadScene(4);
    }

    public void LeaveInstructions()
    {
        SceneManager.LoadScene(1);
    }

    public void toInstructions()
    {
        SceneManager.LoadScene(3);
    }

    public void nextTutoral()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;

        if (scene == 4)
        {
            SceneManager.LoadScene(5);
        }
        else if (scene == 5)
        {
            SceneManager.LoadScene(6);
        }
        else if (scene == 6)
        {
            SceneManager.LoadScene(7);
        }
    }
}
