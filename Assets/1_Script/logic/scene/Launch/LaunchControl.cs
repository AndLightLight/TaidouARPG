using UnityEngine;
using System.Collections;

public class LaunchControl : MonoBehaviour
{

	private static int loadedPercent = 0;
	private static int sectionPercent = 0;

	WaitForEndOfFrame waitEndOfFrame = new WaitForEndOfFrame();


	public enum LaunchSection
	{
		Start,                  // 开始阶段 

		LoadTable,              // 加载表格
		LoadingTable,           // 加载表格中
		
		LoadLoginScene,         // 加载登录场景
		LoadingLoginScene,      // 登录场景加载中

		End,                    // 加载结束
	}

	public static LaunchSection CurSection = LaunchSection.Start;

	void Start()
	{
		StartCoroutine(SelfAddLoadPercent());
	}

	void Update()
	{
		switch (CurSection)
		{
			case LaunchSection.Start:
				UIControl.Instance.Init(UIControl.UIType.Launch);
				SetSectionPercent(0);
				break;

			case LaunchSection.LoadTable:
				if (SetSectionPercent(83))
					gameObject.AddComponent<TableInit>();
				break;
			case LaunchSection.LoadingTable:
				break;

			case LaunchSection.LoadLoginScene:
				if (SetSectionPercent(100))
					gameObject.AddComponent<LoadLoginScene>();
				break;
			case LaunchSection.LoadingLoginScene:
				break;

			case LaunchSection.End:
			default:
				break;
		}
	}

	public static void NextSection(MonoBehaviour component)
	{
		++CurSection;
		if (null != component)
			GameObject.Destroy(component);
	}

	public static bool SetSectionPercent(int loadPercent)
	{
		if (loadedPercent == sectionPercent)
		{
			++CurSection;
			sectionPercent = loadPercent;
			return true;
		}
		return false;
	}

	public static void JumpToSection(LaunchSection section)
	{
		CurSection = section;
	}

	private IEnumerator SelfAddLoadPercent()
	{
		while (true)
		{
			if (loadedPercent < sectionPercent)
			{
				++loadedPercent;
				try
				{
					Debug.Log("" + loadedPercent);
					LaunchPanel panel = UIControl.GetPanel<LaunchPanel>("LaunchPanel");
					if (null != panel)
					{
						panel.SetLoadingProgress(loadedPercent);
					}
				}
				catch (System.Exception e)
				{
					LogManager.Log(e.ToString(), LogType.Fatal);
					yield break;
				}
			}

			yield return waitEndOfFrame;

			if (loadedPercent >= 100)
				break;
		}
	}
}
