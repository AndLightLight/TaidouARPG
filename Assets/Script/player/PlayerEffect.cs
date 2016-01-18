using UnityEngine;

using System.Collections;

public class PlayerEffect : MonoBehaviour {

    private Renderer[] rendererArray;
    private NcCurveAnimation[] curveAnimArray;
    private GameObject effectOffset;
        

	// Use this for initialization
	void Start () {
        rendererArray = this.GetComponentsInChildren<Renderer>();
        curveAnimArray = this.GetComponentsInChildren<NcCurveAnimation>();
        if(transform.Find("EffectOffset")!=null)
            effectOffset = transform.Find("EffectOffset").gameObject;
	}

    void Update() {
        // For test
        //if (Input.GetMouseButtonDown(0)) {
        //    Show();
        //}
    }

    public void Show() {
        if (effectOffset != null) {
            effectOffset.SetActive(false);
            effectOffset.SetActive(true);
        } else {
            foreach (Renderer renderer in rendererArray) {
                renderer.enabled = true;
            }
            foreach (NcCurveAnimation anim in curveAnimArray) {
                anim.ResetAnimation();
            }
        }
    }


}
