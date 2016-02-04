

namespace SkillSystem
{
	enum SkillLogicType
	{
		Min = 0,
		Buff,
		Count,
	}

	public class SkillLogicManager
	{
		private static ISkillLogic[] m_logics = new ISkillLogic[]
			{
				new ISkillLogic(),
				new SkillLogicBuff()
			};

		public static ISkillLogic GetSkillLogic(int id)
		{
			if (id > (int)SkillLogicType.Min && id < (int)SkillLogicType.Count)
			{
				return m_logics[id];
			}

			return null;
		}
	}
}