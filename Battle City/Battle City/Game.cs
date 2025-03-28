using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Timers;


namespace SomeWeirdGame
{
    class Game
    {
        public static readonly Random Random = new Random();
        private static System.Timers.Timer _timer;
        private static int _level = 1;

        private static Field _field = new Field(_level);
        private static Enemy _enemy = new Enemy(_field);
        private static Player _player = new Player(_field);
        private static Reward _reward = new Reward(5);
        private static int _score = 0;
        public enum State
        {
            Pause,
            Run,
            Win,
            Defeat,
        }

        public static State GameState;
        static Game()
        {
            _timer = new System.Timers.Timer(1000);
            _timer.AutoReset = true;
            _timer.Enabled = true;
            _timer.Elapsed += Start;
        }

        public virtual void Display() { }
        public static void ExecuteBasics()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.CursorVisible = false;
            Console.SetWindowSize(Field.WIDTH + 10, Field.HEIGHT + 10);
            Console.SetBufferSize(Field.WIDTH + 10, Field.HEIGHT + 10);
            Menu.ShowMenu();
            _reward.SetRewards(_score, _field, _player);
        }
        public static void Start(object? sender, ElapsedEventArgs e)
        {
            while (GameState == State.Run)
            {
                //Task.Run(() => Menu.Pause());
                _field.Display();
                Console.SetCursorPosition(_enemy.X * 2, _enemy.Y);
                Console.Write("  ");
                _enemy.Move(_field, _player);
                Console.SetCursorPosition(_enemy.X * 2, _enemy.Y);
                _enemy.Display();

                _reward.CheckRewards(_score, _field);

                _score += _reward.GetReward(_player.X, _player.Y, _field);
                /*
                if (_random.Next(0, 101) == 15)
                {
                    _reward.CheckRewards(_score);
                    _reward.SetRewards(_score, _player);
                }
                */
                Console.SetCursorPosition(0, Field.HEIGHT + 1);
                Console.WriteLine("Collect all the stars to win!");
                Console.WriteLine("And avoid enemy bullets if you want to survive.");
                Console.WriteLine($"\nLevel: {_level}");

                Console.WriteLine($"\nScore: {_score}");

                Bullet.UpdateBullets(_player.Bullets, _field, _reward);
                Bullet.UpdateBullets(_enemy.Bullets, _field, _reward);
                Bullet.Hit(_enemy.Bullets, _player);
                Player.CheckCollision(_player, _enemy);
                _enemy.IsOverlapping(_enemy);
                _enemy.Shoot(_field, _player, _enemy);

                Console.SetCursorPosition(_player.X * 2, _player.Y);
                _player.Display();

                Console.SetCursorPosition(_enemy.X * 2, _enemy.Y);
                _enemy.Display();


                if (_score == _reward.NumberOfRewards)
                {
                    if (_level == Field.NUMBER_OF_LEVELS)
                    {
                        GameState = State.Win;
                    }
                    else
                    {
                        _level++;
                        _field = new Field(_level);
                        _player = new Player(_field);
                        _reward = new Reward(5);
                        _reward.SetRewards(0, _field, _player);
                        _score = 0;
                    }
                }

                Player.MovePlayer(_player, _field, _reward, _enemy);

                if (GameState == State.Win)
                {
                    Console.Clear();
                    Console.WriteLine("Congratulations! You have collected all the rewards! Exiting...");
                    Environment.Exit(1);
                }

                if (GameState == State.Defeat) {
                    Console.Clear();
                    Console.WriteLine("*** GAME OVER ***");
                    Environment.Exit(1);
                }
            }
        }
    }
}
