using System;

namespace SomeWeirdGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.CursorVisible = false;
            int levels = 1;

            Field field = new Field();
            Player player = new Player(field);
            Reward reward = new Reward(field, 5);
            reward.SetRewards();
            int score = 0;
            while (field.NumberOfLevels > levels)
            {
                Console.WriteLine($"Level: {levels}");
                field.PrintField();
                score += reward.GetReward(player.X, player.Y);
                Console.SetCursorPosition(0, Field.HEIGHT);
                Console.WriteLine($"\nScore: {score}");
                Console.SetCursorPosition(player.X * 2, player.Y);
                player.DisplayPlayer();

                if (score == reward.NumberOfRewards)
                {
                    levels++;
                    field = new Field();
                    player = new Player(field);
                    reward = new Reward(field, 5);
                    reward.SetRewards();
                    score = 0;
                }

                if (score == reward.NumberOfRewards && levels == field.NumberOfLevels)
                {
                    Console.SetCursorPosition(0, Field.HEIGHT + 1);
                    Console.WriteLine("Congratulations! You have collected all rewards! Exiting...");
                    Environment.Exit(0);
                }

                var key = Console.ReadKey(intercept: true).Key;
                switch (key)
                {
                    case ConsoleKey.W: player.Move(0, -1, field); break;
                    case ConsoleKey.S: player.Move(0, 1, field); break;
                    case ConsoleKey.A: player.Move(-1, 0, field); break;
                    case ConsoleKey.D: player.Move(1, 0, field); break;
                }


            }
        }
    }
}
