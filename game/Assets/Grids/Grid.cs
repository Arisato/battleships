using game.Assets.Vehicles;

namespace game.Assets.Grids
{
    public abstract class Grid
	{
        private bool IsDirectionX { get; set; }

        public int GridSizeX { get; protected set; }

        public int GridSizeY { get; protected set; }

        public char[] Alphabet { get; protected set; }

        public Dictionary<WaterCraft, List<string>> WaterCrafts { get; protected set; } = new();

        public Dictionary<string, byte> CoordinatesInterracted { get; protected set; } = new();

        public string ReadCoordinate(string coordinate)
        {
            foreach (var waterCraft in WaterCrafts)
            {
                if (waterCraft.Value.Remove(coordinate))
                {
                    if (waterCraft.Value.Count < 1)
                    {
                        WaterCrafts.Remove(waterCraft.Key);
                    }

                    CoordinatesInterracted.TryAdd(coordinate, 1);
                    return waterCraft.Key.Name;
                }
            }

            CoordinatesInterracted.TryAdd(coordinate, 0);
            return null;
        }

        protected List<string> GenCoordinates(int waterCraftSize, int gridSizeX, int gridSizeY)
        {
            var coords = new List<string>();
            var startingPointX = new Random().Next(1, gridSizeX+1);
            var startingPointY = new Random().Next(1, gridSizeY+1);
            IsDirectionX = new Random().Next(0, 2) > 0;
            var occupiedCoords = WaterCrafts.SelectMany(a => a.Value)?.ToList();

            for (int i = 0; i < waterCraftSize; i++)
            {
                if (occupiedCoords.Any(x => x == $"{Alphabet.ElementAt(startingPointY-1)}{startingPointX}"))
                {
                    ResetLoopAndCoords(ref i, coords);

                    SelectNext(ref startingPointX, ref startingPointY, gridSizeX, gridSizeY, coords, ref i);
                }
                else
                {
                    coords.Add($"{Alphabet.ElementAt(startingPointY - 1)}{startingPointX}");

                    SelectNext(ref startingPointX, ref startingPointY, gridSizeX, gridSizeY, coords, ref i);
                }
            }

            return coords;
        }

        private void SelectNext(ref int startingPointX, ref int startingPointY, int gridSizeX, int gridSizeY, List<string> coords, ref int i)
        {
            if (IsDirectionX)
            {
                if (startingPointX < gridSizeX)
                {
                    startingPointX++;
                }
                else
                {
                    if (startingPointY < gridSizeY)
                    {
                        startingPointY++;
                        startingPointX = 1;
                        ResetLoopAndCoords(ref i, coords);
                    }
                }
            }
            else
            {
                if (startingPointY < gridSizeY)
                {
                    startingPointY++;
                }
                else
                {
                    startingPointY = 1;
                    startingPointX++;
                    ResetLoopAndCoords(ref i, coords);
                }
            }
        }

        private void ResetLoopAndCoords(ref int i, List<string> coords)
        {
            i = -1;
            coords.Clear();
        }
    }
}