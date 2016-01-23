using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LoginPanel : IPanel
{
    private const string mAccountInput = @"LoginRegion/Account/AccountInput";
    private const string mPswdInput = @"LoginRegion/Password/PasswordInput";
    private const string mLoginButton = @"LoginRegion/LoginButton";
    private const string mReturnBtn = @"ReturnBtn";
    
    private UIInput userInput = null;
    private UIInput pswdInput = null;

    public override void Init()
    {
        base.Init();

        UIButton loginBtn = AddSubObject<UIButton>(mLoginButton);
        UIEventListener.Get(loginBtn.gameObject).onClick += LoginButtonClick;

        UIButton returnBtn = AddSubObject<UIButton>(mReturnBtn);
        UIEventListener.Get(returnBtn.gameObject).onClick += OnBackBtnClicked;

        userInput = AddSubObject<UIInput>(mAccountInput);
        userInput.characterLimit = 32;

        pswdInput = AddSubObject<UIInput>(mPswdInput);
        pswdInput.characterLimit = 32;
    }

    public override void Show(bool enable)
    {
        base.Show(enable);
    }

    public override void Hide(bool disable)
    {
        base.Hide(disable);
    }

    void OnBackBtnClicked(GameObject btn)
    {
        
    }

    void LoginButtonClick(GameObject button)
    {
        if (string.IsNullOrEmpty(userInput.value))
        {
            //TipsManager.SetTips(TipsSystemType.ConfirmTips, "提示", "账号不能为空", ConfirmTipsType.Ok_Button);
            return;
        }

        if (string.IsNullOrEmpty(pswdInput.value))
        {
            //TipsManager.SetTips(TipsSystemType.ConfirmTips, "提示", "密码不能为空", ConfirmTipsType.Ok_Button);
            return;
        }

        string name = "";
        string password = "";

        if (null != userInput)
            name = userInput.value;

        if (null != pswdInput)
            password = pswdInput.value;
    }

}