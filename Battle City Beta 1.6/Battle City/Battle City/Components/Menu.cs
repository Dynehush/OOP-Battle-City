
using System;
using Battle_City.Core;
using Battle_City.Core.Actor;
using Battle_City.Core.GameElements;

namespace Battle_City
{
    internal class Menu
    {
        public static void ShowMenu(Field _field, Player _player, Enemy _enemy)
        {
            Console.CursorVisible = false;
            Console.Clear();
            Console.WriteLine(
            "╔══════════════════════════════════════════╗\n" +
            "║    === Welcome to The BattleCity! ===    ║\n" +
            "╠══════════════════════════════════════════╣\n" +
            "║ ---> Press 'Enter' to start a new game.  ║\n" +
            "║ ---> Press 'F11' to load the last game.  ║\n" +
            "║ ---> Press 'H' for the instructions.     ║\n" +
            "║ ---> Press 'Esc' to exit.                ║\n" +
            "╚══════════════════════════════════════════╝");

            while (true)
            {
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.Enter:
                        if (Game.GameState == Game.State.Return)
                        {
                            Console.Clear();
                            Game.Restart();
                            return;
                        } else
                        {
                            Game.GameState = Game.State.Run;
                            System.Console.Clear();
                            Game.ConsoleRenderer.Init(_field, _player, _enemy);
                            return;
                        }
                    case ConsoleKey.F11:
                        Saver.LoadGame(_field, _player, _enemy);
                        Game.GameState = Game.State.Run;
                        Console.Clear();
                        return;
                    case ConsoleKey.H:
                        ShowInstructions();
                        ShowMenu(_field, _player, _enemy);
                        break;
                    case ConsoleKey.Escape:
                        Environment.Exit(1);
                        break;
                    default:
                        continue;
                }
            }
        }

        public static void ShowInstructions()
        {
            Console.Clear();
            Console.WriteLine(
                "╔══════════════════════════════════════════╗\n" +
                "║            === Instructions ===          ║\n" +
                "╠══════════════════════════════════════════╣\n" +
                "║ Use the arrow keys to move the player.   ║\n" +
                "║ Collect all the stars to win.            ║\n" +
                "║ Avoid enemy bullets.                     ║\n" +
                "║ Press 'Space' to shoot.                  ║\n" +
                "║ Press 'F10' during the game to save.     ║\n" +
                "║ Press 'Esc' to exit the game.            ║\n" +
                "║ Press any key to return to the menu.     ║\n" +
                "║ Good luck!                               ║\n" +
                "╚══════════════════════════════════════════╝");
            Console.ReadKey();
            return;
        }

        public static void ShowGameOver(Field _field, Player _player, Enemy _enemy)
        {
            while (Game.GameState == Game.State.Pause)
            {
                Console.Clear();
                Console.WriteLine(
                    "╔══════════════════════════════════════════╗\n" +
                    "║             === Game Over ===            ║\n" +
                    "╠══════════════════════════════════════════╣\n" +
                    "║ You have been defeated.                  ║\n" +
                    "║ ---> Press 'Enter' to restart the game.  ║\n" +
                    "║ ---> Press 'F11' to load the last game.  ║\n" +
                    "║ ---> Press 'H' for the instructions.     ║\n" +
                    "║ ---> Press 'Esc' to exit.                ║\n" +
                    "╚══════════════════════════════════════════╝");
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.Enter:
                        Game.GameState = Game.State.Run;
                        Game.Level = 1;
                        Console.Clear();
                        Game.Restart();
                        return;
                    case ConsoleKey.F11:
                        Saver.LoadGame(_field, _player, _enemy);
                        Game.GameState = Game.State.Load;
                        Console.Clear();
                        return;
                    case ConsoleKey.H:
                        ShowInstructions();
                        ShowGameOver(_field, _player, _enemy);
                        break;
                    case ConsoleKey.Escape:
                        Environment.Exit(1);
                        return;
                    default:
                        continue;
                }
            }
        }

        public static void SaveProgress(Field _field, Player _player, Enemy _enemy)
        {
            Console.Clear();
            try
            {
                Saver.SaveGame(_field, _player, _enemy);
                Console.WriteLine(
                    "╔══════════════════════════════════════════╗\n" +
                    "║           === Progress Saving ===        ║\n" +
                    "╠══════════════════════════════════════════╣\n" +
                    "║         Game saved successfully!         ║\n" +
                    "║ Press any key to return to the game...   ║\n" +
                    "╚══════════════════════════════════════════╝");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save game: {ex.Message}");
                Console.WriteLine("Press any key to return to the game...");
                Console.ReadKey();
            }
            Game.GameState = Game.State.Run;
            Console.Clear();
        }

        public static void ExitGame(Field _field, Player _player, Enemy _enemy)
        {
            Console.Clear();
            Console.WriteLine(
                "╔══════════════════════════════════════════╗\n" +
                "║             === Exit Game ===            ║\n" +
                "╠══════════════════════════════════════════╣\n" +
                "║ Are you sure you want to exit?           ║\n" +
                "║ ---> Press 'Y' to confirm.               ║\n" +
                "║ ---> Press 'N' to resume game.           ║\n" +
                "║ ---> Press 'M' to return to the menu.    ║\n" +
                "╚══════════════════════════════════════════╝");
            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.Y:
                    Environment.Exit(1);
                    break;
                case ConsoleKey.N:
                    Game.GameState = Game.State.Run;
                    Console.Clear();
                    break;
                case ConsoleKey.M:
                    Console.Clear();
                    Game.GameState = Game.State.Return;
                    ShowMenu(_field, _player, _enemy);
                    break;
                default:
                    ExitGame(_field, _player, _enemy);
                    Console.Clear();
                    break;
            }
        }

        public static void ShowWinMessage(Field _field, Player _player, Enemy _enemy)
        {
            Console.Clear();
            Console.WriteLine(
                        "╔══════════════════════════════════════════╗\n" +
                        "║          === Congratulations! ===        ║\n" +
                        "╠══════════════════════════════════════════╣\n" +
                        "║ You have collected all the rewards!      ║\n" +
                        "║ ---> Press 'Esc' to exit.                ║\n" +
                        "║ ---> Press 'Enter' to start a new game.  ║\n" +
                        "║ ---> Press 'M' to return to the menu.    ║\n" +
                        "╚══════════════════════════════════════════╝");
            var key = Console.ReadKey(true).Key;
            switch (key)    
            {
                case ConsoleKey.Escape:
                    Environment.Exit(1);
                    break;
                case ConsoleKey.Enter:
                    Game.GameState = Game.State.Run;
                    Game.Level = 1;
                    Console.Clear();
                    Game.Restart();
                    return;
                case ConsoleKey.M:
                    Console.Clear();
                    ShowMenu(_field, _player, _enemy);
                    break;
                default:
                    ShowWinMessage(_field, _player, _enemy);
                    Console.Clear();
                    break;
            }
        }
    }
}
