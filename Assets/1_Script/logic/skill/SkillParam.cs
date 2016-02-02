

namespace SkillSystem
{
	public struct Pos
	{
		public float x;
		public float y;
		public float z;
	}

	public class SkillParam
	{
		public int		templateID;
		public Pos		position = new Pos();
		public int		targetID;
	}
}
