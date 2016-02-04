using UnityEngine;
using System.Collections;

public class LaunchPanelMng : PanelManager
{
	public override void  Init(UIControl.SecendLevelUIType st = UIControl.SecendLevelUIType.None)
	{
 	    base.Init();

        LoadPanel("UI/Launch/LaunchPanel", "LaunchPanel", null, 0);
        HideAllPanel(true);
        GetPanel("LaunchPanel").Show(true);
    }
}