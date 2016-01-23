
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class HumanoidAvatar : MonoBehaviour
{
	public HumanoidControl humanoidControl = null;

	public int modelId = -1;
	public int replaceModeId = -1;

	// Animator
	protected Animator animator;
	Transform mTransform = null;


	void OnEnable()
	{
		mTransform = transform;
	}

	void OnDisable()
	{
	}

	public Animator GetAnimator()
	{
		return animator;
	}

	public virtual bool Run
	{
		get { return animator.GetBool(ObjectDefines.HASH_RUN); }
		set { animator.SetBool(ObjectDefines.HASH_RUN, value); }
	}

	public bool Dead
	{
		get { return animator.GetBool(ObjectDefines.HASH_DEAD); }
		set { animator.SetBool(ObjectDefines.HASH_DEAD, value); }
	}

	public float Speed
	{
		get { return animator.speed; }
		set { animator.speed = value; }
	}

	public bool Idle
	{
		set { animator.SetBool(ObjectDefines.HASH_IDLE, value); }
	}

	public void ResetAnimator()
	{
		Run = false;
	}

	void AnimStart()
	{
		Idle = false;

		//SendEvent(humanoidControl, HumanoidControl.HandleEventType.AE_AnimStart);
	}

	void SendEvent(HumanoidControl humanCtrl, int eventValue)
	{
		if (humanCtrl != null)
			humanCtrl.HandleEvent(eventValue);
	}

	//获取当前正在播放的动画的剩余时间
	public virtual float GetCurrentAnimationClipRemainTime()
	{
		AnimatorStateInfo stateInfo = GetAnimator().GetCurrentAnimatorStateInfo(0);

		return stateInfo.length * (Mathf.CeilToInt(stateInfo.normalizedTime) - stateInfo.normalizedTime);
	}

	public void Awake()
	{
		InitializeComponent();
	}

	void Start()
	{
		InitializeModel();
	}

	// Update is called once per frame
	public void Update()
	{
		try
		{
		}
		catch (UnityException uex)
		{
			LogManager.Log(uex.ToString(), LogType.Fatal);
		}
	}

	protected virtual void InitializeComponent()
	{
		// Animator
		animator = GetComponentInChildren<Animator>();
		if (animator == null)
		{
		}
	}

	protected virtual void InitializeModel()
	{

		// Init Effect
		//effectControl.InitEffectOnWeapon (DataManager.Instance.PlayerDic[this].CareerId);
		//effectControl.InitModelEffect();

		//使用池之后就别这么用了
		//脚下挂点
		//underfooting = (GameObject)Instantiate(Resources.Load("EffectPrefab/E_Blame _jiaoxiaguanghuan01"));
		//underfooting = BundleResource.GetMe().GetResourceInstance(1256);		
		//underfooting.transform.parent = transform.FindChild(@"bone_Root/root");
		//underfooting.transform.localPosition = new Vector3(0, 0.1f, 0);
		//underfooting.transform.Rotate(new Vector3(90f, 0, 0));
		//underfooting.SetActive(false);
	}

	public virtual bool UseSkill(int skillBaseID, HumanoidControl targetObj = null)
	{
		try
		{
			/*SkillBase_Tbl SkillTab = com.jdxk.Configs.ConfigPool.Instance.GetDataByKey<SkillBase_Tbl>(skillBaseID);
			if (null == SkillTab)
			{
				return false;
			}

			this.TargetObj = targetObj;
			SkillID = skillBaseID;
			Skill = SkillTab.aniID;
			if (null != targetObj)
			{
				Utility.LookAtTargetImmediately(humanoidControl, targetObj);
			}

			//释放技能飘字
			if (!string.IsNullOrEmpty(SkillTab.fontname))
			{
				HUDTextManager.Instance.SetText(HUDTextManager.TextType.SKILL, SkillTab.fontname, humanoidControl);
			}*/

			return true;
		}
		catch (UnityException uex)
		{
			LogManager.Log(uex.ToString(), LogType.Fatal);
		}
		return false;
	}

	public virtual void Clear()
	{
		SetTarget(false);
	}

	public virtual void SetTarget(bool isTartget)
	{
	}

	//是否是主角
	public bool IsLocalPlayer()
	{
		return humanoidControl is LocalPlayerControl;
	}

	public void BeHit(int skillflag, int DamageHP, int skillId)
	{
	}

	public bool canReceiveImapct(int buffId)
	{
		return true;
		/*SkillBuff_Tbl buffTable = com.jdxk.Configs.ConfigPool.Instance.GetDataByKey<com.jdxk.Configs.SkillBuff_Tbl>(buffId);
		if (null == buffTable) {
			return false;
		}
		MonsterBase_Tbl monsterBase = com.jdxk.Configs.ConfigPool.Instance.GetDataByKey<MonsterBase_Tbl> (modelId);
		if (null == monsterBase) {
			return false;
		}
        MonsterAi_Tbl monsterAi = ConfigPool.Instance.GetDataByKey<MonsterAi_Tbl>(monsterBase.monsterAi);
		if (null == monsterAi) {
			return false;
		}

        bool bRet = false;
		switch ((GameDefines.CHARACTER)monsterAi.characterType) {
		case GameDefines.CHARACTER.Char_Passive:
			bRet = true;
			break;

        case GameDefines.CHARACTER.Char_Aggressive:
            bRet = true;
			break;
        case GameDefines.CHARACTER.Char_Standing:
            bRet = false;
			break;
        case GameDefines.CHARACTER.Char_Timid:
            bRet = false;
			break;
        case GameDefines.CHARACTER.Char_Pitfall:
            bRet = false;
			break;
		default:
			break;
		}
        return bRet;*/
	}

	virtual protected void OnSpawned()
	{
		SetTarget(false);
		Dead = false;
	}

	public void ForceAnimation(int stateNameHash, float duration)
	{
		if (humanoidControl != null && animator != null)
		{
			animator.CrossFade(stateNameHash, duration);
		}
	}
}
