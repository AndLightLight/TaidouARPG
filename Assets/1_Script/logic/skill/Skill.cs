


public class Skill
{
	private int m_ownerID;

	public bool CanTrigger(int target)
	{
		return true;
	}

	public bool Trigger(int owner, int target)
	{
        return false;
	}
}