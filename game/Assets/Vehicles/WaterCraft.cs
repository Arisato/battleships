using game.Assets.Vehicles.Enums;

namespace game.Assets.Vehicles
{
    public class WaterCraft
	{
        public WaterCraft(WaterCraftType type)
		{
            Name = type.ToString();
            Size = (int)type;
		}

		public string Name { get; private set; }
		public int Size { get; private set; }
	}
}