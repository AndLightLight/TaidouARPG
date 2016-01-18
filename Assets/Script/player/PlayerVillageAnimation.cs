using UnityEngine;
using System.Collections;

public class PlayerVillageAnimation : MonoBehaviour {

    private Animator anim;
	// Use this for initialization
	void Start () {
        anim = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (rigidbody.velocity.magnitude > 0.5f) {
            anim.SetBool("Move", true);
        } else {
            anim.SetBool("Move", false);
        }
	}
}
