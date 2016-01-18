using UnityEngine;
using System.Collections;

public class LoadSceneProgressBar : MonoBehaviour {

    public static LoadSceneProgressBar _instance;

    private GameObject bg;
    private UISlider progressBar;
    private bool isAsyn = false;
    private AsyncOperation ao = null;

    void Awake() {
        _instance = this;
        bg = transform.Find("Bg").gameObject;
        progressBar = transform.Find("Bg/ProgressBar").GetComponent<UISlider>();
        gameObject.SetActive(false);
    }

    void Update() {
        if (isAsyn) {
            progressBar.value = ao.progress;
        }
    }

    public void Show(AsyncOperation ao ) {
        gameObject.SetActive(true);
        bg.SetActive(true);
        isAsyn = true;
        this.ao = ao;
    }


}
