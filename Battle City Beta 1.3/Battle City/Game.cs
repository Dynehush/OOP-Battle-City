using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Timers;
using static System.Formats.Asn1.AsnWriter;


namespace Battle_City
{
    abstract class Game
    {
        public static readonly Random Random = new Random();

        public static int Level = 1;
        private static int _score = 0;

        private static Field _field = new Field(Level);
        private static Enemy _enemy = new Enemy(_field);
        private static Player _player = new Player(_field);
        private static Reward _reward = new Reward(5);

        private static Thread? _enemyMoveThread;
        private static bool _enemyMoveActive = true;

        private static Thread? _bulletThread;
        private static bool _bulletActive = true;

        public enum State
        {
            Pause,
            Run,
            Win,
            Defeat,
            Save,
            Exit
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
            StartBulletMovement();
            RunGame();            
        }
        private static readonly object consoleLock = new object();
        private static void StartEnemyMovement()
        {
            _enemyMoveActive = true;

            _enemyMoveThread = new Thread(() =>
            {
                while (_enemyMoveActive)
                {
                    if (GameState != State.Run)
                    {
                        Thread.Sleep(100);
                        continue;
                    }
                    lock (consoleLock)
                    {
                        Player.CheckCollision(_player, _enemy);
                        _enemy.IsOverlapping(_enemy);

                        Console.SetCursorPosition(_enemy.OldX * 2, _enemy.OldY);
                        Console.Write("  ");

                        Console.SetCursorPosition(_enemy.X * 2, _enemy.Y);
                        _enemy.Display();

                        _enemy.Move(_field, _player);
                        _enemy.Shoot(_field, _player, _enemy);
                    }

                    Thread.Sleep(400);
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

            Console.SetCursorPosition(0, Field.Height + 1);
            Console.WriteLine("Collect all the stars to win!");
            Console.WriteLine("Avoid enemy bullets if you want to survive.");
            Console.WriteLine($"\nLevel: {Level}");
            Console.WriteLine($"\nScore: {_score}");
            Console.WriteLine("\nPress 'U' to save your progress.");
        }
        private static void StartBulletMovement()
        {

            _bulletActive = true;

            _bulletThread = new Thread(() =>
            {
                while (_bulletActive && GameState == State.Run)
                {
                    lock (consoleLock)
                    {
                        Bullet.UpdateBullets(_player.Bullets, _field, _reward, _enemy);
                        Bullet.UpdateBullets(_enemy.Bullets, _field, _reward, _enemy);

                        Console.SetCursorPosition(_enemy.OldX * 2, _enemy.OldY);
                        Console.Write("  ");

                        Console.SetCursorPosition(_enemy.X * 2, _enemy.Y);
                        _enemy.Display();

                        Bullet.HitPlayer(_enemy.Bullets, _player);
                        Bullet.HitEnemy(_player.Bullets, _enemy, _field);
                    }

                    Thread.Sleep(500);
                }
            });

            _bulletThread.IsBackground = true;
            _bulletThread.Start();
        }
        public static void Restart()
        {

            Console.Clear();

            _enemyMoveActive = false;
            _bulletActive = false;

            _enemyMoveThread?.Join();
            _bulletThread?.Join();

            _score = 0;
            _field = new Field(Level);
            _player.X = 2; _player.Y = 2;
            _player = new Player(_field);
            _enemy.X = Field.Width - 4; _enemy.Y = Field.Height - 4;

            _enemy = new Enemy(_field);
            _reward = new Reward(5);
            _reward.SetRewards(_score, _field, _player);

            GameState = State.Run;

            StartEnemyMovement();
            StartBulletMovement();

            RunGame();
        }

        public static void SaveGame(string filePath)
        {
            try
            {
                var gameData = new
                {
                    Level = Level,
                    Score = _score,
                    PlayerPosition = new { X = _player.X, Y = _player.Y },
                    EnemyPosition = new { X = _enemy.X, Y = _enemy.Y },
                    Rewards = _reward._rewardPositions.Select(pos => new { X = pos.Item1, Y = pos.Item2 }).ToList(),
                    Field = _field.FieldGrid.Cast<BaseObject>().Select(obj => new
                    {
                        X = obj.X,
                        Y = obj.Y,
                        Symbol = obj.Symbol
                    }).ToList()
                };

                var json = JsonSerializer.Serialize(gameData, new JsonSerializerOptions { WriteIndented = true });

                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save game: {ex.Message}");
            }
        }
        public static void LoadGame(string filePath)
        {
            var json = File.ReadAllText(filePath);
            var gameData = JsonSerializer.Deserialize<JsonElement>(json);

            Level = gameData.GetProperty("Level").GetInt32();
            _score = gameData.GetProperty("Score").GetInt32();
            _player.X = gameData.GetProperty("PlayerPosition").GetProperty("X").GetInt32();
            _player.Y = gameData.GetProperty("PlayerPosition").GetProperty("Y").GetInt32();
            _enemy.X = gameData.GetProperty("EnemyPosition").GetProperty("X").GetInt32();
            _enemy.Y = gameData.GetProperty("EnemyPosition").GetProperty("Y").GetInt32();

            foreach (var reward in gameData.GetProperty("Rewards").EnumerateArray())
            {
                int rewardX = reward.GetProperty("X").GetInt32();
                int rewardY = reward.GetProperty("Y").GetInt32();
                _reward._rewardPositions.Add((rewardX, rewardY));
                _field.FieldGrid[rewardX, rewardY] = new Reward(1);
            }

            _field = new Field(Level);
            foreach (var obj in gameData.GetProperty("Field").EnumerateArray())
            {
                int x = obj.GetProperty("X").GetInt32();
                int y = obj.GetProperty("Y").GetInt32();
                string symbol = obj.GetProperty("Symbol").GetString();

                if (symbol == Field.FieldBase)
                    _field.FieldGrid[x, y] = new EmptyObject(x, y, Field.FieldBase);
                else if (symbol == Field.FieldBarrier)
                    _field.FieldGrid[x, y] = new BarrierObject(x, y, Field.FieldBarrier);
                else if (symbol == Reward._rewardSign)
                    _field.FieldGrid[x, y] = new Reward(1);
                else if (symbol == Player._playerFigure)
                    _field.FieldGrid[x, y] = new Player(_field);
                else if (symbol == Enemy._enemyFigure)
                    _field.FieldGrid[x, y] = new Enemy(_field);
            }
        }



        public static void RunGame()
        {
            while (GameState == State.Run)
            {

                RenderField();

                _reward.CheckRewards(_score, _field);
                _score += _reward.GetReward(_player.X, _player.Y, _field);

                if (_score == _reward.NumberOfRewards)
                {
                    if (Level == Field.NumberOfLevels)
                    {
                        GameState = State.Win;
                    }
                    else
                    {
                        Level++;
                        Restart();
                    }
                }

                Player.PlayerActions(_player, _field, _reward, _enemy);

                if (GameState == State.Win)
                {
                    Console.Clear();
                    Menu.ShowWinMessage();
                    return;
                }

                if (GameState == State.Defeat)
                {
                    Console.Clear();
                    Menu.ShowGameOver();
                    return;
                }

                if (GameState == State.Pause)
                {
                    Console.Clear();
                    Menu.ShowGameOver();
                    Restart();
                }

                if (GameState == State.Save)
                {
                    Console.Clear();
                    Menu.SaveProgress();
                }

                if (GameState == State.Exit)
                {
                    Console.Clear();
                    Menu.ExitGame();
                    return;
                }

                Thread.Sleep(10); 
            }
        }
    }
}
