using Battle_City.Core;
using Battle_City.Core.Actor;
using Battle_City.Core.GameElements;
using System.Text;
using System;
using System.Xml.Linq;
using static Battle_City.Core.Actor.Entity;

namespace Battle_City.Components
{
    public class ConsoleRenderer : IGameRenderer
    {
        private Thread? _enemyMoveThread;
        private bool _enemyMoveActive = true;

        private Thread? _bulletThread;
        private bool _bulletActive = true;

        private Thread? _renderThread;
        private bool _renderActive = true;

        private Thread? _controlThread;
        private bool _controlActive = true;

        private readonly object _consoleLock = new();

        private static string[,] _renderBuffer = new string[Field.Width, Field.Height];
        internal void Init(Field _field, Player _player, Enemy _enemy)
        {
            Start();

            EnemyMovement(_field, _player, _enemy);
            BulletMovement(_field, _player, _enemy);
            StartRendering(_field, _player, _enemy);
            Control(_field, _player, _enemy);
        }
        public void Start() => Console.OutputEncoding = Encoding.UTF8;

        public void Stop()
        {
            _enemyMoveActive = false;
            _bulletActive = false;
            _renderActive = false;
            _controlActive = false;

            _enemyMoveThread?.Interrupt();
            _bulletThread?.Interrupt();
            _renderThread?.Interrupt();
            _controlThread?.Interrupt();

            _enemyMoveThread?.Join();
            _bulletThread?.Join();
            _renderThread?.Join();
            _controlThread?.Join();
        }

        public void Clear()
        {
            for (int x = 0; x < _renderBuffer.GetLength(0); x++)
                for (int y = 0; y < _renderBuffer.GetLength(1); y++)
                    _renderBuffer[x, y] = " ";
        }

        private void EnemyMovement(Field _field, Player _player, Enemy _enemy)
        {
            _enemyMoveActive = true;
            _enemyMoveThread = new Thread(() =>
            {
                try
                {
                    while (_enemyMoveActive)
                    {
                        lock (_consoleLock)
                        {
                            _enemy.Move(_field, _player);
                            Interaction.CheckCollision(_player, _enemy);
                            _enemy.Shoot(_player);
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

        private void BulletMovement(Field _field, Player _player, Enemy _enemy)
        {
            _bulletActive = true;

            _bulletThread = new Thread(() =>
            {
                try
                {
                    while (_bulletActive)
                    {
                        lock (_consoleLock)
                        {
                            Bullet.UpdateBullets(_player.Bullets, _field, _player, _enemy);
                            Bullet.UpdateBullets(_enemy.Bullets, _field, _player, _enemy);
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

        private void StartRendering(Field _field, Player _player, Enemy _enemy)
        {
            _renderActive = true;
            _renderThread = new Thread(() =>
            {
                try
                {
                    while (_renderActive)
                    {
                        lock (_consoleLock)
                        {
                            if (Game.GameState == Game.State.Run)
                                RenderAll(_field, _player, _enemy);
                        }
                    }
                }
                catch (ThreadInterruptedException) { }
            });
            _renderThread.IsBackground = true;
            _renderThread.Start();
        }

        private void Control(Field _field, Player _player, Enemy _enemy)
        {
            _controlActive = true;
            _controlThread = new Thread(() =>
            {
                try
                {
                    while (_controlActive)
                    {
                        lock (_consoleLock)
                        {
                            if (Game.GameState == Game.State.Pause)
                                Menu.ShowGameOver(_field, _player, _enemy);
                        }
                    }
                } catch (ThreadInterruptedException) { }
            });
            _controlThread.IsBackground = true;
            _controlThread.Start();
        }

        private void RenderAll(Field _field, Player _player, Enemy _enemy)
        {
            lock (_consoleLock)
            {
                Clear();

                DrawField(_field);
                Draw(_player);
                foreach (var bullet in _player.Bullets)
                    Draw(bullet);
                foreach (var bullet in _enemy.Bullets)
                    Draw(bullet);
                Draw(_enemy);

                Console.SetCursorPosition(0, 0);
                StringBuilder sb = new StringBuilder();
                for (int y = 0; y < Field.Height; y++)
                {
                    for (int x = 0; x < Field.Width; x++)
                    {
                        sb.Append(_renderBuffer[x, y]);
                    }
                    sb.AppendLine();
                }
                Console.Write(sb.ToString());

                string hearts = "";
                for (int i = _player.Health; i > 0; i--)
                {
                    hearts += Symbols.HeartFIgure;
                }

                Console.SetCursorPosition(0, Field.Height + 1);
                Console.WriteLine("Collect all the stars to win!");
                Console.WriteLine("Avoid enemy bullets if you want to survive.");
                Console.WriteLine($"\nLevel: {Game.Level}/3");
                Console.WriteLine($"\nScore: {Game.Score}/5");
                Console.WriteLine($"\nLives: {_player.Health} {hearts.PadRight(100)}");
            }
        }

        private void Draw(BaseObject element)
        {
            string symbol = element switch
            {
                Enemy => Symbols.EnemyFigure,
                Bullet => Symbols.BulletFigure,
            };

            _renderBuffer[element.X, element.Y] = symbol;
        }

        private void Draw(Player _player)
        {
            _renderBuffer[_player.X, _player.Y] = Symbols.PlayerDirection[_player.PlayerDirection ?? Direction.Right];
        }

        private void DrawField(Field _field)
        {
            for (int y = 0; y < Field.Height; y++)
                for (int x = 0; x < Field.Width; x++)
                {
                    string symbol = _field[x, y] switch
                    {
                        Player => Symbols.PlayerFigure,
                        Enemy => Symbols.EnemyFigure,
                        Bullet => Symbols.BulletFigure,
                        EmptyObject => Symbols.BaseFigure,
                        BarrierObject => Symbols.BarrierFigure,
                        Reward => Symbols.RewardFigure,
                    };
                    _renderBuffer[_field[x, y].X, _field[x, y].Y] = symbol;
                }
        }
    }
}


