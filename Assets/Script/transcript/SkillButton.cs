using UnityEngine;
using System.Collections;

public class SkillButton : MonoBehaviour {

    public PosType posType = PosType.Basic;
    public float coldTime = 4;
    private float coldTimer = 0;// 表示剩余的冷却时间
    private UISprite maskSprite;
    private PlayerAnimation playerAnimation;
    private UIButton btn;

    void Start() {
        playerAnimation =  TranscriptManager._instance.player.GetComponent<PlayerAnimation>();
        if (transform.Find("Mask")) {
            maskSprite = transform.Find("Mask").GetComponent<UISprite>();
        }
        btn = this.GetComponent<UIButton>();
    }

    void Update() {
        if (maskSprite == null) return;

        if (coldTimer > 0) {
            coldTimer -= Time.deltaTime;
            maskSprite.fillAmount = coldTimer / coldTime;
            if (coldTimer <= 0) {
                Enable();
            }
        } else {
            maskSprite.fillAmount = 0;
        }
    }


    void OnPress(bool isPress) {
        playerAnimation.OnAttackButtonClick(isPress, posType);
        if (isPress && maskSprite!=null ) {
            coldTimer = coldTime;
            Disable();
        }
    }

    void Disable() {
        this.collider.enabled = false;
        btn.SetState(UIButtonColor.State.Normal, true);
    }
    void Enable() {
        this.collider.enabled = true;
    }

}
