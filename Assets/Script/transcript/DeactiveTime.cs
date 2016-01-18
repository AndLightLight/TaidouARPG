using UnityEngine;
using System.Collections;

public class DeactiveTime : MonoBehaviour {
    public float deactiveTime = 1;
    private float timer = 0;

	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > deactiveTime) {
            this.gameObject.SetActive(false);
            timer = 0;
        }
	}
}
