using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

	public class Character_Tbl : BaseConfig
	{
		public Character_Tbl()
		{

		}

		string _model;	//model
		public string model { get { return _model;} }

		public override void init(JObject json)
		{
			base.init(json);

			if(json["model"] == null)
				_model = "";
			else
				_model = json["model"].ToString();

		}
	}
