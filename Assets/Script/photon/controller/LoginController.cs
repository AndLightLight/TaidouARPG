using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using LitJson;
using TaidouCommon;
using TaidouCommon.Model;

public class LoginController : ControllerBase
{
    private RoleController roleController;

    public override void Start()
    {
        base.Start();
        roleController = this.GetComponent<RoleController>();
    }

    public override OperationCode OpCode
    {
        get { return OperationCode.Login; }
    }

    public void Login( string username,string password )
    {
        User user = new User() {Username = username, Password = password};
        string json = JsonMapper.ToJson(user);
        Dictionary<byte,object> parameters = new Dictionary<byte, object>();
        parameters.Add((byte) ParameterCode.User,json);
        PhotonEngine.Instance.SendRequest(OperationCode.Login, parameters);
    }

    public override void OnOperationResponse(OperationResponse response)
    {
        switch (response.ReturnCode)
        {
            case (short)ReturnCode.Success:
                //根据登录的用户  加载用户的角色信息
                StartmenuController._instance.HideStartPanel();
                roleController.GetRole();
                break;
            case (short)ReturnCode.Fail:
                MessageManager._instance.ShowMessage(response.DebugMessage,2);
                break;
        }
    }
}
 