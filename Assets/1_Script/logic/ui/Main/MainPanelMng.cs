using UnityEngine;
using System.Collections;

public class MainPanelMng : PanelManager
{
	public override void Init(UIControl.SecendLevelUIType st = UIControl.SecendLevelUIType.None)
	{
		base.Init();

		/*GameObject uiCamaraObj = GameObject.Find(uiCameraPath);
		Camera uiCamera = uiCamaraObj.GetComponent<Camera>();
		uiCamera.clearFlags = CameraClearFlags.Depth;*/

		LoadPanel("UI/Main/MainPanel", "MainPanel", null, 300);

		HideAllPanel(true);

		UIControl.Instance.PanelManager.GetPanel("MainPanel").Show(true);

	}
}
