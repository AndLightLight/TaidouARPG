using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using TaidouCommon;
using TaidouCommon.Model;
using TaidouCommon.Tools;

public class RoleController : ControllerBase {

    public override TaidouCommon.OperationCode OpCode {
        get { return OperationCode.Role;
        }
    }

    public void GetRole()
    {
        Dictionary<byte,object> parameters = new Dictionary<byte, object>();
        parameters.Add( (byte) ParameterCode.SubCode,SubCode.GetRole );
        PhotonEngine.Instance.SendRequest(OpCode,parameters);
    }

    public void AddRole( Role role )
    {
        Dictionary<byte, object> parameters = new Dictionary<byte, object>();
        parameters.Add((byte)ParameterCode.SubCode, SubCode.AddRole);
        parameters.Add((byte) ParameterCode.Role,JsonMapper.ToJson(role));
        PhotonEngine.Instance.SendRequest(OpCode, parameters);
    }

    public void SelectRole(Role role) {
        Dictionary<byte, object> parameters = new Dictionary<byte, object>();
        parameters.Add((byte)ParameterCode.Role, JsonMapper.ToJson(role));
        PhotonEngine.Instance.SendRequest(OpCode,SubCode.SelectRole,parameters);
    }

    public void UpdateRole(Role role) {
        Dictionary<byte, object> parameters = new Dictionary<byte, object>();
        parameters.Add((byte)ParameterCode.Role, JsonMapper.ToJson(role));
        PhotonEngine.Instance.SendRequest(OpCode, SubCode.UpdateRole, parameters);
    }

    public override void OnOperationResponse(ExitGames.Client.Photon.OperationResponse response)
    {
        SubCode subcode = ParameterTool.GetParameter<SubCode>(response.Parameters, ParameterCode.SubCode,false);
        switch (subcode)
        {
            case SubCode.GetRole:
                List<Role> list = ParameterTool.GetParameter<List<Role>>(response.Parameters, ParameterCode.RoleList);
                OnGetRole(list);
                break;
            case SubCode.AddRole:
                Role role = ParameterTool.GetParameter<Role>(response.Parameters, ParameterCode.Role);
                OnAddRole(role);
                break;
            case SubCode.SelectRole:
                if (OnSelectRole!=null)
                {
                    OnSelectRole();
                }
                break;
        } 
    }

    public event OnGetRoleEvent OnGetRole;
    public event OnAddRoleEvent OnAddRole;
    public event OnSelectRoleEvent OnSelectRole;
}
