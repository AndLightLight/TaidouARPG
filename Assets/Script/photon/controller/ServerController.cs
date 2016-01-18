using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using LitJson;
using TaidouCommon;

public class ServerController : ControllerBase {
    public override void Start()
    {
        base.Start();
        PhotonEngine.Instance.OnConnectedToServer += GetServerList;
    }

    public void GetServerList()
    {
        PhotonEngine.Instance.SendRequest(OperationCode.GetServer, new Dictionary<byte, object>() );
    }

    public override void OnOperationResponse(OperationResponse response)
    {
        Dictionary<byte, object> parameters = response.Parameters;
        object jsonObject = null;
        parameters.TryGetValue((byte) ParameterCode.ServerList, out jsonObject);
        List<TaidouCommon.Model.ServerProperty> serverList =
            JsonMapper.ToObject<List<TaidouCommon.Model.ServerProperty>>(jsonObject.ToString());

        int index = 0;
        ServerProperty spDefault=null;
        GameObject goDefault=null;
        foreach (TaidouCommon.Model.ServerProperty spTemp in serverList) {
            //            public string ip="127.0.0.1:9080";
            //             public string name="1区 马达加斯加";
            //public int count=100;
            string ip = spTemp.IP + ":4530";
            string name = spTemp.Name;
            int count = spTemp.Count;
            GameObject go = null;
            if (count > 50) {
                //火爆
                go = NGUITools.AddChild(StartmenuController._instance.serverlistGrid.gameObject, StartmenuController._instance.serveritemRed);
            } else {
                //流畅
                go = NGUITools.AddChild(StartmenuController._instance.serverlistGrid.gameObject, StartmenuController._instance.serveritemGreen);
            }
            ServerProperty sp = go.GetComponent<ServerProperty>();
            sp.ip = ip;
            sp.name = name;
            sp.count = count;
            if (index == 0)
            {
                spDefault = sp;
                goDefault = go;
            }
            StartmenuController._instance.serverlistGrid.AddChild(go.transform);
            index++;
        }
        StartmenuController.sp = spDefault;
        StartmenuController._instance.servernameLabelStart.text = spDefault.name;
    }
    
    public override void OnDestroy()
    {
        base.OnDestroy();
        PhotonEngine.Instance.OnConnectedToServer -= GetServerList;
    }

    public override OperationCode OpCode {
        get { return OperationCode.GetServer;
        }
    }
}
