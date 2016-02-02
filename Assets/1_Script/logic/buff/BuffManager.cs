
using System;
using System.Collections;
using System.Collections.Generic;


namespace BuffSystem
{
	public class BuffManager : Singleton<BuffManager>
	{
		public Dictionary<int, BuffSet> m_buffsetList = new Dictionary<int, BuffSet>();

		public void Init()
		{
			m_buffsetList.Clear();
		}

		public void Update(float deltaTime)
		{
			foreach(var value in m_buffsetList.Values)
			{
				value.Update(deltaTime);
			}
		}

		public int AddBuff(int ownerID, BuffParam param)
		{
			BuffSet buffset = this.FindBuffSet(ownerID);
			if (null == buffset)
			{
				buffset = new BuffSet();
				buffset.Init(ownerID);
			}

			return buffset.AddBuff(param);
		}

		public void RemoveBuff(int ownerID, int buffTemplateID)
		{

		}

		public void RemoveAllBuff(int ownerID)
		{

		}

		public void Clear()
		{

		}

		BuffSet FindBuffSet(int ownerID)
		{
			if (m_buffsetList.ContainsKey(ownerID))
				return m_buffsetList[ownerID];
			else
				return null;
		}

	}
}