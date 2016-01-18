using UnityEngine;
using System.Collections;

public class PlayerBar : MonoBehaviour {

    private UISprite headSprite;
    private UILabel nameLabel;
    private UILabel levelLabel;
    private UISlider energySlider;
    private UILabel energyLabel;
    private UISlider toughenSlider;
    private UILabel toughenLabel;
    private UIButton energyPlusButton;
    private UIButton toughenPlusButton;

    private UIButton headButton;

    void Awake() {
        headSprite = transform.Find("HeadSprite").GetComponent<UISprite>();
        nameLabel = transform.Find("NameLabel").GetComponent<UILabel>();
        levelLabel = transform.Find("LevelLabel").GetComponent<UILabel>();
        energySlider = transform.Find("EnergyProgressBar").GetComponent<UISlider>();
        energyLabel = transform.Find("EnergyProgressBar/Label").GetComponent<UILabel>();
        toughenSlider = transform.Find("ToughenProgressBar").GetComponent<UISlider>();
        toughenLabel = transform.Find("ToughenProgressBar/Label").GetComponent<UILabel>();
        energyPlusButton = transform.Find("EnergyPlusButton").GetComponent<UIButton>();
        toughenPlusButton = transform.Find("ToughenPlusButton").GetComponent<UIButton>();
        headButton = transform.Find("HeadButton").GetComponent<UIButton>();
        PlayerInfo._instance.OnPlayerInfoChanged += this.OnPlayerInfoChanged;

        EventDelegate ed = new EventDelegate(this, "OnHeadButtonClick");
        headButton.onClick.Add(ed);
    }

    void OnDestroy() {
        PlayerInfo._instance.OnPlayerInfoChanged -= this.OnPlayerInfoChanged;
    }
    //当我们的主角信息发生改变的时候 会触发这个方法
    void OnPlayerInfoChanged(InfoType type) {
        if (type==InfoType.All|| type == InfoType.Name || type == InfoType.HeadPortrait || type == InfoType.Level || type == InfoType.Energy || type == InfoType.Toughen) {
            UpdateShow();
        }
    }

    //更新显示
    void UpdateShow() {
        PlayerInfo info = PlayerInfo._instance;
        headSprite.spriteName = info.HeadPortrait;
        levelLabel.text = info.Level.ToString();
        nameLabel.text = info.Name.ToString();
        energySlider.value = info.Energy / 100f;
        energyLabel.text = info.Energy + "/100";
        toughenSlider.value = info.Toughen / 50f;
        toughenLabel.text = info.Toughen + "/50";
    }

    public void OnHeadButtonClick() {
        PlayerStatus._instance.Show();
    }
	
}
