using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour {

    public static InventoryUI _instance;
    public List<InventoryItemUI> itemUIList = new List<InventoryItemUI>();//所有的物品格子
    private UIButton clearupButton;
    private UILabel inventoryLabel;

    private int count = 0;

    void Awake() {
        _instance = this;
        InventoryManager._instance.OnInventoryChange += this.OnInventoryChange;
        clearupButton = transform.Find("ButtonClearup").GetComponent<UIButton>();
        inventoryLabel = transform.Find("InventoryLabel").GetComponent<UILabel>();

        EventDelegate ed = new EventDelegate(this, "OnClearup");
        clearupButton.onClick.Add(ed);

    }

    void OnDestroy() {
        InventoryManager._instance.OnInventoryChange -= this.OnInventoryChange;
    }

    void OnInventoryChange() {
        UpdateShow();
    }

    void UpdateShow() {
        int temp = 0;
        for (int i = 0; i < InventoryManager._instance.inventoryItemList.Count; i++) {
            InventoryItem it = InventoryManager._instance.inventoryItemList[i];
            if (it.IsDressed == false) {
                itemUIList[temp].SetInventoryItem(it);
                temp++;
            } else {
                print(it);
            }
        }
        count = temp;
        for (int i = temp; i < itemUIList.Count; i++) {
            itemUIList[i].Clear();
        }
        inventoryLabel.text = count + "/32";
    }
    public void UpdateCount() {
        count = 0;
        foreach (InventoryItemUI itUI in itemUIList) {
            if (itUI.it != null) {
                count++;
            }
        }
        inventoryLabel.text = count + "/32";
    }

    public void AddInventoryItem( InventoryItem it ) {
        foreach (InventoryItemUI itUI in itemUIList) {
            if (itUI.it == null) {
                itUI.SetInventoryItem(it);
                count++;
                break;
            }
        }
        inventoryLabel.text = count + "/32";
    }
    //整理
    void OnClearup() {
        UpdateShow();
    }

}
