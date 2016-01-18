using UnityEngine;
using System.Collections;

public class TranscriptMapDialog : MonoBehaviour {

    private UILabel desLabel;
    private UILabel energyTagLabel;
    private UILabel energyLabel;
    private UIButton enterButton;
    private UIButton closeButton;
    private TweenScale tween;

    void Start() {
        desLabel = transform.Find("Sprite/DesLabel").GetComponent<UILabel>();
        energyTagLabel = transform.Find("Sprite/EnergyTagLabel").GetComponent<UILabel>();
        energyLabel = transform.Find("Sprite/EnergyLabel").GetComponent<UILabel>();

        closeButton = transform.Find("BtnClose").GetComponent<UIButton>();
        tween = this.GetComponent<TweenScale>();

        EventDelegate ed2 = new EventDelegate(this, "OnClose");
        closeButton.onClick.Add(ed2);
    }

    public void ShowWarn() {
        energyLabel.enabled = false;
        energyTagLabel.enabled = false;
        enterButton.enabled = false;

        desLabel.text = "当前等级无法进入该地下城";
        tween.PlayForward();
    }
    public void ShowDialog(BtnTranscript transcript) {
        energyLabel.enabled = true;
        energyTagLabel.enabled = true;

        desLabel.text = transcript.des;
        energyLabel.text = transcript.needEnergy.ToString();
        tween.PlayForward();
    }

    public void HideDialog()
    {
        tween.PlayReverse();
    }

    void OnClose() {
        tween.PlayReverse();
    }

}
