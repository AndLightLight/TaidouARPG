﻿using UnityEngine;
using System.Collections;

public class EquipPopup : MonoBehaviour {
    public PowerShow powerShow;

    private InventoryItem it;
    private InventoryItemUI itUI;
    private KnapsackRoleEquip roleEquip;

    private UISprite equipSprite;
    private UILabel nameLabel;
    private UILabel qualityLabel;
    private UILabel damageLabel;
    private UILabel hpLabel;
    private UILabel powerLabel;
    private UILabel desLabel;
    private UILabel levelLabel;
    private UILabel btnLabel;
    private UIButton closeButton;
    private UIButton equipButton;
    private UIButton upgradeButton;

    private bool isLeft = true;
    void Awake() {
        equipSprite = transform.Find("EquipBg/Sprite").GetComponent<UISprite>();
        nameLabel = transform.Find("NameLabel").GetComponent<UILabel>();
        qualityLabel = transform.Find("QualityLabel/Label").GetComponent<UILabel>();
        damageLabel = transform.Find("DamageLabel/Label").GetComponent<UILabel>();
        hpLabel = transform.Find("HpLabel/Label").GetComponent<UILabel>();
        powerLabel = transform.Find("PowerLabel/Label").GetComponent<UILabel>();
        desLabel = transform.Find("DesLabel").GetComponent<UILabel>();
        levelLabel = transform.Find("LevelLabel/Label").GetComponent<UILabel>();
        btnLabel = transform.Find("EquipButton/Label").GetComponent<UILabel>();
        closeButton = transform.Find("CloseButton").GetComponent<UIButton>();
        equipButton = transform.Find("EquipButton").GetComponent<UIButton>();
        upgradeButton = transform.Find("UpgradeButton").GetComponent<UIButton>();

        EventDelegate ed1 = new EventDelegate(this, "OnClose");
        closeButton.onClick.Add(ed1);

        EventDelegate ed2 = new EventDelegate(this, "OnEquip");
        equipButton.onClick.Add(ed2);

        EventDelegate ed3 = new EventDelegate(this, "OnUpgrade");
        upgradeButton.onClick.Add(ed3);
    }
    public void Show(InventoryItem it, InventoryItemUI itUI,KnapsackRoleEquip roleEquip,  bool isLeft = true) {
        gameObject.SetActive(true);
        this.it = it;
        this.itUI = itUI;
        this.roleEquip = roleEquip;
        Vector3 pos = transform.localPosition;
        this.isLeft = isLeft;
        if (isLeft) {
            transform.localPosition = new Vector3(-Mathf.Abs(pos.x), pos.y, pos.z);
            btnLabel.text = "装备";
        } else {
            transform.localPosition = new Vector3(Mathf.Abs(pos.x), pos.y, pos.z);
            btnLabel.text = "卸下";
        }
        equipSprite.spriteName = it.Inventory.ICON;
        nameLabel.text = it.Inventory.Name;
        qualityLabel.text = it.Inventory.Quality.ToString();
        damageLabel.text = it.Inventory.Damage.ToString();
        hpLabel.text = it.Inventory.HP.ToString();
        powerLabel.text = it.Inventory.Power.ToString();
        desLabel.text = it.Inventory.Des;
        levelLabel.text = it.Level.ToString();
    }

    public void OnClose() {
        Close();
        transform.parent.SendMessage("DisableButton");
    }
    public void Close() {
        ClearObject();
        gameObject.SetActive(false);
    }
    //点击卸下按钮和装备按钮的时候触发
    public void OnEquip() {
        int startValue = PlayerInfo._instance.GetOverallPower();
        if (isLeft) {//从背包装备到身上
            itUI.Clear();//清空该装备所在的格子
            PlayerInfo._instance.DressOn(it);
        } else {//从身上脱下
            roleEquip.Clear();//把身上的装备清空
            PlayerInfo._instance.DressOff( it);
        }
        int endValue = PlayerInfo._instance.GetOverallPower();
        powerShow.ShowPowerChange(startValue, endValue);

        InventoryUI._instance.SendMessage("UpdateCount");
        OnClose();
    }
    //点击了升级按钮
    public void OnUpgrade() {
        int coinNeed = (it.Level + 1) * it.Inventory.Price;
        bool isSuccess = PlayerInfo._instance.GetCoin(coinNeed);
        if (isSuccess) {
            it.Level += 1;
            levelLabel.text = it.Level.ToString();
            InventoryManager._instance.UpgradeEquip(it);
        } else {
            //给出提示信息
            MessageManager._instance.ShowMessage("金币不足，无法升级");
        }
    }

    void ClearObject() {
        it = null;
        itUI = null;
    }

}
