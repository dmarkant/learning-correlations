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
    [SerializeField] private Text randEndNum;
    [SerializeField] private Text bonusAmnt;
    private int randNum;
    //string path;

    // Start is called before the first frame update
    void Start() {
        //variables
        string id = DataController.Instance.participantID;
        int score = DataController.Instance.finalScore;
        randNum = Random.Range(1000, 2000);
        DataController.Instance.recordEndingNum(randNum.ToString());

        //display end information
        idTxt.text = "ID: " + id;

        scoreTxt.text = "Score: " + score;

        //calculate bonus ammount
        if (score <= 50) {

        }
        else if (score > 50 && score <= 100) {
            bonusAmnt.text = "$0";
        }
        else if (score > 50 && score <= 100) {
            bonusAmnt.text = "$1";
        }
        else if (score > 100 && score <= 150) {
            bonusAmnt.text = "$2";
        }
        else if (score > 150 && score <= 200) {
            bonusAmnt.text = "$3";
        }
        else if (score > 200) {
            bonusAmnt.text = "$4";
        }

        //display random end number
        randEndNum.text = "Number for Qualtrics: " + randNum;
    }
}
