using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using TaidouCommon;
using TaidouCommon.Tools;

public class BossController : ControllerBase {
    public override OperationCode OpCode
    {
        get { return OperationCode.Boss;}
    }

    public void SyncBossAnimation(BossAnimationModel model)
    {
        Dictionary<byte,object> parameters = new Dictionary<byte, object>();   
        ParameterTool.AddParameter(parameters,ParameterCode.BossAnimationModel, model);
        PhotonEngine.Instance.SendRequest(OpCode,SubCode.SyncBossAnimation,parameters);
    }

    public override void OnEvent(EventData eventData)
    {
        SubCode subcode = ParameterTool.GetSubcode(eventData.Parameters);
        switch (subcode)
        {
            case SubCode.SyncBossAnimation:
                BossAnimationModel model = ParameterTool.GetParameter<BossAnimationModel>(eventData.Parameters,
                    ParameterCode.BossAnimationModel);
                if (OnSyncBossAnimation!=null)
                {
                    OnSyncBossAnimation(model);
                }
                break;
        }
    }

    public override void OnOperationResponse(OperationResponse response)
    {
    }

    public event OnSyncBossAnimationEvent OnSyncBossAnimation;
}
