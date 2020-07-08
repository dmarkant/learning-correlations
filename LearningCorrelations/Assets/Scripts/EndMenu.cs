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
        string id = DataController.Instance.participantID;
        int score = DataController.Instance.finalScore;
        int cond = DataController.Instance.condition;
      //  int trials1 = DataController.Instance.r1Trials;
       // int trials2 = DataController.Instance.r2Trials;
        List<double> actualCorrs = DataController.Instance.actualCorr;
        List<double> guesses = DataController.Instance.userCorr;
        List<double> diffs = DataController.Instance.corrDiff;

        //display end information
        idTxt.text = "ID: " + id;

        scoreTxt.text = "Score: " + score;
        
       //set path and save the data
       path = "Assets/Files/" + id + ".csv";
        
        saveData(path, id, cond.ToString(), /*trials1.ToString(), trials2.ToString(),*/ score.ToString(), actualCorrs, guesses, diffs);
    }

    //test data saving
    public static void saveData(string path, string pID, string condition, /*string r1, string r2,*/ string pscore, List<double> corrs, List<double> user, List<double> diff) {
        using (System.IO.StreamWriter file = new System.IO.StreamWriter(path, true))
        {
            file.Write(pID + "," + condition + "," + /*r1 + "," + r2 + "," +*/ pscore + ",");
          
            for(int i = 0; i < corrs.Count; i++) {
                file.Write(corrs[i] + ",");
            }
            for (int i = 0; i < user.Count; i++) {
                file.Write(user[i] + ",");
            }
            for (int i = 0; i < diff.Count; i++) {
                file.Write(diff[i] + ",");
            }
        }
    }
}
