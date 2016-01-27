

using System;
using System.Collections;
using System.Collections.Generic;


public class ItemManager
{
	private Dictionary<long, ItemData> itemList = new Dictionary<long,ItemData>();

	public bool LoadItems()
	{
		return true;
	}

	public bool SaveItems()
	{
		return true;
	}

	public ItemData FindItem(long guid)
	{
		return null;
	}

	public ItemData FindItem(int templateID)
	{
		return null;
	}

	public ItemData AddItem(int templateID, int count)
	{
		return null;
	}

	public void RemoveItem(long guid)
	{

	}

	public void UseItem(long guid, int count)
	{

	}

}