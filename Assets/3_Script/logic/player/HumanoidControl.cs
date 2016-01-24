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

    public override void Move(Vector3 speed)
    {
        m_humanoidAvatar.Run = true;
    }

    public override void MoveTo(Vector3 targetPosition)
    {
        base.MoveTo(targetPosition);

        m_humanoidAvatar.Run = true;
    }

    protected override void OnNavArrive()
    {
        m_humanoidAvatar.Run = false;
    }

}
