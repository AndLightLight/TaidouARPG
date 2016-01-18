using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using TaidouCommon;
using TaidouCommon.Model;
using TaidouCommon.Tools;


public class PhotonEngine : MonoBehaviour ,IPhotonPeerListener{

    public ConnectionProtocol protocol = ConnectionProtocol.Tcp;
    public string serverAddress = "127.0.0.1:4530";
    public string applicationName = "TaidouServer";

    public delegate void OnConnectedToServerEvent();

    public event OnConnectedToServerEvent OnConnectedToServer;

    private Dictionary<byte, ControllerBase> controllers = new Dictionary<byte, ControllerBase>();
    private static PhotonEngine _instance;
    private PhotonPeer peer;
    private bool isConnected = false;

    public static PhotonEngine Instance {
        get {
            return _instance;
        }
    }

    public Role role;

    void Awake() {
        _instance = this;
        peer = new PhotonPeer(this, protocol);
        peer.Connect(serverAddress, applicationName);
        DontDestroyOnLoad(this.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        if (peer!=null)
            peer.Service();
	}
    public void RegisterController(OperationCode opCode, ControllerBase controller) {
        controllers.Add((byte)opCode, controller);
    }
    public void UnRegisterController(OperationCode opCode)
    {
        controllers.Remove((byte) opCode);
    }

    public void SendRequest( OperationCode opCode,Dictionary<byte,object> parameters  )
    {
        Debug.Log("sendrequest to server , opcode : "+opCode);
        peer.OpCustom((byte) opCode, parameters, true);
    }

    public void SendRequest(OperationCode opCode, SubCode subCode, Dictionary<byte, object> parameters)
    {
        Debug.Log("sendrequest to server , opcode : " + opCode +" subCode:  " +subCode);
        parameters.Add((byte)ParameterCode.SubCode, subCode);
        peer.OpCustom((byte)opCode, parameters, true);
    }

    public void DebugReturn(DebugLevel level, string message) {
        Debug.Log(level + ":" + message);
    }

    public void OnEvent(EventData eventData)
    {
        ControllerBase controller;
        OperationCode opCode = ParameterTool.GetParameter<OperationCode>(eventData.Parameters,
            ParameterCode.OperationCode, false);
        controllers.TryGetValue((byte) opCode, out controller);
        if (controller != null)
        {
            controller.OnEvent(eventData);
        }
        else
        {
            Debug.LogWarning( "Receive a unknown event . OperationCode: "+opCode );
        }
    }

    public void OnOperationResponse(OperationResponse operationResponse) {
        ControllerBase controller;
        controllers.TryGetValue(operationResponse.OperationCode, out controller);
        if (controller != null) {
            controller.OnOperationResponse(operationResponse);
        } else {
            Debug.LogWarning("Receive a unknown response . OperationCode :" + operationResponse.OperationCode);
        }
    }

    public void OnStatusChanged(StatusCode statusCode) {
        Debug.Log("OnStatusChanged:" + statusCode);
        switch (statusCode) {
            case StatusCode.Connect:
                isConnected = true;
                OnConnectedToServer();
                break;

            default:
                isConnected = false;
                break;
        }
    }
}
