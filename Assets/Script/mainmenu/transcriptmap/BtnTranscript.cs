using UnityEngine;
using System.Collections;

public class BtnTranscript : MonoBehaviour {

    public int id;
    public int needLevel;
    public int needEnergy = 3;
    public string sceneName;
    public string des = "这里是一个阴森恐怖的地方，你敢出来溜溜吗";


    public void OnClick() {
        transform.parent.SendMessage("OnBtnTranscriptClick", this);
    }
	
}
