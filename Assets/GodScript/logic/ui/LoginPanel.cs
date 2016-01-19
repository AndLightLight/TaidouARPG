using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using com.jdxk.net.message;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class LoginPanel : PanelBase
{
    private bool bSavePasswd = true;

    private const string mAccountInput = @"LoginRegion/Account/AccountInput";
    private const string mPswdInput = @"LoginRegion/Password/PasswordInput";
    private const string mRegisterButton = @"LoginRegion/RegisterButton";
    private const string mLoginButton = @"LoginRegion/LoginButton";
    //private const string mRememberBtn = @"LoginRegion/Account/RememberAccount";
    private const string mForgetPswdButton = @"LoginRegion/Password/ForgetPswdButton";
    private const string mReturnBtn = @"ReturnBtn";
    
    private UIInput userInput = null;
    private UIInput pswdInput = null;
    private string storageName = "";
    private string storagePswd = "";
    private const string defaultPswd = "********************";

    public override void Init()
    {
        base.Init();

        // init login button
        UIButton loginBtn = AddSubObject<UIButton>(mLoginButton);
        UIEventListener.Get(loginBtn.gameObject).onClick += LoginButtonClick;

        // init register button
        UIButton registerBtn = AddSubObject<UIButton>(mRegisterButton);
        UIEventListener.Get(registerBtn.gameObject).onClick += RegisterButtonClick;

        // init return button
        UIButton returnBtn = AddSubObject<UIButton>(mReturnBtn);
        UIEventListener.Get(returnBtn.gameObject).onClick += OnBackBtnClicked;

        UIButton forgetBtn = AddSubObject<UIButton>(mForgetPswdButton);
        UIEventListener.Get(forgetBtn.gameObject).onClick += ForgetPswdButtonClick;

        //UIToggle rememberBtn = AddSubObject<UIToggle>(mRememberBtn);
        //UIEventListener.Get(rememberBtn.gameObject).onClick = OnRememberBtnClicked;
        DataStorage.GetInstance.SetPlayerInfo(GameDefines.IsSavePswd, System.Convert.ToInt32(true));    //默认存储账户名和密码
        //rememberBtn.startsActive = true;
        //if (DataStorage.GetInstance.GetPlayerInfo(GameDefines.IsSavePswd, ref bSavePasswd))
        //{ // has saved
        //    if (!bSavePasswd)
        //    { // has saved not saved
        //        bSavePasswd = false;
        //        rememberBtn.startsActive = false;
        //    }
        //}

        // set user input's limited
        userInput = AddSubObject<UIInput>(mAccountInput);
        userInput.characterLimit = (int)GameDefines.ACCOUNT_LEN_MAX;

        pswdInput = AddSubObject<UIInput>(mPswdInput);
        pswdInput.characterLimit = (int)GameDefines.ACCOUNT_LEN_MAX;
    }

    public override void Show(bool enable)
    {
        UIControl.Instance.PanelManager.HideAllPanel(true);
        UIControl.Instance.PanelManager.GetPanel("LoginBg").Show(true);
        //隐藏3DCamera
        LoginManager.Instance.HideMainCamera();
        base.Show(enable);

        // update userInput
        UpdateUserInput();
    }

    public override void Hide(bool disable)
    {
        base.Hide(disable);
    }

    void RegisterButtonClick(GameObject button)
    {
        UIControl.Instance.PanelManager.HideAllPanel(true);
        UIControl.GetPanel("RegisterPanel").Show(true);
    }

    void OnBackBtnClicked(GameObject btn)
    {
        UIControl.Instance.PanelManager.HideAllPanel(true);
        UIControl.GetPanel("QuickLoginPanel").Show(true);

        LoginManager.DisConnectLoginServer();
    }

    void LoginButtonClick(GameObject button)
    {
        if (string.IsNullOrEmpty(userInput.value))
        {
            TipsManager.SetTips(TipsSystemType.ConfirmTips, "提示", "账号不能为空", ConfirmTipsType.Ok_Button);
            return;
        }

        if (string.IsNullOrEmpty(pswdInput.value))
        {
            TipsManager.SetTips(TipsSystemType.ConfirmTips, "提示", "密码不能为空", ConfirmTipsType.Ok_Button);
            return;
        }

        string name = "";
        string password = "";

        if (null != userInput)
            name = userInput.value;

        if (null != pswdInput)
            password = pswdInput.value;

        //如果有密码的本地数据缓存
        if (storagePswd.Length > 0)
        {
            if (password.Equals(defaultPswd))
                password = storagePswd;
            else
                password = MathUtility.MD5Hash(password);
        }
        else
        {
            password = MathUtility.MD5Hash(password);
        }

        

        //将用户名、密码缓存到本地
        DataStorage.GetInstance.SetPlayerInfo(GameDefines.UserName, name, false);
        DataStorage.GetInstance.SetPlayerInfo(GameDefines.Password, password, false);
        //保存临时参数
        LoginManager.Instance.nLoginType = Enum_LoginType.LOGIN;
		LoginManager.uid = name;
        LoginManager.Instance.refUserName = name;
        LoginManager.Instance.refPassword = password;

        JObject jsonRoot = new JObject();
        jsonRoot[GameDefines.UserName] = LoginManager.Instance.refUserName;
        jsonRoot[GameDefines.Password] = LoginManager.Instance.refPassword;
        LoginManager.extInfo = JsonConvert.SerializeObject(jsonRoot);

        //尝试连接服务器
        LoginManager.ConnectLoginServer();
        //将所连接的服务器ID保存到本地
        LoginManager.Instance.SaveLoginServerId();
        LoginManager.Instance.LoginStatus = LoginManager.LoginStat.Login;
        UIControl.GetPanel("WaitLoadingPanel").Show(true);
    }

    //void OnRememberBtnClicked(GameObject button)
    //{
    //    bSavePasswd = !bSavePasswd;
    //    DataStorage.GetInstance.SetPlayerInfo(GameDefines.IsSavePswd, System.Convert.ToInt32(bSavePasswd));
    //}

    public bool GetCheckState()
    {
        if (bSavePasswd)
            return true;
        else
            return false;
    }

    void ForgetPswdButtonClick(GameObject button)
    {
        //logic for forget password
        Application.OpenURL(@"http://www.jdxk.com.cn/"); //待修改
    }

    public void UpdateUserInput()
    {
        storageName = "";
        storagePswd = "";
        if (null != userInput)
        {
            //如果有用户名的本地数据缓存，就读取缓存
            if (DataStorage.GetInstance.GetPlayerInfo(GameDefines.UserName, ref storageName))
            {
                userInput.value = storageName;
            }
        }

        if (null != pswdInput)
        {
            //如果有密码的本地数据缓存，就读取本地缓存
            if (DataStorage.GetInstance.GetPlayerInfo(GameDefines.Password, ref storagePswd))
            {
                if (bSavePasswd)
                {
                    //如果勾选记录密码，则密码显示一串*
                    pswdInput.value = defaultPswd;
                }
                else
                {
                    pswdInput.value = "";
                }
            }
            else
            {
                pswdInput.value = "";
            }
        }
    }

}