using System.Text.Json;
using game.GameEngines;
using game.Services;
using game.Settings;
using game.Utilities;
using game.Utilities.Enums;

internal class Program
{
    private static void Main(string[] args)
    {
        // Game Setup
        var gameEngine = new BattleShipsEngine(new GridBuilder().BuildGrid(InitSettings()));
        FeedbackInterface.SendFeedback(ActionType.InitSettings);
        FeedbackInterface.SendFeedback(ActionType.InitGrid);
        FeedbackInterface.SendFeedback(ActionType.EngineInit);

        // Game Start
        FeedbackInterface.SendFeedback(ActionType.GameStart);

        while (gameEngine.IsGameLive)
        {
            FeedbackInterface.PrintMap(gameEngine.Grid);
            FeedbackInterface.SendFeedback(ActionType.PlayerTurn);
            gameEngine.ReadCoordInput(Console.ReadLine()?.ToUpper());
        }

        // Game End
        if (gameEngine.IsGridValid)
        {
            FeedbackInterface.PrintMap(gameEngine.Grid);
            FeedbackInterface.SendFeedback(ActionType.GameEnd);
        }
        else
        {
            FeedbackInterface.SendFeedback(ActionType.InvalidGrid);
        }
    }

    private static Settings InitSettings()
    {
        using StreamReader file = File.OpenText($"{Directory.GetCurrentDirectory()}//{nameof(Settings)}.json");

        return JsonSerializer.Deserialize<Settings>(file.BaseStream);
    }
}