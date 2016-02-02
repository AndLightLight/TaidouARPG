

namespace SkillSystem
{
	public class ISkillLogic
	{
		public virtual void OnActive(int ownerID, SkillParam skillParam) { }

		public virtual void OnDeactive() { }

		public virtual void OnEffect() { }

	}
}