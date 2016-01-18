using UnityEngine;
using System.Collections;

public class SkillItemUI : MonoBehaviour {

    public PosType posType;
    public bool isSelect = false;
    private Skill skill;
    private UISprite sprite;
    private UIButton button;
    private UISprite Sprite {
        get {
            if (sprite == null) {
                sprite = this.GetComponent<UISprite>();
            }
            return sprite;
        }
    }
    private UIButton Button {
        get {
            if (button == null)
                button = this.GetComponent<UIButton>();
            return button;
        }
    }
    void Start()
    {
        SkillManager._instance.OnSyncSkillComplete += this.OnSyncSkillComplete;
    }

    public void OnSyncSkillComplete()
    {
        UpdateShow();
        if (isSelect) {
            OnClick();
        }
    }
    void UpdateShow() {
        skill = SkillManager._instance.GetSkillByPosition(posType);

        Sprite.spriteName = skill.Icon;
        Button.normalSprite = skill.Icon;
    }

    void OnClick() {
            transform.parent.parent.SendMessage("OnSkillClick", skill);
    }

    void OnDestroy()
    {
        if (SkillManager._instance != null)
        {
            SkillManager._instance.OnSyncSkillComplete -= this.OnSyncSkillComplete;
        }
    }

}
