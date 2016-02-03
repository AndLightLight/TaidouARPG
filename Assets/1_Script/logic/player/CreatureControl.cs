using UnityEngine;
using System.Collections;

public class CreatureControl : ObjectControl
{
    private float m_speed = 0.0f;
    public float Speed
    {
        set
        {
            m_speed = value;
            if (m_navMeshAgent != null)
                m_navMeshAgent.speed = m_speed;
        }
        get
        {
            return m_speed;
        }
    }

    private bool m_isMoving = false;

    public NavMeshAgent m_navMeshAgent = null;

    protected override void SelfStart()
    {
		base.SelfUpdate();
    }

    protected override void SelfUpdate()
    {
		base.SelfUpdate();

        if (m_isMoving)
        {
            float dist = MathUtility.CalcDistance2D(m_navMeshAgent.destination, this.GetPosition());
            if (dist < ObjectDefines.maxStopDistance)
            {
                OnStopMove();
                m_isMoving = false;
            }
        }

    }

    public virtual void Initialize()
    {

    }

    public virtual bool HandleEvent(int eventValue)
    {
        return false;
    }

    public virtual void Move(Vector3 speed)
    {
        m_isMoving = true;

        speed.Normalize();

        Vector3 destination = this.transform.position + speed;

        m_navMeshAgent.SetDestination(destination);

        OnBeginMove(destination);
    }

    public virtual void MoveTo(Vector3 targetPosition)
    {
        m_isMoving = true;

        m_navMeshAgent.SetDestination(targetPosition);

        OnBeginMove(targetPosition);
    }

    public virtual void StopMove()
    {
        m_isMoving = true;
        m_navMeshAgent.Stop();
        OnStopMove();
    }

    protected virtual void OnBeginMove(Vector3 destination) { }

    protected virtual void OnStopMove() { }

}
