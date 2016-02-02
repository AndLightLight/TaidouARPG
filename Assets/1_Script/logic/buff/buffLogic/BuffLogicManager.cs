

namespace BuffSystem
{
	enum BuffLogicType
	{
		Min = 0,
		AttrIncrease,
		AttrReduce,
		Count,
	}

	public class BuffLogicManager
	{
		private static IBuffLogic[] m_logics = new IBuffLogic[]
			{
				new IBuffLogic(),
				new BuffLogicAttrIncrease()
			};

		public static IBuffLogic GetBuffLogic(int id)
		{
			if (id > (int)BuffLogicType.Min && id < (int)BuffLogicType.Count)
			{
				return m_logics[id];
			}

			return null;
		}
	}
}