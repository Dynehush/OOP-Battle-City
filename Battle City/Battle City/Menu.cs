using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeWeirdGame
{
    class Menu : Game
    {
        public static string IntroductionText = "=== Welcome to The BattleCity! ===\n\n" +
            "---> Press Enter to start a new game.\n\n" +
            "---> Type 'h' for the instructions.\n\n" +
            "---> Press 'Esc' to exit.";

        public static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine(IntroductionText);

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    GameState = State.Run;
                    break;
                }
                else if (key.Key == ConsoleKey.H)
                {
                    ShowInstructions();
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    GameState = State.Defeat;
                    break;
                }
            }
        }

        public static void ShowInstructions()
        {
            Console.Clear();
            Console.WriteLine("=== Instructions ===\n\n" +
                "Use the arrow keys to move the player.\n\n" +
                "Collect all the stars to win.\n\n" +
                "Avoid enemy bullets.\n\n" +
                "Press 'Space' to shoot.\n\n" +
                "Press 'Esc' to exit the game.\n\n" +
                "Good luck!");
            Console.ReadKey();
            ShowMenu();
        }
        public static void Pause()
        {
            var key = Console.ReadKey(intercept: true).Key;
            if (key == ConsoleKey.P)
            {
                key = 0;
                GameState = State.Pause;
                Console.Clear();    
                Console.WriteLine("Game is paused. \n\n" +
                    "Press 'P' again to resume. ");
            }
            while (key != ConsoleKey.P)
            {
                key = Console.ReadKey(intercept: true).Key;
            }
            if (key == ConsoleKey.P)
            {
                GameState = State.Run;
            }
        }

    }
}
