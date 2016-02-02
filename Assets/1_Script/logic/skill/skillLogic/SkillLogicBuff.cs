
using BuffSystem;

namespace SkillSystem
{
	public class SkillLogicBuff : ISkillLogic
	{
		public override void OnActive(int ownerID, SkillParam skillParam)
		{
			if (skillParam.templateID <= 0)
				return;

			Skill_Tbl skillTbl = TemplatePool.Instance.GetDataByKey<Skill_Tbl>(skillParam.templateID);
			if (null == skillTbl)
			{
				LogManager.Log("SkillLogicBuff.OnActive : invalid skill id.", LogType.Error);
				return;
			}

			for (int i = 0; i < skillTbl.buff1.Length; ++i)
			{
				if (skillTbl.buff1[i] <= 0)
					continue;

				BuffParam buffParam = new BuffParam();
				buffParam.templateID = skillTbl.buff1[i];
				buffParam.senderID = ownerID;
				buffParam.targetID = skillParam.targetID;

				BuffManager.Instance.AddBuff(ownerID, buffParam);
			}

		}

		public override void OnDeactive()
		{

		}

		public override void OnEffect()
		{

		}

	}
}