using game.Assets.Vehicles.Enums;

namespace game.Settings
{
    public class Settings
	{
        public int GridSizeX { get; set; }
		public int GridSizeY { get; set; }
        public required IDictionary<WaterCraftType, int> VehiclesQty { get; set; }
    }
}