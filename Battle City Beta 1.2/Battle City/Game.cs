using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Timers;


namespace Battle_City
{
    abstract class Game
    {
        public static readonly Random Random = new Random();
        private static int _level = 1;

        private static Field _field = new Field(_level);
        private static Enemy _enemy = new Enemy(_field);
        private static Player _player = new Player(_field);
        private static Reward _reward = new Reward(5);
        private static int _score = 0;

        private static Thread? _enemyMoveThread;
        private static bool _enemyMoveActive = true;

        public enum State
        {
            Pause,
            Run,
            Win,
            Defeat
        }

        public static State GameState;

        public static void ExecuteBasics()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.CursorVisible = false;
            Menu.ShowMenu();

            _reward.SetRewards(_score, _field, _player);
            GameState = State.Run;

            StartEnemyMovement(); 
            StartEnemyBulletMovement();
            RunGame();            
        }
        private static readonly object consoleLock = new object();
        private static void StartEnemyMovement()
        {
            _enemyMoveActive = true;

            _enemyMoveThread = new Thread(() =>
            {
                while (_enemyMoveActive && GameState == State.Run)
                {
                    

                    lock (consoleLock)
                    {
                        Player.CheckCollision(_player, _enemy);
                        _enemy.IsOverlapping(_enemy);

                        _enemy.Move(_field, _player);
                        _enemy.Shoot(_field, _player, _enemy);

                        Console.SetCursorPosition(_enemy.OldX * 2, _enemy.OldY);
                        Console.Write("  ");

                        Console.SetCursorPosition(_enemy.X * 2, _enemy.Y);
                        _enemy.Display();
                    }

                    Thread.Sleep(200);
                }

               
            });

            _enemyMoveThread.IsBackground = true;
            _enemyMoveThread.Start();
        }

        private static void RenderField()
        {
            Console.Clear();  

            _field.DisplayField();

            Console.SetCursorPosition(_player.X * 2, _player.Y);
            _player.Display();

            Console.SetCursorPosition(_enemy.X * 2, _enemy.Y);
            _enemy.Display();

            Console.SetCursorPosition(0, Field.HEIGHT + 1);
            Console.WriteLine("Collect all the stars to win!");
            Console.WriteLine("And avoid enemy bullets if you want to survive.");
            Console.WriteLine($"\nLevel: {_level}");
            Console.WriteLine($"\nScore: {_score}");
        }

        private static Thread? _enemyBulletThread;
        private static bool _enemyBulletActive = true;
        private static void StartEnemyBulletMovement()
        {
            _enemyBulletActive = true;

            _enemyBulletThread = new Thread(() =>
            {
                while (_enemyBulletActive && GameState == State.Run)
                {
                    lock (consoleLock)
                    {
                        Bullet.UpdateBullets(_player.Bullets, _field, _reward);
                        Bullet.UpdateBullets(_enemy.Bullets, _field, _reward);
                        Bullet.HitPlayer(_enemy.Bullets, _player);
                        Bullet.HitEnemy(_player.Bullets, _enemy, _field);
                    }

                    Thread.Sleep(100);
                }
            });

            _enemyBulletThread.IsBackground = true;
            _enemyBulletThread.Start();
        }

        public static void Restart()
        {
            Console.Clear();
            _enemyMoveActive = false;
            _enemyMoveThread?.Join();

            _score = 0;
            _level = 1;
            _field = new Field(_level);
            _player.X = 2; _player.Y = 2;
            _player = new Player(_field);
            _enemy.X = Field.WIDTH - 4; _enemy.Y = Field.HEIGHT - 4;
            _enemy = new Enemy(_field);
            _reward = new Reward(5);
            _reward.SetRewards(_score, _field, _player);
            GameState = State.Run;
            StartEnemyMovement();
            StartEnemyBulletMovement();

            Thread gameThread = new Thread(RunGame);
            gameThread.Start();

            
        }
        //private static void RenderField()
        //{
        //    _field.DisplayField();

        //    Console.SetCursorPosition(_player.X * 2, _player.Y);
        //    _player.Display();

        //    Console.SetCursorPosition(_enemy.X * 2, _enemy.Y);
        //    _enemy.Display();

        //    Console.SetCursorPosition(0, Field.HEIGHT + 1);
        //    Console.WriteLine("Collect all the stars to win!");
        //    Console.WriteLine("And avoid enemy bullets if you want to survive.");
        //    Console.WriteLine($"\nLevel: {_level}");
        //    Console.WriteLine($"\nScore: {_score}");
        //}

        //private static void Render()
        //{
        //    _enemyActive = true;

        //    _gameLoopActive = true;
        //    _gameLoopThread = new Thread(() =>
        //    {
        //        while (_gameLoopActive && GameState == State.Run)
        //        {
        //            RenderField();
        //            Thread.Sleep(200); 
        //        }
        //    });
        //    _gameLoopThread.Start();

        //    _enemyMoveThread = new Thread(() =>
        //    {
        //        while (_enemyActive && GameState == State.Run)
        //        {
        //            _enemy.Move(_field, _player);
        //            Thread.Sleep(300); 
        //        }
        //    });
        //    _enemyMoveThread.Start();

        //    _enemyShootThread = new Thread(() =>
        //    {
        //        while (_enemyActive && GameState == State.Run)
        //        {
        //            _enemy.Shoot(_field, _player, _enemy);
        //            Thread.Sleep(100);
        //        }
        //    });
        //    _enemyShootThread.Start();

        //}
        public static void RunGame()
        {
            while (GameState == State.Run)
            {
                RenderField();

                _reward.CheckRewards(_score, _field);
                _score += _reward.GetReward(_player.X, _player.Y, _field);

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
                        _enemy = new Enemy(_field);
                        _reward = new Reward(5);
                        _reward.SetRewards(0, _field, _player);
                        _score = 0;
                    }
                }

                Player.MovePlayer(_player, _field, _reward, _enemy);

                if (GameState == State.Win)
                {
                    Console.Clear();
                    Console.WriteLine(
                        "╔══════════════════════════════════════════╗\n" +
                        "║        === Congratulations! ===          ║\n" +
                        "╠══════════════════════════════════════════╣\n" +
                        "║ You have collected all the rewards!      ║\n" +
                        "║ Exiting...                               ║\n" +
                        "╚══════════════════════════════════════════╝");
                    Environment.Exit(1);
                }

                if (GameState == State.Defeat)
                {
                    _enemyMoveActive = false;
                    _enemyMoveThread?.Join();

                    Console.Clear();
                    Menu.ShowGameOver();
                    return;
                }

                if (GameState == State.Pause)
                {
                    _enemyMoveActive = false;
                    _enemyMoveThread?.Join();
                    Console.Clear();
                    Menu.ShowGameOver();
                    return;
                }

                Thread.Sleep(10); 
            }
        }
    }
}
