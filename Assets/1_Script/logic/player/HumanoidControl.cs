using UnityEngine;
using System.Collections;

public class HumanoidControl : CreatureControl
{
    public HumanoidAvatar m_humanoidAvatar = null;

    protected override void SelfStart()
    {
        base.SelfStart();
    }

    protected override void SelfUpdate()
    {
        base.SelfUpdate();
    }

    public override void Initialize()
    {
        base.Initialize();

        this.Speed = 3.0f;
    }

    protected override void OnBeginMove(Vector3 destination)
    {
        m_humanoidAvatar.Run = true;

        TransformUtility.LookAtTargetImmediately(this, destination - this.transform.position);
    }

    protected override void OnStopMove()
    {
        m_humanoidAvatar.Run = false;
    }

    


}
