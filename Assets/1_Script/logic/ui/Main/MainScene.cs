using UnityEngine;
using System.IO;
using System.Collections;

public class MainScene : MonoBehaviour
{
    void Awake()
    {
       /* if (null != UIControl.Instance.UIRootObj)
        {
            GameObject.DestroyImmediate(UIControl.Instance.UIRootObj);
        }*/
    }

    // Use this for initialization
	void Start ()
    {
		UIControl.Instance.Init(UIControl.UIType.Main);
	}
}
