using UnityEngine;
using System.Collections;

public class HpBarManager : MonoBehaviour {
    public static HpBarManager _instance;
    public GameObject hpBarPrefab;
    public GameObject hudTextPrefab;

    void Awake() {
        _instance = this;
    }

    public GameObject GetHpBar( GameObject target ) {
        GameObject go = NGUITools.AddChild(this.gameObject, hpBarPrefab);
        go.GetComponent<UIFollowTarget>().target = target.transform;
        return go;
    }
    public GameObject GetHudText(GameObject target) {
        GameObject go = NGUITools.AddChild(this.gameObject, hudTextPrefab);
        go.GetComponent<UIFollowTarget>().target = target.transform;
        return go;
    }

}
