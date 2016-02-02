
using System;
using System.Collections;
using System.Collections.Generic;


namespace SkillSystem
{
	class SkillManager : Singleton<SkillManager>
	{
		#region ����

		private Dictionary<int, SkillSet> m_skillSetList = new Dictionary<int, SkillSet>();

		#endregion


		#region ���ء�ж�ؼ���

		public void Init()
		{
			
		}

		public bool LoadSkill(int ownerID)
		{
			if (m_skillSetList.ContainsKey(ownerID))
			{
				LogManager.Log("LoadSkill : repeated load. Owner : " + ownerID, LogType.Error);
				return false;
			}

			SkillSet skillSet = new SkillSet();
			skillSet.Init(ownerID);
			m_skillSetList.Add(ownerID, skillSet);

			Skill skill = new Skill();
			skill.Init(ownerID, 111);
			skillSet.AddSkill(skill);
			
			return false;
		}

		public bool SaveSkill(int ownerID)
		{
			return false;
		}

		#endregion


		#region ʹ�ü���

		public bool CanUseSkill(int ownerID, SkillParam skillParam)
		{
			Skill skill = GetSkill(ownerID, skillParam.templateID);
			if (null == skill)
			{
				LogManager.Log("CanUseSkill : skill not exist. id : " + skillParam.templateID, LogType.Error);
				return false;
			}

			// to do : check can use ?

			ISkillLogic logic = SkillLogicManager.GetSkillLogic(skill.Template.id);
			if (logic != null)
			{
				;
			}

			return false;
		}

		public bool UseSkill(int ownerID, SkillParam skillParam)
		{
			Skill skill = GetSkill(ownerID, skillParam.templateID);
			if (null == skill)
			{
				LogManager.Log("UseSkill : skill not exist. id : " + skillParam.templateID, LogType.Error);
				return false;
			}

			ISkillLogic logic = SkillLogicManager.GetSkillLogic(skill.Template.id);
			if (logic != null)
			{
				logic.OnActive(ownerID, skillParam);
			}

			return false;
		}

		#endregion


		public void Update(float deltaTime)
		{
			foreach(var value in m_skillSetList.Values)
			{
				value.Update(deltaTime);
			}
		}

		public Skill GetSkill(int ownerID, int skillTemplateID)
		{
			if (m_skillSetList.ContainsKey(ownerID))
			{
				Skill skill = m_skillSetList[ownerID].GetSkill(skillTemplateID);
				return skill;
			}

			return null;
		}

		public void RemoveSkill(int ownerID, int skillTemplateID)
		{
			if (m_skillSetList.ContainsKey(ownerID))
			{
				m_skillSetList[ownerID].RemoveSkill(skillTemplateID);
			}
		}

		public void RemoveSkills(int ownerID)
		{
			if (m_skillSetList.ContainsKey(ownerID))
			{
				m_skillSetList.Remove(ownerID);
			}
		}

		public void RemoveAll()
		{
			m_skillSetList.Clear();
		}

	}
}