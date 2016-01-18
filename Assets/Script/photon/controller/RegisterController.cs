using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using TaidouCommon;
using TaidouCommon.Model;

public class RegisterController : ControllerBase
{

    private StartmenuController controller;
    private User user;


    public override TaidouCommon.OperationCode OpCode {
        get { return OperationCode.Register; }
    }

    public void Register(string username, string password,StartmenuController controller)
    {
        this.controller = controller;
        user = new User() {Username = username, Password = password};
        string json = JsonMapper.ToJson(user);
        Dictionary<byte,object> parameters = new Dictionary<byte, object>();
        parameters.Add((byte) ParameterCode.User,json);
        PhotonEngine.Instance.SendRequest(OperationCode.Register, parameters);
    }

    public override void OnOperationResponse(ExitGames.Client.Photon.OperationResponse response) {
        switch (response.ReturnCode)
        {
            case  (short)ReturnCode.Fail:
                MessageManager._instance.ShowMessage(response.DebugMessage,2);
                break;
            case (short)ReturnCode.Success:
                MessageManager._instance.ShowMessage("注册成功", 2);
                controller.HideRegisterPanel();
                controller.ShowStartPanel();
                controller.usernameLabelStart.text = user.Username;
                break;
        }
    }
}
