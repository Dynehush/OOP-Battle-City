using Battle_City;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;

abstract class Game
{
    public static readonly Random Random = new Random();

    public static int Level = 1;
    private static int _score = 0;

    public static readonly string heartSymbol = "❤️";
    public static int Lives = 5;

    private static Field _field = new Field(Level);
    private static Enemy _enemy = new Enemy(_field);
    private static Player _player = new Player(_field);
    private static Reward _reward = new Reward(5);

    private static Thread? _enemyMoveThread;
    private static bool _enemyMoveActive = true;

    private static Thread? _bulletThread;
    private static bool _bulletActive = true;

    private static Thread? _renderThread;
    private static bool _renderActive = true;

    private static readonly object consoleLock = new object();

    private static char[,] renderBuffer = new char[Field.Width, Field.Height];

    public enum State
    {
        Pause,
        Run,
        Win,
        Defeat,
        Save,
        Exit,
        Load
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
        StartRendering();

        RunGame();
    }

    private static void RenderAll()
    {
        lock (consoleLock)
        {
            for (int y = 0; y < Field.Height; y++)
                for (int x = 0; x < Field.Width; x++)
                    renderBuffer[x, y] = ' ';

            _field.DrawToBuffer(renderBuffer);
            _player.DrawToBuffer(renderBuffer);
            foreach (var bullet in _player.Bullets)
                bullet.DrawToBuffer(renderBuffer);
            foreach (var bullet in _enemy.Bullets)
                bullet.DrawToBuffer(renderBuffer);
            _enemy.DrawToBuffer(renderBuffer);

            Console.SetCursorPosition(0, 0);
            StringBuilder sb = new StringBuilder();
            for (int y = 0; y < Field.Height; y++)
            {
                for (int x = 0; x < Field.Width; x++)
                {
                    sb.Append(renderBuffer[x, y]);
                }
                sb.AppendLine();
            }
            Console.Write(sb.ToString());

            string hearts = "";
            for (int i = Lives; i > 0; i--)
            {
                hearts += heartSymbol;
            }

            Console.SetCursorPosition(0, Field.Height + 1);
            Console.WriteLine("Collect all the stars to win!");
            Console.WriteLine("Avoid enemy bullets if you want to survive.");
            Console.WriteLine($"\nLevel: {Level}/3");
            Console.WriteLine($"\nScore: {_score}/5");
            Console.WriteLine($"\nLives: {Lives} {hearts.PadRight(100)}");
            Console.WriteLine("\nPress 'F10' to save your progress.");
        }
    }

    public static void Restart()
    {

        Console.Clear();

        StopThreads();

        Lives = 5;
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
        StartRendering();

        RunGame();
    }

    private static void GenerateNewLevel()
    {
        Console.Clear();

        StopThreads();

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
        StartRendering();

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
                Lives = Lives,
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
        Lives = gameData.GetProperty("Lives").GetInt32();
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
            else if (symbol == Player.PlayerFigure)
                _field.FieldGrid[x, y] = new Player(_field);
            else if (symbol == Enemy._enemyFigure)
                _field.FieldGrid[x, y] = new Enemy(_field);
        }
    }
    private static void StartEnemyMovement()
    {
        _enemyMoveActive = true;

        _enemyMoveThread = new Thread(() =>
        {
            try
            {
                while (_enemyMoveActive)
                {
                    lock (consoleLock)
                    {
                        Player.CheckCollision(_player, _enemy);
                        _enemy.IsOverlapping(_enemy);
                        _enemy.Move(_field, _player);
                        _enemy.Shoot(_field, _player, _enemy);
                    }

                    Thread.Sleep(400);
                }
            }
            catch (ThreadInterruptedException)
            {
                
            }
        });

        _enemyMoveThread.IsBackground = true;
        _enemyMoveThread.Start();
    }
    private static void StartBulletMovement()
    {
        _bulletActive = true;

        _bulletThread = new Thread(() =>
        {
            try
            {
                while (_bulletActive && GameState == State.Run)
                {
                    lock (consoleLock)
                    {
                        Bullet.UpdateBullets(_player.Bullets, _field, _reward, _enemy);
                        Bullet.UpdateBullets(_enemy.Bullets, _field, _reward, _enemy);
                        Bullet.HitPlayer(_enemy.Bullets, _player);
                        Bullet.HitEnemy(_player.Bullets, _enemy, _field);
                    }

                    Thread.Sleep(200);
                }
            }
            catch (ThreadInterruptedException)
            {

            }
        });

        _bulletThread.IsBackground = true;
        _bulletThread.Start();
    }
    private static void StartRendering()
    {
        _renderActive = true;
        _renderThread = new Thread(() =>
        {
            try
            {
                while (_renderActive)
                {
                    lock (consoleLock)
                    {
                        if (GameState == State.Run)
                        {
                            RenderAll();
                        }
                    }
                }
            }
            catch (ThreadInterruptedException)
            {

            }
        });
        _renderThread.IsBackground = true;
        _renderThread.Start();
    }
    private static void StopThreads()
    {
        _enemyMoveActive = false;
        _bulletActive = false;
        _renderActive = false;

        _enemyMoveThread?.Interrupt(); 
        _bulletThread?.Interrupt();
        _renderThread?.Interrupt();

        _enemyMoveThread?.Join();
        _bulletThread?.Join();
        _renderThread?.Join();
    }
    public static void RunGame()
    {
        while (GameState == State.Run)
        {

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
                    GenerateNewLevel();
                }
            }

            Player.PlayerActions(_player, _field, _reward, _enemy);

            if (GameState == State.Win)
            {
                Console.Clear();
                Menu.ShowWinMessage();
                return;
            }

            if (GameState == State.Defeat || GameState == State.Pause)
            {
                Console.Clear();
                Menu.ShowGameOver();
            }

            if (GameState == State.Save)
            {
                StopThreads();

                Console.Clear();
                Menu.SaveProgress();

                StartEnemyMovement();
                StartBulletMovement();
                StartRendering();
            }

            if (GameState == State.Exit)
            {
                StopThreads();

                Console.Clear();
                Menu.ExitGame();

                StartEnemyMovement();
                StartBulletMovement();
                StartRendering();
            }

            if (GameState == State.Load)
            {
                StartEnemyMovement();
                StartBulletMovement();
                StartRendering();

                GameState = State.Run;
            }

            Thread.Sleep(10);
        }
    }
}
