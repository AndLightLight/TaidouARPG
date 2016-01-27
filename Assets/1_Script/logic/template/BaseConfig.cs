using System;
using System.IO;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class BaseConfig
{
	private int _id = 0;
	public int id { get { return _id; } }

	private string _strId;
	public string strId { get { return _strId; } }

	public BaseConfig()
	{

	}

	public virtual void init(JObject json)
	{
		if (false == int.TryParse(json["id"].ToString(), out _id))
			_strId = json["id"].ToString();
	}

}
