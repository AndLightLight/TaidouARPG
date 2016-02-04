

namespace SkillSystem
{
	public class Skill
	{

		#region Êý¾Ý

		private int m_ownerID;

		private Skill_Tbl m_template;
		public Skill_Tbl Template { get { return m_template; } }

		private float m_CDTimeLeft;

		private float m_delayTimeLeft;

		#endregion

		public void Init(int ownerID, int templateID)
		{
			this.m_ownerID = ownerID;
			m_template = TemplatePool.Instance.GetDataByKey<Skill_Tbl>(templateID);
			if (m_template != null)
			{
				m_CDTimeLeft = m_template.CD;
				m_delayTimeLeft = 0;
			}
		}

		public bool CanTrigger(int target)
		{
			return true;
		}

		public bool Trigger(int owner, int target)
		{
			return false;
		}

		public void Update(float deltaTime)
		{
		}

	}
}