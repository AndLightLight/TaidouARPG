

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
		public int m_templateID;
		public Pos m_position = new Pos();
		public int m_target;
	}
}
