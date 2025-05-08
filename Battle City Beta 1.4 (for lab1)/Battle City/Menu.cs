using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_City
{
    class Menu
    {
        public static void ShowMenu()
        {
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
                        Game.GameState = Game.State.Run;
                        Console.Clear();
                        return;
                    case ConsoleKey.F11:
                        Game.LoadGame("C:\\Uni 2 semester\\OOP\\projects\\Battle City Beta 1.3\\Battle City\\Field.txt");
                        Game.GameState = Game.State.Run;
                        Console.Clear();
                        return;
                    case ConsoleKey.H:
                        ShowInstructions();
                        Console.Clear();
                        break;
                    case ConsoleKey.Escape:
                        Environment.Exit(1);
                        Console.Clear();
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
                "║ Press 'Esc' to exit the game.            ║\n" +
                "║ Press any key to return to the menu.     ║\n" +
                "║ Good luck!                               ║\n" +
                "╚══════════════════════════════════════════╝");
            Console.ReadKey();
            ShowMenu();
        }

        public static void ShowGameOver()
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
                        Game.LoadGame("C:\\Uni 2 semester\\OOP\\projects\\Battle City Beta 1.3\\Battle City\\Field.txt");
                        Game.GameState = Game.State.Run;
                        Console.Clear();
                        return;
                    case ConsoleKey.H:
                        ShowInstructions();
                        Console.Clear();
                        break;
                    case ConsoleKey.Escape:
                        Environment.Exit(1);
                        return;
                    default:
                        continue;
                }
            }
        }

        public static void SaveProgress()
        {
            Console.Clear();
            try
            {
                Game.SaveGame("C:\\Uni 2 semester\\OOP\\projects\\Battle City Beta 1.3\\Battle City\\Field.txt");
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

        public static void ExitGame()
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
                    ShowMenu();
                    break;
                default:
                    ExitGame();
                    Console.Clear();
                    break;
            }
        }

        public static void ShowWinMessage()
        {
            Console.WriteLine(
                        "╔══════════════════════════════════════════╗\n" +
                        "║          === Congratulations! ===        ║\n" +
                        "╠══════════════════════════════════════════╣\n" +
                        "║ You have collected all the rewards!      ║\n" +
                        "║ Exiting...                               ║\n" +
                        "╚══════════════════════════════════════════╝");
            Environment.Exit(1);
        }
    }
}
