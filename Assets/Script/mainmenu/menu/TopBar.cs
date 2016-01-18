using UnityEngine;
using System.Collections;

public class TopBar : MonoBehaviour {

    private UILabel coinLabel;
    private UILabel diamondLabel;
    private UIButton coinPlusButton;
    private UIButton diamondPlusButton;

    void Awake() {
        coinLabel = transform.Find("CoinBg/Label").GetComponent<UILabel>();
        diamondLabel = transform.Find("DiamondBg/Label").GetComponent<UILabel>();
        coinPlusButton = transform.Find("CoinBg/PlusButton").GetComponent<UIButton>();
        diamondPlusButton = transform.Find("DiamondBg/PlusButton").GetComponent<UIButton>();
        PlayerInfo._instance.OnPlayerInfoChanged += this.OnPlayerInfoChanged;
    }

    void OnDestroy() {
        PlayerInfo._instance.OnPlayerInfoChanged -= this.OnPlayerInfoChanged;
    }

    void OnPlayerInfoChanged(InfoType type) {
        if (type == InfoType.All || type == InfoType.Coin || type == InfoType.Diamond) {
            UpdateShow();
        }
    }

    void UpdateShow() {
        PlayerInfo info = PlayerInfo._instance;

        coinLabel.text = info.Coin.ToString();
        diamondLabel.text = info.Diamond.ToString();
    }
	
}
