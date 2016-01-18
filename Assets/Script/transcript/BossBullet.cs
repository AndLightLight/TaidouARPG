using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossBullet : MonoBehaviour {

    public float moveSpeed = 3;
    public float Damage {
        set;
        private get;
    }
    public List<GameObject> playerList = new List<GameObject>();
    private float repeatRate = 1f;
    private float force = 1000;
	// Use this for initialization
	void Start () {
        InvokeRepeating("Attack", 0, repeatRate);
        Destroy(this.gameObject,5);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
	}

    void OnTriggerEnter( Collider col ) {
        if (col.tag == "Player") {
            if (playerList.IndexOf(col.gameObject)<0) {
                playerList.Add(col.gameObject);
            }
        }
    }
    void OnTriggerExit(Collider col) {
        if (col.tag == "Player") {
            if (playerList.IndexOf(col.gameObject) >= 0) {
                playerList.Remove(col.gameObject);
            }
        }
        
    }

    void Attack() {
        foreach (GameObject player in playerList) {
            player.SendMessage("TakeDamage",Damage*repeatRate  , SendMessageOptions.DontRequireReceiver);
            player.rigidbody.AddForce(transform.forward * force);

        }
    }
}
