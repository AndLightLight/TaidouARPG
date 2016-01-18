
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PanelManager
{
    private Dictionary<string, PanelBase> panelDic = null;
	private Dictionary<string, PanelBase> secendLevelPanelDic = null;
	
	private Dictionary<string, PanelBase> deleteDic = null;
    protected const string uiCameraPath = "/UI Root/Camera";

	public virtual void Init(UIControl.SecendLevelUIType st = UIControl.SecendLevelUIType.None)
	{
        panelDic = new Dictionary<string, PanelBase>();
		secendLevelPanelDic = new Dictionary<string, PanelBase> ();
		deleteDic = new Dictionary<string, PanelBase> ();

        UIPanel rootPanel = NGUITools.CreateUI(false, LayerMask.NameToLayer(GameDefines.LayerNGUI));
        rootPanel.name = "UI Root";
        UIRoot uiRoot = rootPanel.gameObject.GetComponent<UIRoot>();
        uiRoot.scalingStyle = UIRoot.Scaling.FixedSizeOnMobiles;
        uiRoot.manualHeight = (Screen.height > 640 ? 768 : 640);
        uiRoot.minimumHeight = (Screen.height > 640 ? 768 : 640);
        uiRoot.maximumHeight = 768;

        // add Effect Camera
        GameObject effectCameraObj = new GameObject("EffectCamera");
		effectCameraObj.layer = LayerMask.NameToLayer (GameDefines.LayerNGUI);
		int mask = (1 << LayerMask.NameToLayer (GameDefines.LayerEffect));
        effectCameraObj.transform.parent = rootPanel.transform;
        Camera effectCamera = effectCameraObj.AddComponent<Camera>();
		effectCamera.clearFlags = CameraClearFlags.Depth;
		effectCamera.cullingMask = mask;
		effectCamera.orthographic = true;
		effectCamera.orthographicSize = 1.0f;
		effectCamera.nearClipPlane = -10f;
		effectCamera.farClipPlane = 10f;
		effectCamera.depth = 2;

        // add Another UICamera
        GameObject otherUICameraObj = new GameObject("OtherUICamera");
        otherUICameraObj.layer = LayerMask.NameToLayer(GameDefines.LayerNGUI);
        int maskUI = (1 << LayerMask.NameToLayer(GameDefines.LayerHigherUI));
        otherUICameraObj.transform.parent = rootPanel.transform;
        Camera otherUICamera = otherUICameraObj.AddComponent<Camera>();
        otherUICamera.clearFlags = CameraClearFlags.Depth;
        otherUICamera.cullingMask = maskUI;
        otherUICamera.orthographic = true;
        otherUICamera.orthographicSize = 1.0f;
        otherUICamera.nearClipPlane = -10f;
        otherUICamera.farClipPlane = 10f;
        otherUICamera.depth = 3;
        UICamera uiCamera = otherUICameraObj.AddComponent<UICamera>();
        uiCamera.enabled = false;
        //uiCamera.eventReceiverMask = (1 << LayerMask.NameToLayer(GameDefines.LayerHigherUI));

        GameObject obj = Resources.Load<GameObject>(@"UI/LightPanel");
        if (null != obj)
            NGUITools.AddChild(effectCameraObj, obj);

        // 设置UIControl里的静态变量
        GameObject uiCameraObj = GameObject.Find(uiCameraPath);
        UIControl.CameraUI = uiCameraObj.GetComponent<Camera>();
        UIControl.CameraUIFx = effectCamera;
        UIControl.CameraHigherUI = otherUICameraObj.GetComponent<Camera>();
    }

    /// <summary>
    /// 释放面板
    /// </summary>
    public virtual void Release()
    {
        panelDic.Clear();
        panelDic = null;

		secendLevelPanelDic.Clear ();
		secendLevelPanelDic = null;

		deleteDic.Clear ();
		deleteDic = null;
    }

    /// <summary>
    /// 加载面板
    /// </summary>
    protected void LoadPanel(string prefabPath, string panelName, Transform parentObj, float zValue, bool isSecendLevelUI = false)
    {
        try
        {
            #region UI卡死，加一个容错试试
            if (panelDic.ContainsKey(panelName))
            {
                if (secendLevelPanelDic.ContainsKey(panelName))
                {
                    //这个UI想要重复打开？导致卡死？？
                    LogManager.Log("PanelManager LoadPanel: " + panelName + " Open Again?!", LogType.Error);
                    Debug.Log("PanelManager LoadPanel: " + panelName + " Open Again?!");
                    if (null != UIControl.GetPanel(panelName))
                    {
                        UIControl.GetPanel(panelName).UnLoad(panelName);
                    }
                }
            }
            #endregion

            GameObject panelPrefab = Resources.Load(prefabPath) as GameObject;
            GameObject panelObj = GameObject.Instantiate(panelPrefab) as GameObject;

            if (parentObj == null)
            {
                parentObj = UIControl.CameraUI.transform;
            }
            panelObj.transform.parent = parentObj;

            if (zValue != 0)
                panelObj.transform.localPosition = new Vector3(0, 0, zValue);

            panelObj.transform.localScale = Vector3.one;
            panelObj.name = panelName;

            PanelBase panelBase = panelObj.GetComponent<PanelBase>();
            panelDic.Add(panelName, panelBase);
            if (isSecendLevelUI)
                secendLevelPanelDic.Add(panelName, panelBase);
            // 初始化Panel
            panelBase.Init();
        }
        catch (UnityException uex)
        {
            LogManager.Log(uex.ToString(), LogType.Fatal);
        }
    }

	/// <summary>
	/// 卸载面板
	/// </summary>
	public void UnLoadPanel(string panelName, bool isClearAssets = true)
	{
		if(secendLevelPanelDic.ContainsKey(panelName) && panelDic.ContainsKey(panelName))
		{
			GameObject.Destroy(panelDic[panelName].gameObject);
			secendLevelPanelDic.Remove(panelName);
			panelDic.Remove(panelName);
			if(isClearAssets)
				Resources.UnloadUnusedAssets();
		}
	}

    public void UnLoadMainPanel(string panelName)
    {
        if (panelDic.ContainsKey(panelName))
        {
            GameObject.Destroy(panelDic[panelName].gameObject);
            panelDic.Remove(panelName);
        }
    }

    /// <summary>
    /// 获取面板
    /// </summary>
    public PanelBase GetPanel(string panelName)
    {
        if (panelDic.ContainsKey(panelName))
            return panelDic[panelName];

        return null;
    }

    public T GetPanel<T>(string panelName) where T : PanelBase
    {
        try
        {
            PanelBase pb = GetPanel(panelName);
            if (null != pb)
                return pb as T;
        }
        catch (UnityException uex)
        {
            LogManager.Log(uex.ToString(), LogType.Fatal);
        }
        catch (System.Exception ex)
        {
            LogManager.Log(ex.ToString(), LogType.Fatal);
        }
        return default(T);
    }

	/// <summary>
	/// 隐藏所有的UI, 动态加载的UI不会被卸载，只会被隐藏，慎用！！
	/// </summary>
    public void HideAllPanel(bool disable)
    {
        List<string> bufferList = new List<string>(panelDic.Keys);
        foreach (string str in bufferList)
        {
            if (panelDic.ContainsKey(str))
            {
                PanelBase panel = panelDic[str];
                if (panel != null)
                {
                    panel.Hide(disable);
                } 
            }
        }
    }

	/// <summary>
	/// 隐藏所有常驻内存的UI，跳过动态加载的UI
	/// </summary>
	public void HideAllPanelExceptSecendLevelUI(bool disable)
	{
		foreach (KeyValuePair<string, PanelBase> data in panelDic)
		{
			if(secendLevelPanelDic.ContainsKey(data.Key))
				continue;
			
			PanelBase panel = data.Value;
			panel.Hide(disable);
		}
	}

	/// <summary>
	/// 卸载所有二级UI.如果想保留最新加载的UI，remainPanelName代表需要保留的面板名字
	/// </summary>
	public void HideAllSecendLevelUI(bool disable, string remainPanelName = "")
	{
		foreach (KeyValuePair<string, PanelBase> data in secendLevelPanelDic)
		{
			if(remainPanelName.Length > 0)
				if(data.Key.Equals(remainPanelName))
					continue;

            if (data.Value == null)
            {
                continue;
            }

			deleteDic.Add(data.Key, data.Value);
		}
		foreach (KeyValuePair<string, PanelBase> data in deleteDic)
		{
            if (data.Value == null)
            {
                continue;
            }

			data.Value.UnLoad(data.Key);
		}
		deleteDic.Clear ();

        if (!remainPanelName.Equals(Panels.GuideTips) && UIControl.Instance.PanelManager.GetPanel(Panels.GuideTips) != null)
        {
            TmpGuideControl.HideGuideTipPanel();
        }

        BagTipsManager.Instance.OnDestoryTipsObj(false);
	}
}
