using UnityEngine;
using System.Collections;

public class CreatureControl : IObject
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
		
	}

	protected override void SelfUpdate()
	{
        if (m_isMoving)
        {
            float dist = (m_navMeshAgent.destination - this.GetPosition()).magnitude;
            if (dist < 0.15f)
            {
                OnNavArrive();
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

    }

    public virtual void MoveTo(Vector3 targetPosition)
    {
        m_isMoving = true;
        m_navMeshAgent.SetDestination(targetPosition);
    }

    protected virtual void OnNavArrive()
    {

    }

}
