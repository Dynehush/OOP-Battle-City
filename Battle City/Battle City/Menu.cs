using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeWeirdGame
{
    class Menu
    {
        public static string IntroductionText =
            "╔══════════════════════════════════════════╗\n" +
            "║    === Welcome to The BattleCity! ===    ║\n" +
            "╠══════════════════════════════════════════╣\n" +
            "║ ---> Press 'Enter' to start a new game.  ║\n" +
            "║ ---> Press 'h' for the instructions.     ║\n" +
            "║ ---> Press 'Esc' to exit.                ║\n" +
            "╚══════════════════════════════════════════╝";

        public static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine(IntroductionText);

            while (true)
            {
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.Enter:
                        Game.GameState = Game.State.Run;
                        return;
                    case ConsoleKey.H:
                        ShowInstructions();
                        break;
                    case ConsoleKey.Escape:
                        Environment.Exit(0);
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
                "║ Good luck!                               ║\n" +
                "╚══════════════════════════════════════════╝");
            Console.ReadKey();
            ShowMenu();
        }

        public static void ShowGameOver()
        {
            Console.Clear();
            Thread.Sleep(1000);
            Console.WriteLine(
                "╔══════════════════════════════════════════╗\n" +
                "║             === Game Over ===            ║\n" +
                "╠══════════════════════════════════════════╣\n" +
                "║ You have been defeated.                  ║\n" +
                "║ ---> Press 'Enter' to restart the game.  ║\n" +
                "║ ---> Press 'h' for the instructions.     ║\n" +
                "║ ---> Press 'Esc' to exit.                ║\n" +
                "╚══════════════════════════════════════════╝");


            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    switch (key)
                    {
                        case ConsoleKey.Enter:
                            Game.Restart();
                            return;
                        case ConsoleKey.H:
                            ShowInstructions();
                            break;
                        case ConsoleKey.Escape:
                            Game.GameState = Game.State.Defeat;
                            return;
                        default:
                            if (Console.KeyAvailable)
                                key = Console.ReadKey(true).Key;
                            continue;
                    }
                }
            }
        }
    }
}
