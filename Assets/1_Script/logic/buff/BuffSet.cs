

using System;
using System.Collections;
using System.Collections.Generic;


namespace BuffSystem
{
	
	public class BuffSet
	{
		private int m_ownerID;
		private Dictionary<int, Buff> m_buffList = new Dictionary<int,Buff>();
		private List<Buff> m_buffInstPool = new List<Buff>();
		private int m_idCounter = 0;

		public void Init(int ownerID)
		{
			this.m_ownerID = ownerID;
			this.ClearAll();
		}

		public void Update(float deltaTime)
		{
			foreach (var value in m_buffList.Values)
			{
				value.Update(deltaTime);
			}
		}

		public int AddBuff(BuffParam param)
		{
			if (param.templateID <= 0)
				return (int)BuffDefines.ActionReult.Act_ParamError;

			Buff_Tbl buffTbl = TemplatePool.Instance.GetDataByKey<Buff_Tbl>(param.templateID);
			if (null == buffTbl)
				return (int)BuffDefines.ActionReult.Act_CfgError;

			// firstly cover same series
			foreach (var value in m_buffList.Values)
			{
				if (null == value)
					continue;

				Buff_Tbl buffTemp = value.GetTemplate();
				if (buffTemp.buffType == buffTbl.buffType)
				{
					if ((int)BuffDefines.CoverType.Cover == buffTbl.coverType)
					{
						CoverBuff(value, param);
						return (int)BuffDefines.ActionReult.Act_Success;
					}
				}
			}

			return DoAddBuff(param);
		}

		public void RemoveBuff(int buffInstID)
		{
			Buff buff = FindBuffInst(buffInstID);
			if (null != buff)
			{
				m_buffList.Remove(buff.GetID());
				m_buffInstPool.Add(buff);
			}
		}

		public void ClearAll()
		{
			foreach (var value in m_buffList.Values)
			{
				if (null != value)
					m_buffInstPool.Add(value);
			}

			m_buffList.Clear();
		}

		private Buff FindBuffInst(int buffInstID)
		{
			if (m_buffList.ContainsKey(buffInstID))
				return m_buffList[buffInstID];
			else
				return null;
		}

		private Buff GetFreeInst()
		{
			if (m_buffInstPool.Count != 0)
			{
				Buff buff = m_buffInstPool[0];
				buff.Reset();
				m_buffInstPool.RemoveAt(0);
				return buff;
			}
			else
			{
				return new Buff();
			}
		}

		private void CoverBuff(Buff oldBuff, BuffParam param)
		{
			RemoveBuff(oldBuff.GetID());
			DoAddBuff(param);
		}

		private int DoAddBuff(BuffParam param)
		{
			Buff buff = this.GetFreeInst();
			if (buff != null)
			{
				buff.Init(++m_idCounter, param);

				buff.Pile();

				return (int)BuffDefines.ActionReult.Act_Success;
			}

			return (int)BuffDefines.ActionReult.Act_Falied;
		}

	}

}