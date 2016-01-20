
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IPanel : MonoBehaviour
{
    protected bool isShown;
    public bool IsShown { get { return isShown; } }

    protected Dictionary<string, GameObject> subObjects = new Dictionary<string, GameObject>();

    private GameObject ColdingObj = null;
    private float ColdingTime = 0;
    private const float ButtonColdDown = 1f;

    public virtual void Init()
    {
        
    }

    public virtual void Show(bool active)
    {
        isShown = true;
        if (active)
        {
            transform.gameObject.SetActive(true);
        }
    }

    public virtual void Hide(bool disable)
    {
		isShown = false;
        if (disable)
        {
            transform.gameObject.SetActive(false);
		}
    }

	public virtual void UnLoad(string panelName)
	{
		isShown = false;
		//UIControl.Instance.PanelManager.UnLoadPanel(panelName);
	}

    protected T AddSubObject<T>(string name) where T : Component
    {
        Transform objTrans = transform.FindChild(name);
		if (null != objTrans)
        {
			subObjects.Add(name, objTrans.gameObject);
			return objTrans.GetComponent<T>();
        }

        return default(T);
    }

    protected T FindSubObject<T>(string name) where T : Component
    {
        if (subObjects.ContainsKey(name))
        {
            GameObject obj = subObjects[name];
            return obj.GetComponent<T>();
        }

        return default(T);
    }

    protected T LoadPanel<T>(GameObject parent, string path) where T : IPanel
    {
        GameObject obj = Resources.Load(path) as GameObject;
        GameObject panelObj = NGUITools.AddChild(gameObject, obj);

        T IPanel = panelObj.GetComponent<T>();
        IPanel.Init();

        return IPanel;
    }
}

