
using System;
using System.Collections;
using System.Collections.Generic;


namespace SkillSystem
{
	public class SkillSet
	{
		#region Êý¾Ý

		private Dictionary<int, Skill> m_skillList = new Dictionary<int, Skill>();

		private int m_ownerID;

		#endregion

		public void Init(int ownerID)
		{
			this.m_ownerID = ownerID;
		}

		public void Update(float deltaTime)
		{
			foreach (var value in m_skillList.Values)
			{
				value.Update(deltaTime);
			}
		}

		public void AddSkill(Skill skill)
		{
			if (null == skill)
				return;

			if (m_skillList.ContainsKey(skill.Template.id))
			{
				LogManager.Log("AddSkill : skill repeated. id : " + skill.Template.id, LogType.Error);
				return;
			}

			m_skillList.Add(skill.Template.id, skill);
		}

		public void RemoveSkill(int templateID)
		{
			if (!m_skillList.ContainsKey(templateID))
			{
				LogManager.Log("RemoveSkill : skill not exist. id : " + templateID, LogType.Error);
				return;
			}

			m_skillList.Remove(templateID);
		}

		public Skill GetSkill(int skillTemplateID)
		{
			if (m_skillList.ContainsKey(skillTemplateID))
				return m_skillList[skillTemplateID];
			else
				return null;
		}

	}
}