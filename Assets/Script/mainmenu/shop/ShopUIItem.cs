using UnityEngine;
using System.Collections;

public class ShopUIItem : MonoBehaviour
{
    public int diamondChangeCount=0;
    public int coinChangeCount=0;

    public void OnBuyButtonClick()
    {
        ShopUI.Instance.OnBuy(coinChangeCount,diamondChangeCount);
    }
}
