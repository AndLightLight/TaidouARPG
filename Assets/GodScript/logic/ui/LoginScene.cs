using UnityEngine;
using System.IO;
using System.Collections;

public class LoginScene : MonoBehaviour
{
    void Awake()
    {
        if (null != UIControl.Instance.UIRootObj)
        {
            GameObject.DestroyImmediate(UIControl.Instance.UIRootObj);
        }
        MainScene.bInit = false;
    }

    // Use this for initialization
	void Start ()
    {
        SceneManager.Instance.mSceneType = GameDefines.SceneType.ST_LOGIN;

        // UI Init
		UIControl.Instance.Init(UIControl.UIType.Login);
		
		ReYun.Instance.Game_Event_CheckPoint(@"登录场景加载成功");
	}
}
