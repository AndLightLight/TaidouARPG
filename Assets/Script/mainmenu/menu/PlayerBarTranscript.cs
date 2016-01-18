using UnityEngine;
using System.Collections;

public class PlayerBarTranscript : MonoBehaviour {

    private UISprite headSprite;
    private UILabel nameLabel;
    private UILabel levelLabel;
    private UISlider hpSlider;
    private UILabel hpLabel;
    private UISlider energySlider;
    private UILabel energyLabel;
    private UIButton energyPlusButton;
    private UIButton toughenPlusButton;

    private UIButton headButton;

    void Awake() {
        headSprite = transform.Find("HeadSprite").GetComponent<UISprite>();
        nameLabel = transform.Find("NameLabel").GetComponent<UILabel>();
        levelLabel = transform.Find("LevelLabel").GetComponent<UILabel>();
        energySlider = transform.Find("EnergyProgressBar").GetComponent<UISlider>();
        energyLabel = transform.Find("EnergyProgressBar/Label").GetComponent<UILabel>();
        hpSlider = transform.Find("HpProgressBar").GetComponent<UISlider>();
        hpLabel = transform.Find("HpProgressBar/Label").GetComponent<UILabel>();
        energyPlusButton = transform.Find("EnergyPlusButton").GetComponent<UIButton>();
        toughenPlusButton = transform.Find("ToughenPlusButton").GetComponent<UIButton>();
        headButton = transform.Find("HeadButton").GetComponent<UIButton>();
        PlayerInfo._instance.OnPlayerInfoChanged += this.OnPlayerInfoChanged;
    }

    void Start()
    {
        UpdateShow();
        TranscriptManager._instance.player.GetComponent<PlayerAttack>().OnPlayerHpChange += this.OnPlayerHpChange;
    }

    void OnDestroy() {
        PlayerInfo._instance.OnPlayerInfoChanged -= this.OnPlayerInfoChanged;
        if (TranscriptManager._instance.player != null)
        {
            TranscriptManager._instance.player.GetComponent<PlayerAttack>().OnPlayerHpChange -= this.OnPlayerHpChange;
        }
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
        hpSlider.value = info.HP/info.HP;
        hpLabel.text = info.HP + "/"+info.HP;
    }

    void OnPlayerHpChange(int hp) {
        PlayerInfo info = PlayerInfo._instance;
        hpSlider.value = (float)hp / info.HP;
        hpLabel.text = hp+ "/" + info.HP;
    }

}
