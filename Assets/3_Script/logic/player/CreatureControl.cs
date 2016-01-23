using UnityEngine;
using System.Collections;

public class CreatureControl : IObject
{
    public float speed;

	protected override void SelfStart()
	{
		
	}

	protected override void SelfUpdate()
	{

	}

	public bool HandleEvent(int eventValue)
	{
		return false;
	}
}
