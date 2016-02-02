

namespace BuffSystem
{
	public class Buff
	{
		private int					m_id;
		private bool				m_valid;
		private BuffParam		m_param;
		private Buff_Tbl		m_template;
		private int					m_birthTime;
		private float				m_lifeTime;
		private float				m_persistTime;

		public void Reset()
		{
			m_id = 0;
			m_valid = false;
			m_param = null;
			m_template = null;
			m_birthTime = 0;
			m_lifeTime = 0;
			m_persistTime = 0;
		}

		public void Init(int id, BuffParam param)
		{
			Reset();

			this.m_id = id;
			m_template = TemplatePool.Instance.GetDataByKey<Buff_Tbl>(param.templateID);
			if (m_template != null)
			{

			}
		}

		public void Update(float deltaTime)
		{
		}

		public int GetID()
		{
			return this.m_id;
		}

		public Buff_Tbl GetTemplate()
		{
			return m_template;
		}

		public bool IsValid()
		{
			return m_valid;
		}

		public void SetValid(bool valid)
		{
			m_valid = valid;
		}

		public void Pile()
		{
			if (null == m_template)
				return;

			IBuffLogic logic = BuffLogicManager.GetBuffLogic(m_template.buffType);
			if (logic != null)
			{
				logic.OnActive();
			}
		}

	}
}