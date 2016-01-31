using UnityEngine;
using System.Collections;

public class LoadingPanelMng : PanelManager
{
	public override void Init(UIControl.SecendLevelUIType st = UIControl.SecendLevelUIType.None)
	{
		base.Init();

		//LoadPanel(@"UI/Loading/LoadingPanel", "LoadingPanel", null, 0);
		//GetPanel("LoadingPanel").Show(true);       
	}
}
