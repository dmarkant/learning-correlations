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
        //display end information
        idTxt.text = "ID: " + PlayerPrefs.GetString("participantID");
        scoreTxt.text = "Score: " + PlayerPrefs.GetInt("pScore").ToString();  
        
        path = "Assets/Files/" + PlayerPrefs.GetString("participantID") + ".csv";

        saveData(path, PlayerPrefs.GetString("participantID"), PlayerPrefs.GetInt("pScore").ToString());
    }

    //test data saving
    public static void saveData(string path, string pID, string pscore) {
        using (System.IO.StreamWriter file = new System.IO.StreamWriter(path, true))
        {
            file.WriteLine(pID + "," + pscore);
        }
    }
}
