using game.Assets.Grids;
using game.Utilities.Enums;
using static System.Console;

namespace game.Utilities
{
    public static class FeedbackInterface
	{
        public static void SendFeedback(ActionType action, string? option = null)
        {
            ForegroundColor = ConsoleColor.White;
            switch (action)
            {
                case ActionType.Miss:
                    WriteLine($"\n\nIt's a miss!");
                    break;
                case ActionType.PlayerTurn:
                    WriteLine("\n\nPlayer Turn! Awaiting input..");
                    break;
                case ActionType.Hit:
                    WriteLine($"\n\n{option} has been hit!");
                    break;
                case ActionType.ShipHasSunk:
                    WriteLine($"\n\n{option} has been sunk!");
                    break;
                case ActionType.InitSettings:
                    WriteLine("Settings initialized..");
                    break;
                case ActionType.InitGrid:
                    WriteLine("Grid initialized..");
                    break;
                case ActionType.EngineInit:
                    WriteLine("Game engine initialized..");
                    break;
                case ActionType.GameStart:
                    WriteLine("\nGame is in session!\n");
                    break;
                case ActionType.InvalidGrid:
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine("\n\nGrid size is inadequate for selected ships in the [Settings] json file.");
                    Read();
                    break;
                case ActionType.GameEnd:
                    WriteLine("\n\nAll ships destroyed. Player won!");
                    Read();
                    break;
            }
        }

        public static void PrintMap(Grid grid)
        {
            var craftLabels = grid.WaterCrafts.GroupBy(c => c.Key.Name);

            for (int y = 0; y <= grid.GridSizeY; y++)
            {
                for (int x = 0; x <= grid.GridSizeX; x++)
                {
                    if (y == 0)
                    {
                        ForegroundColor = ConsoleColor.DarkYellow;
                        if (x == 0)
                        {
                            Write("[ ]");
                        }
                        else
                        {
                            Write($"[{x}]");
                        }

                        if (x == grid.GridSizeX)
                        {
                            ForegroundColor = ConsoleColor.DarkMagenta;
                            Write($"***Enemy Ships***");
                        }
                    }
                    else
                    {
                        if (x == 0)
                        {
                            ForegroundColor = ConsoleColor.DarkYellow;
                            Write($"[{grid.Alphabet[y - 1]}]");
                        }
                        else
                        {
                            if (grid.CoordinatesInterracted.ContainsKey($"{grid.Alphabet[y - 1]}{x}"))
                            {
                                if (grid.CoordinatesInterracted.First(interactedCoord => interactedCoord.Key == $"{grid.Alphabet[y - 1]}{x}").Value == 0)
                                {
                                    ForegroundColor = ConsoleColor.DarkCyan;
                                    Write("[*]");
                                }
                                else
                                {
                                    ForegroundColor = ConsoleColor.Red;
                                    Write("[*]");
                                }
                            }
                            else
                            {
                                ForegroundColor = ConsoleColor.DarkBlue;
                                Write("[~]");
                            }

                            if (x == grid.GridSizeX)
                            {
                                if (y > 0)
                                {
                                    if (y == 1 && !craftLabels.Any())
                                    {
                                        ForegroundColor = ConsoleColor.DarkMagenta;
                                        Write($"       [None]");
                                    }

                                    if (craftLabels?.Count() >= y)
                                    {
                                        ForegroundColor = ConsoleColor.DarkMagenta;
                                        Write($"  [{craftLabels.ElementAt(y-1).First().Key.Name}][{craftLabels.ElementAt(y - 1)?.Count()}]");
                                    }
                                }
                            }
                        }
                    }
                }

                WriteLine();
            }
        }
    }
}

