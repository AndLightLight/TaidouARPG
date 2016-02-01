

public class ItemData
{
	public long m_guid;
	public int m_templateID;
	public int m_count;
	public int m_obtainedTime;
	public int m_loss;

	public Item_Tbl m_template;

	public ItemData()
	{

	}

	public ItemData(ItemData srcData)
	{
		this.Init(srcData.m_guid, srcData.m_templateID, srcData.m_count, srcData.m_obtainedTime, srcData.m_loss);
	}

	public void Init(long guid, int templateID, int count, int obainTime, int loss)
	{
		this.m_guid = guid;
		this.m_templateID = templateID;
		this.m_count = count;
		this.m_obtainedTime = obainTime;
		this.m_loss = loss;

		if (null == m_template || m_template.id != templateID)
		{
			m_template = TemplatePool.Instance.GetDataByKey<Item_Tbl>(this.m_templateID);
		}
	}
	
}