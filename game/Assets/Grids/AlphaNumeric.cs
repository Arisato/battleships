using game.Assets.Vehicles;

namespace game.Assets.Grids
{
    public class AlphaNumeric : Grid
	{
		public AlphaNumeric(List<WaterCraft> waterCrafts, int gridSizeX, int gridSizeY)
		{
			GridSizeX = gridSizeX;
			GridSizeY = gridSizeY;
            Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

            foreach (var waterCraft in waterCrafts)
			{
				WaterCrafts.Add(waterCraft, GenCoordinates(waterCraft.Size, gridSizeX, gridSizeY));
            }
		}
    }
}