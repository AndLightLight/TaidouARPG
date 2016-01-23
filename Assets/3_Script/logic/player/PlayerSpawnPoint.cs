using UnityEngine;
using System.Collections;

public class PlayerSpawnPoint : MonoBehaviour
{

	void Start ()
    {
        if (null == LocalPlayerControl.Instance)
        {
            Transform tansform = this.GetComponent<Transform>();
            LocalPlayerControl.Instance = ObjectFactory.CreateLocalPlayer(111, tansform.position.x, tansform.position.y, tansform.position.z);
            //Transform playerTransform = LocalPlayerControl.Instance.GetComponent<Transform>();
            //playerTransform.SetParent(tansform);
        }
	}
	
	void Update ()
    {
	
	}
}
