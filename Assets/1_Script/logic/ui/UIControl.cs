

using UnityEngine;
using System.Collections;

public class UIControl
{
    public enum UIType
    {
        Launch,
        Login,
        Loading,
        Main,
    }

	public enum SecendLevelUIType
	{
		/// <summary>
		/// 默认常驻内存的面板
		/// </summary>
		None,
	}

    private static UIControl instance = null;
    public static UIControl Instance
    {
        get
        {
            if (null == instance)
                instance = new UIControl();
            return instance;
        }
    }

    private static PanelManager panelManger;
    public PanelManager PanelManager { get { return panelManger; } }

    public UIType mUIType = UIType.Launch;

    public static Camera CameraUI = null;
    public static Camera CameraHigherUI = null;
    public static Camera CameraUIFx = null;

    public GameObject UIRootObj = null;

    // 加载每一个场景的PanelManger，如果两个场景使用相同UI布局，则不释放资源
    public void Init(UIType t)
    {
        mUIType = t;

        //主UI不卸载
        if (t == UIType.Main && null != panelManger && null != UIRootObj)
            return;

        if (panelManger != null)
        {
            panelManger.Release();
            panelManger = null;
        }

        switch (t)
        {
            case UIType.Login:
                panelManger = new LoginPanelMng();
                break;
            case UIType.Loading:
                panelManger = new LoadingPanelMng();
                break;
			case UIType.Main:
                panelManger = new MainPanelMng();
                break;
            default:
                //panelManger = new MainPanelMng();
                break;
        }

        if (null != panelManger)
			panelManger.Init(SecendLevelUIType.None);
	}
	
	public void Init(SecendLevelUIType st = SecendLevelUIType.None)
	{
		if (null != panelManger)
			panelManger.Init(st);
	}
	
	/// <summary>
    /// 获取UI静态方法
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IPanel GetPanel(string key)
    {
        if (panelManger == null)
            return null;

        return panelManger.GetPanel(key);
    }

    public static T GetPanel<T>(string panelName) where T : IPanel
    {
        if (panelManger != null)
        {
            return panelManger.GetPanel<T>(panelName);
        }
        return null;
    }
    // 加载公共UI，这部分UI在跨场景时资源不会被释放掉
    public void LoadCommonUI()
    {
    }

    public void DestoryCommonUI()
    {
    }

    #region 增加一个UICamera，解决UI显示在模型下层的问题
    int HigherUICount = 0;
    public void ShowNormalUICamera()
    {
        --HigherUICount;
        if (HigherUICount <= 0)
        {
            HigherUICount = 0;
            //高科技，切换UI摄像机
            UIControl.CameraHigherUI.GetComponent<UICamera>().enabled = false;
            //UIControl.CameraUI.GetComponent<UICamera>().enabled = true;
        }
    }

    public void ShowHigherUICamera()
    {
        ++HigherUICount;
        //高科技，切换UI摄像机
        UIControl.CameraHigherUI.GetComponent<UICamera>().enabled = true;
        //UIControl.CameraUI.GetComponent<UICamera>().enabled = false;
    }
    public void SetHigherUILayer(GameObject obj, Transform trans)
    {
       // NGUITools.SetLayer(obj, LayerMask.NameToLayer(GameDefines.LayerHigherUI));
        //NGUITools.SetChildLayer(trans, LayerMask.NameToLayer(GameDefines.LayerHigherUI));
    }
    #endregion
}

