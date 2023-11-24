using game.Assets.Grids;
using game.Utilities;
using game.Utilities.Enums;

namespace game.GameEngines
{
    public class BattleShipsEngine
	{
		public BattleShipsEngine(AlphaNumeric grid)
		{
			if (grid != null)
			{
				IsGridValid = true;
                Grid = grid;
            }
        }

		public void ReadCoordInput(string input)
		{
			var countSnapshot = Grid.WaterCrafts.Count;
			var targetName = Grid.ReadCoordinate(input);

            if (!string.IsNullOrEmpty(targetName))
			{
				if (countSnapshot > Grid.WaterCrafts.Count)
				{
					FeedbackInterface.SendFeedback(ActionType.ShipHasSunk, targetName);
				}
				else
				{
                    FeedbackInterface.SendFeedback(ActionType.Hit, targetName);
                }
			}
			else
			{
                FeedbackInterface.SendFeedback(ActionType.Miss);
            }
		}

		public bool IsGridValid { get; private set; }

        public bool IsGameLive { get { return IsGridValid && Grid.WaterCrafts.Any(); } }

        public AlphaNumeric Grid { get; private set; }
	}
}