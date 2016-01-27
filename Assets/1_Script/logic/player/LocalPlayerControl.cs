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
        base.SelfStart();
	}

	protected override void SelfUpdate()
	{
        base.SelfUpdate();
	}

    public override void Initialize()
    {
        base.Initialize();
    }

}
