

namespace ItemDefines
{
	// ��Ʒ���ࣺװ����ʳ����ߡ�����
	enum ItemType
	{
		Equip,
		Food,
		Tool,
		Material,
	}

	// װ��С�ࣺñ�ӡ��������·�������
	enum EquipType
	{
		Hat,
		Weapon,
		Dress,
		Ear,
	}

	// ʳ��С�ࣺ�������ӡ���ʵ���⡢����
	enum FoodType
	{
		Flower,
		Seed,
		Fruit,
		Meat,
		Animal,
	}

	// ����С�ࣺ��ͷ��ì����
	enum ToolType
	{
		Axe,
	}

	// ����С�ࣺ�ݡ�ԭľ��ʯ�����
	enum MaterialType
	{
		Grass,
		Wood,
		Stone,
		Robot,
	}

	// ʹ����Ʒ�����ķ���ֵ
	enum UseItemRet
	{
		Success = 0,
		Error = 1,
		LowCount,
		InCD,
		LowLevel,
	}

}