

namespace Skill
{
	public class ISkillLogic
	{
		public void Active()
		{
			this.OnActive();
		}

		public void Deactive()
		{
			this.OnDeactive();
		}

		public void Effect()
		{
			this.OnEffect();
		}

		protected virtual void OnActive() { }

		protected virtual void OnDeactive() { }

		protected virtual void OnEffect() { }

	}
}