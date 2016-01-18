using UnityEngine;
using System.Collections;

public class SkillUI : MonoBehaviour {
    public static SkillUI _instance;
    private UILabel skillNameLabel;
    private UILabel skillDesLabel;
    private UIButton closeButton;
    private UIButton upgradeButton;
    private UILabel upgradeButtonLabel;
    private TweenPosition tween;
    private Skill skill;

    void Awake() {
        _instance = this;
        skillNameLabel = transform.Find("Bg/SkillNameLabel").GetComponent<UILabel>();
        skillDesLabel = transform.Find("Bg/DesLabel").GetComponent<UILabel>();
        closeButton = transform.Find("CloseButton").GetComponent<UIButton>();
        upgradeButton = transform.Find("UpgradeButton").GetComponent<UIButton>();
        upgradeButtonLabel = transform.Find("UpgradeButton/Label").GetComponent<UILabel>();
        tween = this.GetComponent<TweenPosition>();

        skillNameLabel.text = "";
        skillDesLabel.text = "";
        DisableUpgradeButton("选择技能");

        EventDelegate ed = new EventDelegate(this, "OnUpgrade");
        upgradeButton.onClick.Add(ed);

        EventDelegate ed1 = new EventDelegate(this, "OnClose");
        closeButton.onClick.Add(ed1);
    }

    void DisableUpgradeButton(string label="") {
        upgradeButton.SetState(UIButton.State.Disabled,true);
        upgradeButton.collider.enabled = false;
        if (label != "") {
            upgradeButtonLabel.text = label;
        }
    }
    void EnableUpgradeButton(string label = "") {
        upgradeButton.SetState(UIButton.State.Normal,true);
        upgradeButton.collider.enabled = true;
        if (label != "") {
            upgradeButtonLabel.text = label;
        }
    }

    void OnSkillClick(Skill skill) {
        this.skill = skill;
        PlayerInfo info = PlayerInfo._instance;
        if ((500 * (skill.Level + 1)) <= info.Coin) {
            if (skill.Level < info.Level) {
                EnableUpgradeButton("升级");
            } else {
                DisableUpgradeButton("最大等级");
            }
        } else {
            DisableUpgradeButton("金币不足");
        }
        skillNameLabel.text = skill.Name + " Lv." + skill.Level;
        skillDesLabel.text = "当前技能的攻击力为：" + (skill.Damage * skill.Level) + "  下一级技能的攻击力为：" + (skill.Damage * (skill.Level + 1)) + "  升级所需要的金币数：" + (500 * (skill.Level + 1));
    }

    void OnUpgrade() {
        PlayerInfo info = PlayerInfo._instance;
        if (skill.Level < info.Level) {
            int coinNeed = (500 * (skill.Level + 1));
            bool isSuccess = info.GetCoin(coinNeed);
            if (isSuccess) {
                skill.Upgrade();
                //同步到数据库 skilldb role
                SkillManager._instance.Upgrade(skill);
                OnSkillClick(skill);
            } else {
                DisableUpgradeButton("金币不足");
            }
        } else {
            DisableUpgradeButton("最大等级");
        }

    }
    public void Show() {
        tween.PlayForward();
    }
    public void Hide() {
        tween.PlayReverse();
    }
    void OnClose() {
        tween.PlayReverse();
    }
}
