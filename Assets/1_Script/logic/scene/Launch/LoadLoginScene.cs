using UnityEngine;
using System.Collections;


public class LoadLoginScene : MonoBehaviour
{
	WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

	void Start()
	{
		StartCoroutine(LoadingScene());
	}

	IEnumerator LoadingScene()
	{
		/*LoginScene loginscene = SceneManager.Instance.GetComponent<LoginScene>();
		MainScene mainscene = SceneManager.Instance.GetComponent<MainScene>();
		if (null != loginscene)
			GameObject.DestroyImmediate(loginscene);
		if (null != mainscene)
			GameObject.DestroyImmediate(mainscene);*/

		//SceneManager.Instance.gameObject.AddComponent<LoginScene>();

		string RawPath =
#if UNITY_ANDROID && !UNITY_EDITOR
		    "jar:file://" + Application.dataPath + "!/assets/";
#elif UNITY_IOS && !UNITY_EDITOR
		    "file://" + Application.dataPath + "/Raw/";
#elif UNITY_STANDALONE_WIN
 "file://" + Application.dataPath + "/Resources/";
#else
            "file://" + Application.dataPath + "/Resources/";
#endif

		WWW www = new WWW(RawPath + "Scene/Test.unity");
		yield return www;

		if (!string.IsNullOrEmpty(www.error))
		{
			LogManager.Log(www.error, LogType.Error);
			yield break;
		}
		AssetBundle bundle = www.assetBundle;
		//LoginScene.sceneBundle = bundle;
		AsyncOperation async = Application.LoadLevelAsync(@"Test");
		yield return async;

	}
}
    
