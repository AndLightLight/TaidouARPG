using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LaunchPanel : IPanel
{
	private const string mPercentLabelPath = @"percent";
	private UILabel percentLabel;

    public override void Init()
    {
        base.Init();

		percentLabel = AddSubObject<UILabel>(mPercentLabelPath);
    }

	void Start()
	{

	}

	void Update()
	{

	}

	public void SetLoadingProgress(int percent)
	{
		percentLabel.text = percent + "%";
	}

}