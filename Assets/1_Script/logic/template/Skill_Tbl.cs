using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

	public class Skill_Tbl : BaseConfig
	{
		public Skill_Tbl()
		{

		}

		int _motionId;	//motionId
		public int motionId { get { return _motionId;} }

		int _logicId;	//logicId
		public int logicId { get { return _logicId;} }

		string _skillName;	//skillName
		public string skillName { get { return _skillName;} }

		string _detailInfo;	//detailInfo
		public string detailInfo { get { return _detailInfo;} }

		int _index;	//技能升级面板的排列顺序与CD组
		public int index { get { return _index;} }

		string _description;	//description
		public string description { get { return _description;} }

		int _aniID;	//AniID
		public int aniID { get { return _aniID;} }

		int _careerId;	//careerId
		public int careerId { get { return _careerId;} }

		int _skillType;	//skillType
		public int skillType { get { return _skillType;} }

		int _skillSubType;	//skillSubType
		public int skillSubType { get { return _skillSubType;} }

		int _skillClassify;	//skillClassify
		public int skillClassify { get { return _skillClassify;} }

		int _pileId;	//pileId
		public int pileId { get { return _pileId;} }

		int _isDragAble;	//isDragAble
		public int isDragAble { get { return _isDragAble;} }

		string _icon;	//icon
		public string icon { get { return _icon;} }

		int _target;	//target
		public int target { get { return _target;} }

		int _castDistance;	//castDistance
		public int castDistance { get { return _castDistance;} }

		int _MPcost;	//MPcost
		public int MPcost { get { return _MPcost;} }

		int _rageCost;	//rageCost
		public int rageCost { get { return _rageCost;} }

		int _CD;	//CD
		public int CD { get { return _CD;} }

		int _combatPower;	//combatPower
		public int combatPower { get { return _combatPower;} }

		int _level;	//level
		public int level { get { return _level;} }

		int[] _skillLevel = new int[2];	//skillLevel
		public int[] skillLevel { get { return _skillLevel;} }

		int _preSkill;	//preSkill
		public int preSkill { get { return _preSkill;} }

		int _aftSkill;	//aftSkill
		public int aftSkill { get { return _aftSkill;} }

		int _costMoney;	//costMoney
		public int costMoney { get { return _costMoney;} }

		int _moneyCount;	//moneyCount
		public int moneyCount { get { return _moneyCount;} }

		int _costSpirit;	//costSpirit
		public int costSpirit { get { return _costSpirit;} }

		int _spiritCount;	//spiritCount
		public int spiritCount { get { return _spiritCount;} }

		int _costItem;	//costItem
		public int costItem { get { return _costItem;} }

		int _itemCount;	//itemCount
		public int itemCount { get { return _itemCount;} }

		int _itemPrice;	//itemPrice
		public int itemPrice { get { return _itemPrice;} }

		int _rageInc;	//rageInc
		public int rageInc { get { return _rageInc;} }

		int[] _buff1 = new int[3];	//buff1
		public int[] buff1 { get { return _buff1;} }

		int[] _buff2 = new int[3];	//buff2
		public int[] buff2 { get { return _buff2;} }

		int[] _buff3 = new int[3];	//buff3
		public int[] buff3 { get { return _buff3;} }

		int[] _effect1 = new int[5];	//effect1
		public int[] effect1 { get { return _effect1;} }

		int[] _effect2 = new int[5];	//effect2
		public int[] effect2 { get { return _effect2;} }

		int[] _effect3 = new int[5];	//effect3
		public int[] effect3 { get { return _effect3;} }

		public override void init(JObject json)
		{
			base.init(json);

			if(json["motionId"] == null)
				_motionId = 0;
			else
				int.TryParse(json["motionId"].ToString(), out _motionId);

			if(json["logicId"] == null)
				_logicId = 0;
			else
				int.TryParse(json["logicId"].ToString(), out _logicId);

			if(json["skillName"] == null)
				_skillName = "";
			else
				_skillName = json["skillName"].ToString();

			if(json["detailInfo"] == null)
				_detailInfo = "";
			else
				_detailInfo = json["detailInfo"].ToString();

			if(json["index"] == null)
				_index = -1;
			else
				int.TryParse(json["index"].ToString(), out _index);

			if(json["description"] == null)
				_description = "";
			else
				_description = json["description"].ToString();

			if(json["aniID"] == null)
				_aniID = 0;
			else
				int.TryParse(json["aniID"].ToString(), out _aniID);

			if(json["careerId"] == null)
				_careerId = 0;
			else
				int.TryParse(json["careerId"].ToString(), out _careerId);

			if(json["skillType"] == null)
				_skillType = 0;
			else
				int.TryParse(json["skillType"].ToString(), out _skillType);

			if(json["skillSubType"] == null)
				_skillSubType = 0;
			else
				int.TryParse(json["skillSubType"].ToString(), out _skillSubType);

			if(json["skillClassify"] == null)
				_skillClassify = 0;
			else
				int.TryParse(json["skillClassify"].ToString(), out _skillClassify);

			if(json["pileId"] == null)
				_pileId = 0;
			else
				int.TryParse(json["pileId"].ToString(), out _pileId);

			if(json["isDragAble"] == null)
				_isDragAble = 0;
			else
				int.TryParse(json["isDragAble"].ToString(), out _isDragAble);

			if(json["icon"] == null)
				_icon = "";
			else
				_icon = json["icon"].ToString();

			if(json["target"] == null)
				_target = 0;
			else
				int.TryParse(json["target"].ToString(), out _target);

			if(json["castDistance"] == null)
				_castDistance = 0;
			else
				int.TryParse(json["castDistance"].ToString(), out _castDistance);

			if(json["MPcost"] == null)
				_MPcost = 0;
			else
				int.TryParse(json["MPcost"].ToString(), out _MPcost);

			if(json["rageCost"] == null)
				_rageCost = 0;
			else
				int.TryParse(json["rageCost"].ToString(), out _rageCost);

			if(json["CD"] == null)
				_CD = 0;
			else
				int.TryParse(json["CD"].ToString(), out _CD);

			if(json["combatPower"] == null)
				_combatPower = 0;
			else
				int.TryParse(json["combatPower"].ToString(), out _combatPower);

			if(json["level"] == null)
				_level = 0;
			else
				int.TryParse(json["level"].ToString(), out _level);

			if(json["skillLevel"] != null)
			{
				int skillLevelCount = (json["skillLevel"].ToString()).Split(',').Length;
				for(int i=0; i<2; i++){
					if(i < skillLevelCount)
						int.TryParse((json["skillLevel"].ToString()).Split(',')[i], out _skillLevel[i]);
					else
						_skillLevel[i] = 0;
				}
			}
			else
			{
				for(int i=0; i<2; i++)
					_skillLevel[i] = 0;
			}

			if(json["preSkill"] == null)
				_preSkill = 0;
			else
				int.TryParse(json["preSkill"].ToString(), out _preSkill);

			if(json["aftSkill"] == null)
				_aftSkill = 0;
			else
				int.TryParse(json["aftSkill"].ToString(), out _aftSkill);

			if(json["costMoney"] == null)
				_costMoney = 0;
			else
				int.TryParse(json["costMoney"].ToString(), out _costMoney);

			if(json["moneyCount"] == null)
				_moneyCount = 0;
			else
				int.TryParse(json["moneyCount"].ToString(), out _moneyCount);

			if(json["costSpirit"] == null)
				_costSpirit = 0;
			else
				int.TryParse(json["costSpirit"].ToString(), out _costSpirit);

			if(json["spiritCount"] == null)
				_spiritCount = 0;
			else
				int.TryParse(json["spiritCount"].ToString(), out _spiritCount);

			if(json["costItem"] == null)
				_costItem = 0;
			else
				int.TryParse(json["costItem"].ToString(), out _costItem);

			if(json["itemCount"] == null)
				_itemCount = 0;
			else
				int.TryParse(json["itemCount"].ToString(), out _itemCount);

			if(json["itemPrice"] == null)
				_itemPrice = 0;
			else
				int.TryParse(json["itemPrice"].ToString(), out _itemPrice);

			if(json["rageInc"] == null)
				_rageInc = 0;
			else
				int.TryParse(json["rageInc"].ToString(), out _rageInc);

			if(json["buff1"] != null)
			{
				int buff1Count = (json["buff1"].ToString()).Split(',').Length;
				for(int i=0; i<3; i++){
					if(i < buff1Count)
						int.TryParse((json["buff1"].ToString()).Split(',')[i], out _buff1[i]);
					else
						_buff1[i] = 0;
				}
			}
			else
			{
				for(int i=0; i<3; i++)
					_buff1[i] = 0;
			}

			if(json["buff2"] != null)
			{
				int buff2Count = (json["buff2"].ToString()).Split(',').Length;
				for(int i=0; i<3; i++){
					if(i < buff2Count)
						int.TryParse((json["buff2"].ToString()).Split(',')[i], out _buff2[i]);
					else
						_buff2[i] = 0;
				}
			}
			else
			{
				for(int i=0; i<3; i++)
					_buff2[i] = 0;
			}

			if(json["buff3"] != null)
			{
				int buff3Count = (json["buff3"].ToString()).Split(',').Length;
				for(int i=0; i<3; i++){
					if(i < buff3Count)
						int.TryParse((json["buff3"].ToString()).Split(',')[i], out _buff3[i]);
					else
						_buff3[i] = 0;
				}
			}
			else
			{
				for(int i=0; i<3; i++)
					_buff3[i] = 0;
			}

			if(json["effect1"] != null)
			{
				int effect1Count = (json["effect1"].ToString()).Split(',').Length;
				for(int i=0; i<5; i++){
					if(i < effect1Count)
						int.TryParse((json["effect1"].ToString()).Split(',')[i], out _effect1[i]);
					else
						_effect1[i] = 0;
				}
			}
			else
			{
				for(int i=0; i<5; i++)
					_effect1[i] = 0;
			}

			if(json["effect2"] != null)
			{
				int effect2Count = (json["effect2"].ToString()).Split(',').Length;
				for(int i=0; i<5; i++){
					if(i < effect2Count)
						int.TryParse((json["effect2"].ToString()).Split(',')[i], out _effect2[i]);
					else
						_effect2[i] = 0;
				}
			}
			else
			{
				for(int i=0; i<5; i++)
					_effect2[i] = 0;
			}

			if(json["effect3"] != null)
			{
				int effect3Count = (json["effect3"].ToString()).Split(',').Length;
				for(int i=0; i<5; i++){
					if(i < effect3Count)
						int.TryParse((json["effect3"].ToString()).Split(',')[i], out _effect3[i]);
					else
						_effect3[i] = 0;
				}
			}
			else
			{
				for(int i=0; i<5; i++)
					_effect3[i] = 0;
			}

		}
	}
