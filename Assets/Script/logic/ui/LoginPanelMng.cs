using UnityEngine;
using System.Collections;

public class LoginPanelMng : PanelManager
{
    public override void Init(/*UIControl.SecendLevelUIType st = UIControl.SecendLevelUIType.None*/)
    {
        base.Init();

        GameObject uiCamaraObj = GameObject.Find(uiCameraPath);
        Camera uiCamera = uiCamaraObj.GetComponent<Camera>();
        uiCamera.clearFlags = CameraClearFlags.Depth;

       // NGUITools.SetActive(UIControl.CameraUIFx.gameObject, false);

        // 除角色选择角色之外所有界面的背景界面
        LoadPanel("UI/Login/LoginBackground", "LoginBg", null, 500);

        LoadPanel("UI/Login/QuickLoginPanel", "QuickLoginPanel", null, 300);
        LoadPanel("UI/Login/LoginPanel", "LoginPanel", null, 300);
        LoadPanel("UI/Login/RegisterPanel", "RegisterPanel", null, 300);
        LoadPanel("UI/Login/SelectRolePanel", "SelectRolePanel", null, 300);
        LoadPanel("UI/Login/CreateRolePanel", "CreateRolePanel", null, 300);
        LoadPanel("UI/Login/SelectServerPanel", "SelectServerPanel", null, 300);
        LoadPanel("UI/Common/ModelPanel", "ModelPanel", null, 0);
        LoadPanel("UI/Common/UserInputPanel", "UserInputPanel", null, -250);
        LoadPanel("UI/Common/TipsPanel", "TipsPanel", null, -300);
        LoadPanel("UI/Loading/WaitLoadingPanel", "WaitLoadingPanel", null, -400);
        //系统向上飘动tips面板
        LoadPanel("UI/Main/Notice/FloatWordTipPanel", "FloatWordTipPanel", null, 0);

        GameObject roleShowObj = GameObject.Find("RoleShow").gameObject;
        LoadPanel(@"UI/Login/ProfessionsPresentation", "ProfessionsPresentation", roleShowObj.transform, 0f);

        HideAllPanel(true);

        //UIControl.Instance.PanelManager.GetPanel("SelectRolePanel").Show(true);

    }
}
