using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

	public class Buff_Tbl : BaseConfig
	{
		public Buff_Tbl()
		{

		}

		int _name;	//name
		public int name { get { return _name;} }

		int _discribe;	//discribe
		public int discribe { get { return _discribe;} }

		string _icon;	//icon
		public string icon { get { return _icon;} }

		int _showPos;	//showPos
		public int showPos { get { return _showPos;} }

		int _target;	//target
		public int target { get { return _target;} }

		int _targetCount;	//targetCount
		public int targetCount { get { return _targetCount;} }

		bool _isBullet;	//isBullet
		public bool isBullet { get { return _isBullet;} }

		int _range;	//range
		public int range { get { return _range;} }

		int _rangePar1;	//rangePar1
		public int rangePar1 { get { return _rangePar1;} }

		int _rangePar2;	//rangePar2
		public int rangePar2 { get { return _rangePar2;} }

		int _rangeLast;	//rangeLast
		public int rangeLast { get { return _rangeLast;} }

		int _buffLast;	//buffLast
		public int buffLast { get { return _buffLast;} }

		int _hitCount;	//hitCount
		public int hitCount { get { return _hitCount;} }

		int _buffType;	//buffType
		public int buffType { get { return _buffType;} }

		int[] _typePar1 = new int[3];	//typePar1
		public int[] typePar1 { get { return _typePar1;} }

		int[] _typePar2 = new int[3];	//typePar2
		public int[] typePar2 { get { return _typePar2;} }

		int[] _typePar3 = new int[3];	//typePar3
		public int[] typePar3 { get { return _typePar3;} }

		int[] _typePar4 = new int[3];	//typePar4
		public int[] typePar4 { get { return _typePar4;} }

		int[] _typePar5 = new int[3];	//typePar5
		public int[] typePar5 { get { return _typePar5;} }

		int[] _typePar6 = new int[3];	//typePar6
		public int[] typePar6 { get { return _typePar6;} }

		int[] _beginShow = new int[3];	//beginShow
		public int[] beginShow { get { return _beginShow;} }

		int[] _endShow = new int[3];	//endShow
		public int[] endShow { get { return _endShow;} }

		int _isTriggered;	//isTriggered
		public int isTriggered { get { return _isTriggered;} }

		int[] _triggerBuffId = new int[10];	//triggerBuffId
		public int[] triggerBuffId { get { return _triggerBuffId;} }

		int _ifMutex;	//ifMutex
		public int ifMutex { get { return _ifMutex;} }

		int[] _mutexBuffId = new int[10];	//mutexBuffId
		public int[] mutexBuffId { get { return _mutexBuffId;} }

		int[] _mutexBuffType = new int[10];	//mutexBuffType
		public int[] mutexBuffType { get { return _mutexBuffType;} }

		int _clearType;	//clearType
		public int clearType { get { return _clearType;} }

		int[] _killBuffId = new int[10];	//killBuffId
		public int[] killBuffId { get { return _killBuffId;} }

		int[] _killBuffType = new int[10];	//killBuffType
		public int[] killBuffType { get { return _killBuffType;} }

		public override void init(JObject json)
		{
			base.init(json);

			if(json["name"] == null)
				_name = 0;
			else
				int.TryParse(json["name"].ToString(), out _name);

			if(json["discribe"] == null)
				_discribe = 0;
			else
				int.TryParse(json["discribe"].ToString(), out _discribe);

			if(json["icon"] == null)
				_icon = "";
			else
				_icon = json["icon"].ToString();

			if(json["showPos"] == null)
				_showPos = 0;
			else
				int.TryParse(json["showPos"].ToString(), out _showPos);

			if(json["target"] == null)
				_target = 1;
			else
				int.TryParse(json["target"].ToString(), out _target);

			if(json["targetCount"] == null)
				_targetCount = 0;
			else
				int.TryParse(json["targetCount"].ToString(), out _targetCount);

			if(json["isBullet"] == null)
				_isBullet = false;
			else
				bool.TryParse(json["isBullet"].ToString(), out _isBullet);

			if(json["range"] == null)
				_range = 0;
			else
				int.TryParse(json["range"].ToString(), out _range);

			if(json["rangePar1"] == null)
				_rangePar1 = 0;
			else
				int.TryParse(json["rangePar1"].ToString(), out _rangePar1);

			if(json["rangePar2"] == null)
				_rangePar2 = 0;
			else
				int.TryParse(json["rangePar2"].ToString(), out _rangePar2);

			if(json["rangeLast"] == null)
				_rangeLast = 0;
			else
				int.TryParse(json["rangeLast"].ToString(), out _rangeLast);

			if(json["buffLast"] == null)
				_buffLast = 0;
			else
				int.TryParse(json["buffLast"].ToString(), out _buffLast);

			if(json["hitCount"] == null)
				_hitCount = 0;
			else
				int.TryParse(json["hitCount"].ToString(), out _hitCount);

			if(json["buffType"] == null)
				_buffType = 1;
			else
				int.TryParse(json["buffType"].ToString(), out _buffType);

			if(json["typePar1"] != null)
			{
				int typePar1Count = (json["typePar1"].ToString()).Split(',').Length;
				for(int i=0; i<3; i++){
					if(i < typePar1Count)
						int.TryParse((json["typePar1"].ToString()).Split(',')[i], out _typePar1[i]);
					else
						_typePar1[i] = 1;
				}
			}
			else
			{
				for(int i=0; i<3; i++)
					_typePar1[i] = 1;
			}

			if(json["typePar2"] != null)
			{
				int typePar2Count = (json["typePar2"].ToString()).Split(',').Length;
				for(int i=0; i<3; i++){
					if(i < typePar2Count)
						int.TryParse((json["typePar2"].ToString()).Split(',')[i], out _typePar2[i]);
					else
						_typePar2[i] = 0;
				}
			}
			else
			{
				for(int i=0; i<3; i++)
					_typePar2[i] = 0;
			}

			if(json["typePar3"] != null)
			{
				int typePar3Count = (json["typePar3"].ToString()).Split(',').Length;
				for(int i=0; i<3; i++){
					if(i < typePar3Count)
						int.TryParse((json["typePar3"].ToString()).Split(',')[i], out _typePar3[i]);
					else
						_typePar3[i] = 0;
				}
			}
			else
			{
				for(int i=0; i<3; i++)
					_typePar3[i] = 0;
			}

			if(json["typePar4"] != null)
			{
				int typePar4Count = (json["typePar4"].ToString()).Split(',').Length;
				for(int i=0; i<3; i++){
					if(i < typePar4Count)
						int.TryParse((json["typePar4"].ToString()).Split(',')[i], out _typePar4[i]);
					else
						_typePar4[i] = 0;
				}
			}
			else
			{
				for(int i=0; i<3; i++)
					_typePar4[i] = 0;
			}

			if(json["typePar5"] != null)
			{
				int typePar5Count = (json["typePar5"].ToString()).Split(',').Length;
				for(int i=0; i<3; i++){
					if(i < typePar5Count)
						int.TryParse((json["typePar5"].ToString()).Split(',')[i], out _typePar5[i]);
					else
						_typePar5[i] = 0;
				}
			}
			else
			{
				for(int i=0; i<3; i++)
					_typePar5[i] = 0;
			}

			if(json["typePar6"] != null)
			{
				int typePar6Count = (json["typePar6"].ToString()).Split(',').Length;
				for(int i=0; i<3; i++){
					if(i < typePar6Count)
						int.TryParse((json["typePar6"].ToString()).Split(',')[i], out _typePar6[i]);
					else
						_typePar6[i] = 0;
				}
			}
			else
			{
				for(int i=0; i<3; i++)
					_typePar6[i] = 0;
			}

			if(json["beginShow"] != null)
			{
				int beginShowCount = (json["beginShow"].ToString()).Split(',').Length;
				for(int i=0; i<3; i++){
					if(i < beginShowCount)
						int.TryParse((json["beginShow"].ToString()).Split(',')[i], out _beginShow[i]);
					else
						_beginShow[i] = 0;
				}
			}
			else
			{
				for(int i=0; i<3; i++)
					_beginShow[i] = 0;
			}

			if(json["endShow"] != null)
			{
				int endShowCount = (json["endShow"].ToString()).Split(',').Length;
				for(int i=0; i<3; i++){
					if(i < endShowCount)
						int.TryParse((json["endShow"].ToString()).Split(',')[i], out _endShow[i]);
					else
						_endShow[i] = 0;
				}
			}
			else
			{
				for(int i=0; i<3; i++)
					_endShow[i] = 0;
			}

			if(json["isTriggered"] == null)
				_isTriggered = 0;
			else
				int.TryParse(json["isTriggered"].ToString(), out _isTriggered);

			if(json["triggerBuffId"] != null)
			{
				int triggerBuffIdCount = (json["triggerBuffId"].ToString()).Split(',').Length;
				for(int i=0; i<10; i++){
					if(i < triggerBuffIdCount)
						int.TryParse((json["triggerBuffId"].ToString()).Split(',')[i], out _triggerBuffId[i]);
					else
						_triggerBuffId[i] = 0;
				}
			}
			else
			{
				for(int i=0; i<10; i++)
					_triggerBuffId[i] = 0;
			}

			if(json["ifMutex"] == null)
				_ifMutex = 0;
			else
				int.TryParse(json["ifMutex"].ToString(), out _ifMutex);

			if(json["mutexBuffId"] != null)
			{
				int mutexBuffIdCount = (json["mutexBuffId"].ToString()).Split(',').Length;
				for(int i=0; i<10; i++){
					if(i < mutexBuffIdCount)
						int.TryParse((json["mutexBuffId"].ToString()).Split(',')[i], out _mutexBuffId[i]);
					else
						_mutexBuffId[i] = 0;
				}
			}
			else
			{
				for(int i=0; i<10; i++)
					_mutexBuffId[i] = 0;
			}

			if(json["mutexBuffType"] != null)
			{
				int mutexBuffTypeCount = (json["mutexBuffType"].ToString()).Split(',').Length;
				for(int i=0; i<10; i++){
					if(i < mutexBuffTypeCount)
						int.TryParse((json["mutexBuffType"].ToString()).Split(',')[i], out _mutexBuffType[i]);
					else
						_mutexBuffType[i] = 0;
				}
			}
			else
			{
				for(int i=0; i<10; i++)
					_mutexBuffType[i] = 0;
			}

			if(json["clearType"] == null)
				_clearType = 0;
			else
				int.TryParse(json["clearType"].ToString(), out _clearType);

			if(json["killBuffId"] != null)
			{
				int killBuffIdCount = (json["killBuffId"].ToString()).Split(',').Length;
				for(int i=0; i<10; i++){
					if(i < killBuffIdCount)
						int.TryParse((json["killBuffId"].ToString()).Split(',')[i], out _killBuffId[i]);
					else
						_killBuffId[i] = 0;
				}
			}
			else
			{
				for(int i=0; i<10; i++)
					_killBuffId[i] = 0;
			}

			if(json["killBuffType"] != null)
			{
				int killBuffTypeCount = (json["killBuffType"].ToString()).Split(',').Length;
				for(int i=0; i<10; i++){
					if(i < killBuffTypeCount)
						int.TryParse((json["killBuffType"].ToString()).Split(',')[i], out _killBuffType[i]);
					else
						_killBuffType[i] = 0;
				}
			}
			else
			{
				for(int i=0; i<10; i++)
					_killBuffType[i] = 0;
			}

		}
	}
