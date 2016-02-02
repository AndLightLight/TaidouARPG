

using System;
using System.Collections;
using System.Collections.Generic;


namespace BuffSystem
{
	
	public class BuffSet
	{
		private int m_ownerID;
		private Dictionary<int, Buff> m_buffList;

		public void Init(int ownerID)
		{
			this.m_ownerID = ownerID;
			m_buffList.Clear();
		}

		public int AddBuff(BuffParam param)
		{
			return 0;
		}

		public void RemoveBuff(int templateID)
		{

		}

	}

}