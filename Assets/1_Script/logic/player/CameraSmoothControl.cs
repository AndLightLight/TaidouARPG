/// <summary>
///
/// Component Name: CameraSmoothControl
/// 
/// Author: Dr
///
/// Usage:
///      1、挂在主摄像机上，通过MainCameraCtrl动态加载
/// 
/// Require: 
///      1、SceneManager，需要得到SceneManager里的保存的主角色;需要获取住UI面板
///
/// Mark:
///      1、此脚本只完成一个功能：控制摄像机平滑
///      2、新增功能首先考虑新加脚本（组件）的方式而不是修改这个脚本
///
/// Brief:
///      1、提供控制摄像机平滑的参数
///      2、提供了双手指放大缩小摄像机FOV功能
///
/// Log:
/// 	 2015-03-03 拆分实现摄像机平滑
/// 
/// </summary>

using UnityEngine;
using System.Collections;

public class CameraSmoothControl : MonoBehaviour 
{
    public static float ScrollFactor = 1f;

    private float _Factor = 0.05f;
	private float _ScreenFactor = 1f;
    private const string _mainPanel = "MainPanel";

	// Use this for initialization
	void Start () 
    {
	    
	}
	
	// Update is called once per frame
	void Update () 
    {
        CameraSmooth();	
	}

    void CameraSmooth()
    {
#if (UNITY_IPHONE || UNITY_ANDROID) && !UNITY_EDITOR
        try
        {
			if (Input.touchCount == 2 && UIControl.GetPanel(_mainPanel).gameObject.activeSelf && MainCameraCtrl.isAllowScroll)
            {
                Camera uiCamera = GameObject.Find("UI Root/Camera").camera;
                Ray ray = uiCamera.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer(GameDefines.LayerNoSupportTouch)
                           || hit.collider.gameObject.layer == LayerMask.NameToLayer(GameDefines.LayerNGUI))
                    {
                        return;
                    }
                }

                ray = uiCamera.ScreenPointToRay(Input.GetTouch(1).position);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer(GameDefines.LayerNoSupportTouch)
                           || hit.collider.gameObject.layer == LayerMask.NameToLayer(GameDefines.LayerNGUI))
                    {
                        return;
                    }
                }
                if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
                {
                    Vector2 afterDis = Input.GetTouch(0).position - Input.GetTouch(1).position;
                    Vector2 preDis = (Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition)
                                 - (Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition);
                    float movedDelta = afterDis.magnitude - preDis.magnitude;
					ScrollFactor += _ScreenFactor * _Factor * movedDelta * Time.deltaTime;

                    //平滑时停止移动
                   PersonControl.Instance.StopMove();
                }
            }
        }
        catch (System.Exception ex)
        {
            LogManager.Log("CameraSmooth Exception: " + ex.StackTrace, LogType.Error);
        }
#endif
        if (ScrollFactor <= 0)
            ScrollFactor = 0;
        if (ScrollFactor >= 1)
            ScrollFactor = 1;
    }
}
