

namespace Buff
{
	public class IBuffLogic
	{
		public void Active()
		{
			this.OnActive();
		}

		public void Deactive()
		{
			this.OnDeactive();
		}

		protected virtual void OnActive() { }

		protected virtual void OnDeactive() { }

	}
}