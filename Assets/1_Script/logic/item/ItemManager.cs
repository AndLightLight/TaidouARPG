

using System;
using System.Collections;
using System.Collections.Generic;


public class ItemManager : Singleton<ItemManager>
{
	private Dictionary<long, ItemData> m_itemList = new Dictionary<long, ItemData>();

	private static long guidCounter = 110;

	private long GenerateGuid()
	{
		return guidCounter++;
	}

	public bool LoadItems(int ownerID)
	{
		// test
		ItemData data = new ItemData();
		data.Init(this.GenerateGuid(), 1, 123, 4444, 10);

		AddItem(data);

		return true;
	}

	public bool SaveItems(int ownerID)
	{
		return true;
	}

	public ItemData FindItem(long guid)
	{
		if (m_itemList.ContainsKey(guid))
			return m_itemList[guid];
		else
			return null;
	}

	public ItemData FindItem(int templateID)
	{
		foreach(var value in m_itemList.Values)
		{
			if (value.m_templateID == templateID)
				return value;
		}

		return null;
	}

	public ItemData AddItem(int templateID, int count)
	{
		ItemData data = FindItem(templateID);
		if (data != null)
		{
			data.m_count += count;
		}
		else
		{
			data = new ItemData();
			data.Init(this.GenerateGuid(), 1, 123, 4444, 10);
			m_itemList.Add(data.m_guid, data);
		}

		return data;
	}

	public bool AddItem(ItemData data)
	{
		if (m_itemList.ContainsKey(data.m_guid))
		{
			LogManager.Log("AddItem : repeated item. template : " + data.m_templateID, LogType.Error);
			return false;
		}
		else
		{
			m_itemList.Add(data.m_guid, data);
			return true;
		}
	}

	public void RemoveItem(long guid)
	{
		ItemData data = FindItem(guid);
		if (data != null)
		{
			m_itemList.Remove(data.m_guid);
		}
		else
		{
			LogManager.Log("RemoveItem : item not exist. guid : " + guid, LogType.Error);
		}
	}

	public void RemoveItem(int templateID)
	{
		ItemData data = FindItem(templateID);
		if (data != null)
		{
			m_itemList.Remove(data.m_guid);
		}
		else
		{
			LogManager.Log("RemoveItem : item not exist. template : " + templateID, LogType.Error);
		}
	}

	public void RemoveItem(ItemData data)
	{
		if (data != null)
		{
			if (m_itemList.ContainsValue(data))
				m_itemList.Remove(data.m_guid);
			else
				LogManager.Log("RemoveItem : item not exist. guid : " + data.m_guid, LogType.Error);
		}
		else
		{
			LogManager.Log("RemoveItem : data is null.", LogType.Error);
		}
	}

	public void UseItem(long guid, int count)
	{
		ItemData data = FindItem(guid);
		if (data != null)
		{
			int ret = CanUseItem(data, count);
			if (ret != 0)
			{
				return;
			}

			OnUseItem(data, count);
		}
		else
		{
			LogManager.Log("UseItem : item not exist. guid : " + guid, LogType.Error);
		}

	}

	public void UseItem(int templateID, int count)
	{
		ItemData data = FindItem(templateID);
		if (data != null)
		{
			int ret = CanUseItem(data, count);
			if (ret != 0)
			{
				return;
			}

			OnUseItem(data, count);
		}
		else
		{
			LogManager.Log("UseItem : item not exist. template : " + templateID, LogType.Error);
		}

	}

	private int CanUseItem(ItemData data, int count)
	{
		// to do : count, cd, level...
		int ret = (int)ItemDefines.UseItemRet.Success;

		do
		{
			if (data.m_count < count)
			{
				ret = (int)ItemDefines.UseItemRet.LowCount;
				break;
			}

		}
		while(false);

		return ret;
	}

	private void OnUseItem(ItemData data, int count)
	{
		data.m_count -= count;
		if (data.m_count == 0)
		{
			this.RemoveItem(data);
		}

		// to do : effect...


	}

}