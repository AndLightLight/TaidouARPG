using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TaidouCommon.Model;

public class InventoryManager : MonoBehaviour {
    public static InventoryManager _instance;
    public TextAsset listinfo;
    public Dictionary<int, Inventory> inventoryDict = new Dictionary<int, Inventory>();
    //public Dictionary<int, InventoryItem> inventoryItemDict = new Dictionary<int, InventoryItem>();
    public List<InventoryItem> inventoryItemList = new List<InventoryItem>();

    public delegate void OnInventoryChangeEvent();
    public event OnInventoryChangeEvent OnInventoryChange;

    private InventoryItemDBController inventoryItemDBController;


    void Awake() {
        _instance = this;
        inventoryItemDBController = this.GetComponent<InventoryItemDBController>();
        inventoryItemDBController.OnGetInventoryItemDBList += this.OnGetInventoryItemDBList;
        inventoryItemDBController.OnAddInventoryItemDB += this.OnAddInventoryItemDB;
        ReadInventoryInfo();
    }
    void Start() {
        ReadInventoryItemInfo();
    }

    void Update()
    {
        PickUp();
    }

    void ReadInventoryInfo() {
        string str = listinfo.ToString();
        string[] itemStrArray = str.Split('\n');
        foreach (string itemStr in itemStrArray) {
            //ID 名称 图标 类型（Equip，Drug） 装备类型(Helm,Cloth,Weapon,Shoes,Necklace,Bracelet,Ring,Wing) 售价 星级 品质 伤害 生命 战斗力 作用类型 作用值 描述
            string[] proArray = itemStr.Split('|');
            Inventory inventory = new Inventory();
            inventory.ID = int.Parse(proArray[0]);
            inventory.Name = proArray[1];
            inventory.ICON = proArray[2];
            switch (proArray[3]) {
                case "Equip":
                    inventory.InventoryTYPE = InventoryType.Equip;
                    break;
                case "Drug":
                    inventory.InventoryTYPE = InventoryType.Drug;
                    break;
                case "Box":
                    inventory.InventoryTYPE = InventoryType.Box;
                    break;
            }
            if (inventory.InventoryTYPE == InventoryType.Equip) {
                switch (proArray[4]) {
                    case "Helm":
                        inventory.EquipTYPE = EquipType.Helm;
                        break;
                    case "Cloth":
                        inventory.EquipTYPE = EquipType.Cloth;
                        break;
                    case "Weapon":
                        inventory.EquipTYPE = EquipType.Weapon;
                        break;
                    case "Shoes":
                        inventory.EquipTYPE = EquipType.Shoes;
                        break;
                    case "Necklace":
                        inventory.EquipTYPE = EquipType.Necklace;
                        break;
                    case "Bracelet":
                        inventory.EquipTYPE = EquipType.Bracelet;
                        break;
                    case "Ring":
                        inventory.EquipTYPE = EquipType.Ring;
                        break;
                    case "Wing":
                        inventory.EquipTYPE = EquipType.Wing;
                        break;
                }

            }
            //print(itemStr);
            //售价 星级 品质 伤害 生命 战斗力 作用类型 作用值 描述
            inventory.Price = int.Parse( proArray[5] );
            if (inventory.InventoryTYPE == InventoryType.Equip) {
                inventory.StarLevel = int.Parse(proArray[6]);
                inventory.Quality = int.Parse(proArray[7]);
                inventory.Damage = int.Parse(proArray[8]);
                inventory.HP = int.Parse(proArray[9]);
                inventory.Power = int.Parse(proArray[10]);
            }
            if (inventory.InventoryTYPE == InventoryType.Drug) {
                inventory.ApplyValue = int.Parse(proArray[12]);
            }
            inventory.Des = proArray[13];
            inventoryDict.Add(inventory.ID, inventory);
        }
    }
    //完成角色背包信息的初始化，获得拥有的物品
    void ReadInventoryItemInfo() {
        //随机生成主角拥有的物品
        //for (int j = 0; j < 20;j++ ) {
        //    int id = Random.Range(1001, 1020);
        //    Inventory i = null;
        //    inventoryDict.TryGetValue(id, out i);
        //    if (i.InventoryTYPE == InventoryType.Equip) {
        //        InventoryItem it = new InventoryItem();
        //        it.Inventory = i;
        //        it.Level = Random.Range(1, 10);
        //        it.Count = 1;
        //        inventoryItemList.Add( it);
        //    } else {
        //        //先判断背包里面是否已经存在
        //        InventoryItem it = null;
        //        bool isExit = false;
        //        foreach (InventoryItem temp in inventoryItemList) {
        //            if (temp.Inventory.ID == id) {
        //                isExit = true;
        //                it = temp;
        //                break;
        //            }
        //        }
        //        if (isExit) {
        //            it.Count++;
        //        } else {
        //            it = new InventoryItem();
        //            it.Inventory = i;
        //            it.Count = 1;
        //            inventoryItemList.Add( it);
        //        }
        //    }
        //}
        inventoryItemDBController.GetInventoryItemDB();
    }

    void PickUp()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            int id = Random.Range(1001, 1020);
            Inventory i = null;
            inventoryDict.TryGetValue(id, out i);
            if (i.InventoryTYPE == InventoryType.Equip)
            {
                //InventoryItem it = new InventoryItem();
                //it.Inventory = i;
                //it.Level = Random.Range(1, 10);
                //it.Count = 1;
                //inventoryItemList.Add(it);
                //InventoryItemDB itemDB = it.CreateInventoryItemDB();
                InventoryItemDB itemDB = new InventoryItemDB();
                itemDB.InventoryID = id;
                itemDB.Count = 1;
                itemDB.IsDressed = false;
                itemDB.Level = Random.Range(1, 10);
                inventoryItemDBController.AddInventoryItemDB(itemDB);
            }
            else
            {
                //先判断背包里面是否已经存在
                InventoryItem it = null;
                bool isExit = false;
                foreach (InventoryItem temp in inventoryItemList) {
                    if (temp.Inventory.ID == id) {
                        isExit = true;
                        it = temp;
                        break;
                    }
                }
                if (isExit)
                {
                    it.Count++;
                    //同步inventoryitemdb 进行update
                    inventoryItemDBController.UpdateInventoryItemDB(it.InventoryItemDB);
                }
                else
                {
                    InventoryItemDB itemDB = new InventoryItemDB();
                    itemDB.InventoryID = id;
                    itemDB.Count = 1;
                    itemDB.IsDressed = false;
                    itemDB.Level = Random.Range(1, 10);
                    inventoryItemDBController.AddInventoryItemDB(itemDB);
                }
            }
        }
    }

    public void OnAddInventoryItemDB(InventoryItemDB itemDB)
    {
        InventoryItem it = new InventoryItem(itemDB);
        inventoryItemList.Add(it);

        OnInventoryChange();
    }

    public void OnGetInventoryItemDBList( List<InventoryItemDB> list  )
    {
        foreach (var itemDB in list)
        {
            InventoryItem it = new InventoryItem(itemDB);
            inventoryItemList.Add(it);
        }
        OnInventoryChange();
    }

    public void RemoveInventoryItem( InventoryItem it ) {
        this.inventoryItemList.Remove(it);
    }

    public void UpgradeEquip( InventoryItem it )
    {
        inventoryItemDBController.UpgradeEquip(it.InventoryItemDB);
    }

    void OnDestroy()
    {
        if (inventoryItemDBController != null)
        {
            inventoryItemDBController.OnGetInventoryItemDBList -= this.OnGetInventoryItemDBList;
        }
    }
}
