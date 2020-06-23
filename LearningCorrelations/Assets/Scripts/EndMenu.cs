using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class EndMenu : MonoBehaviour
{
    [SerializeField] private Text scoreTxt;
    [SerializeField] private Text idTxt;
    string path;

    // Start is called before the first frame update
    void Start() {
        //variables
        string id = DataController.Instance.getID();

        //display end information
        idTxt.text = "ID: " + id;

        scoreTxt.text = "Score: " + PlayerPrefs.GetInt("pScore").ToString();  
        
       //set path and save the data
       path = "Assets/Files/" + id + ".csv";

        // saveData(path, PlayerPrefs.GetString("participantID"), PlayerPrefs.GetInt("condition").ToString(), PlayerPrefs.GetInt("r1").ToString(), 
        // PlayerPrefs.GetInt("r2").ToString(), PlayerPrefs.GetInt("pScore").ToString());

        saveData(path, id, PlayerPrefs.GetInt("condition").ToString(), PlayerPrefs.GetInt("r1").ToString(),
            PlayerPrefs.GetInt("r2").ToString(), PlayerPrefs.GetInt("pScore").ToString());
    }

    //test data saving
    public static void saveData(string path, string pID, string condition, string r1, string r2, string pscore) {
        using (System.IO.StreamWriter file = new System.IO.StreamWriter(path, true))
        {
            file.WriteLine(pID + "," + condition + "," + r1 + "," + r2 + "," + pscore);
        }
    }
}
