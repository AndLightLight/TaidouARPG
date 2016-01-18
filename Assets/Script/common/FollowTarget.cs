using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour {

    public Vector3 offset;
    private Transform playerBip;
    public float smoothing = 1;

	// Use this for initialization
	void Start () {
	    if (TranscriptManager._instance != null)
	    {
	        playerBip = TranscriptManager._instance.player.transform.Find("Bip01");
	    }
	    else
	    {
	        playerBip = GameObject.FindGameObjectWithTag("Player").transform.Find("Bip01");
	    }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //transform.position = playerBip.position + offset;
        Vector3 targetPos = playerBip.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothing * Time.deltaTime);
	}
}
