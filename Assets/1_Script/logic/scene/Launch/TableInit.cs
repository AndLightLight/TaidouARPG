
using UnityEngine;
using System.Collections;


public class TableInit : MonoBehaviour
{
    bool _inited = false;

	void Start ()
    {
        StartCoroutine(TemplatePool.Instance.Build());
	}
	

	void Update ()
    {
		if (!_inited && TemplatePool.Instance.HasBuilt)
        {
            _inited = true;
			LogManager.Log("TableInit..........OK");
            LaunchControl.NextSection(this);
        }
	}
}
