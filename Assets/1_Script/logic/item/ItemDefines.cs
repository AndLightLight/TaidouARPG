

namespace ItemDefines
{
	// 物品大类：装备、食物、工具、材料
	enum ItemType
	{
		Equip,
		Food,
		Tool,
		Material,
	}

	// 装备小类：帽子、武器、衣服、耳朵
	enum EquipType
	{
		Hat,
		Weapon,
		Dress,
		Ear,
	}

	// 食物小类：花、种子、果实、肉、活物
	enum FoodType
	{
		Flower,
		Seed,
		Fruit,
		Meat,
		Animal,
	}

	// 工具小类：斧头、矛、凿
	enum ToolType
	{
		Axe,
	}

	// 材料小类：草、原木、石、零件
	enum MaterialType
	{
		Grass,
		Wood,
		Stone,
		Robot,
	}

	// 使用物品操作的返回值
	enum UseItemRet
	{
		Success = 0,
		Error = 1,
		LowCount,
		InCD,
		LowLevel,
	}

}