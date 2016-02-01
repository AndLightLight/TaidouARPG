using UnityEngine;
using System.Collections;


public class LoadLoginScene : MonoBehaviour
{
    WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
    
    void Start()
    {
        //StartCoroutine(LoadingScene());
    }

   /* IEnumerator LoadingScene()
    {
        LoginScene loginscene = SceneManager.Instance.GetComponent<LoginScene>();
        MainScene mainscene = SceneManager.Instance.GetComponent<MainScene>();
        if (null != loginscene)
            GameObject.DestroyImmediate(loginscene);
        if (null != mainscene)
            GameObject.DestroyImmediate(mainscene);

        SceneManager.Instance.gameObject.AddComponent<LoginScene>();

        WWW www = new WWW(ResourceManager.RawPath + "Scene/GJ_Scene_login.unity3d");
        yield return www;

        if (!string.IsNullOrEmpty(www.error))
        {
            LogManager.Log(www.error, LogType.Error);
            yield break;
        }
        AssetBundle bundle = www.assetBundle;
        LoginScene.sceneBundle = bundle;
        AsyncOperation async = Application.LoadLevelAsync(@"GJ_Scene_login");
        yield return async;

    }*/
}
    
