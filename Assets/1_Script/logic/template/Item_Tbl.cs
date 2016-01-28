using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

	public class Item_Tbl : BaseConfig
	{
		public Item_Tbl()
		{

		}

		string _name;	//name
		public string name { get { return _name;} }

		string _discribe;	//discribe
		public string discribe { get { return _discribe;} }

		int _quality;	//quality
		public int quality { get { return _quality;} }

		int _level;	//level
		public int level { get { return _level;} }

		int _type;	//type
		public int type { get { return _type;} }

		int _subType;	//subType
		public int subType { get { return _subType;} }

		int _profession;	//profession
		public int profession { get { return _profession;} }

		int _composition;	//composition
		public int composition { get { return _composition;} }

		string _icon;	//icon
		public string icon { get { return _icon;} }

		int _coolGroup;	//coolGroup
		public int coolGroup { get { return _coolGroup;} }

		int _cooling;	//cooling
		public int cooling { get { return _cooling;} }

		int _useResult;	//useResult
		public int useResult { get { return _useResult;} }

		int _timingMode;	//timingMode
		public int timingMode { get { return _timingMode;} }

		int _valid;	//valid
		public int valid { get { return _valid;} }

		int _maxOwnNum;	//maxOwnNum
		public int maxOwnNum { get { return _maxOwnNum;} }

		int _state;	//state
		public int state { get { return _state;} }

		public override void init(JObject json)
		{
			base.init(json);

			if(json["name"] == null)
				_name = "";
			else
				_name = json["name"].ToString();

			if(json["discribe"] == null)
				_discribe = "";
			else
				_discribe = json["discribe"].ToString();

			if(json["quality"] == null)
				_quality = 0;
			else
				int.TryParse(json["quality"].ToString(), out _quality);

			if(json["level"] == null)
				_level = 0;
			else
				int.TryParse(json["level"].ToString(), out _level);

			if(json["type"] == null)
				_type = 0;
			else
				int.TryParse(json["type"].ToString(), out _type);

			if(json["subType"] == null)
				_subType = 0;
			else
				int.TryParse(json["subType"].ToString(), out _subType);

			if(json["profession"] == null)
				_profession = 0;
			else
				int.TryParse(json["profession"].ToString(), out _profession);

			if(json["composition"] == null)
				_composition = 0;
			else
				int.TryParse(json["composition"].ToString(), out _composition);

			if(json["icon"] == null)
				_icon = "";
			else
				_icon = json["icon"].ToString();

			if(json["coolGroup"] == null)
				_coolGroup = -1;
			else
				int.TryParse(json["coolGroup"].ToString(), out _coolGroup);

			if(json["cooling"] == null)
				_cooling = -1;
			else
				int.TryParse(json["cooling"].ToString(), out _cooling);

			if(json["useResult"] == null)
				_useResult = 0;
			else
				int.TryParse(json["useResult"].ToString(), out _useResult);

			if(json["timingMode"] == null)
				_timingMode = 0;
			else
				int.TryParse(json["timingMode"].ToString(), out _timingMode);

			if(json["valid"] == null)
				_valid = 0;
			else
				int.TryParse(json["valid"].ToString(), out _valid);

			if(json["maxOwnNum"] == null)
				_maxOwnNum = -1;
			else
				int.TryParse(json["maxOwnNum"].ToString(), out _maxOwnNum);

			if(json["state"] == null)
				_state = 0;
			else
				int.TryParse(json["state"].ToString(), out _state);

		}
	}
