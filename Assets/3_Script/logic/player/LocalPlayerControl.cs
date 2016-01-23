using UnityEngine;
using System.Collections;

public class LocalPlayerControl : HumanoidControl
{
	private static LocalPlayerControl _ins = null;
	public static LocalPlayerControl Instance
	{
		get { return _ins; }
		set { _ins = value; }
	}

	protected override void SelfStart()
	{
        speed = 1.0f;
	}

	protected override void SelfUpdate()
	{

	}

}
