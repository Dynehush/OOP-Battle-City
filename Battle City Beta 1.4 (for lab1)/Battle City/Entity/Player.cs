

namespace Battle_City
{
    class Player : BaseObject
    {
        private int _x = 2;
        private int _y = 2;
        internal static string PlayerFigure = "→";
        public static readonly string PlayerBullet = "0";
        private bool _isShot = false;

        internal static readonly Dictionary<string, string> _playerDirection = new Dictionary<string, string>
        {
            {"up", "↑" },
            {"down", "↓" },
            {"left", "←" },
            {"right", "→" }
        };
        public List<Bullet> Bullets { get; } = new List<Bullet>();

        public new int X
        {
            get => _x;
            set
            {
                if (value >= 0 && value < Field.Width)
                {
                    _x = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Player cannot move outside the field");
                }
            }
        }

        public new int Y
        {
            get => _y;
            set
            {
                if (value >= 0 && value < Field.Height)
                {
                    _y = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Player cannot move outside the field");
                }
            }
        }

        public Player(Field _field) : base(0, 0, PlayerFigure)
        {
            X = _x;
            Y = _y;

            while (!_field.IsWalkable(X, Y))
            {
                X++;
                if (X >= Field.Width)
                {
                    X = 2;
                    Y++;
                }
            }
        }

        public override void DrawToBuffer(char[,] buffer)
        {
            buffer[X, Y] = PlayerFigure[0];
        }

        public static void PlayerActions(Player _player, Field _field, Reward _reward, Enemy _enemy)
        {
            var key = Console.ReadKey(true).Key;
            bool isSpacebarPressed = false;
            switch (key)
            {
                case ConsoleKey.W: _player.Move(0, -1, _field); PlayerFigure = _playerDirection["up"]; break;
                case ConsoleKey.S: _player.Move(0, 1, _field); PlayerFigure = _playerDirection["down"]; break;
                case ConsoleKey.A: _player.Move(-1, 0, _field); PlayerFigure = _playerDirection["left"]; break;
                case ConsoleKey.D: _player.Move(1, 0, _field); PlayerFigure = _playerDirection["right"]; break;

                case ConsoleKey.UpArrow: _player.Move(0, -1, _field); PlayerFigure = _playerDirection["up"]; break;
                case ConsoleKey.DownArrow: _player.Move(0, 1, _field); PlayerFigure = _playerDirection["down"]; break;
                case ConsoleKey.LeftArrow: _player.Move(-1, 0, _field); PlayerFigure = _playerDirection["left"]; break;
                case ConsoleKey.RightArrow: _player.Move(1, 0, _field); PlayerFigure = _playerDirection["right"]; break;

                case ConsoleKey.Spacebar: 
                    if (!_player._isShot)
                        _player.Shoot(_field, _reward, _enemy, _player);
                        _player._isShot = true;
                        isSpacebarPressed = true;
                    break;

                case ConsoleKey.F10: Game.GameState = Game.State.Save; break;

                case ConsoleKey.Escape: Game.GameState = Game.State.Exit; break;
            }
            if (!isSpacebarPressed)
            {
                _player._isShot = false; 
            }
        }

        

        public void Move(int dx, int dy, Field _field)
        {
            int newX = X + dx;
            int newY = Y + dy;

            if (newX >= Field.Width || newY >= Field.Height || newX < 0 || newY < 0)
            {
                Console.WriteLine("Player cannot move outside the field.");
                return;
            }

            if (_field.IsWalkable(newX, newY))
            {
                X = newX;
                Y = newY;
            }
        }

        public void Shoot(Field _field, Reward _reward, Enemy _enemy, Player _player)
        {
            if (_player.X < _enemy.X && _player.Y < _enemy.Y)
            {
                Bullets.Add(new Bullet(X, Y, 1, 1, PlayerBullet));
            }
            else if (_player.X > _enemy.X && _player.Y < _enemy.Y)
            {
                Bullets.Add(new Bullet(X, Y, -1, 1, PlayerBullet));
            }
            else if (_player.X < _enemy.X && _player.Y > _enemy.Y)
            {
                Bullets.Add(new Bullet(X, Y, 1, -1, PlayerBullet));
            }
            else if (_player.X > _enemy.X && _player.Y > _enemy.Y)
            {
                Bullets.Add(new Bullet(X, Y, -1, -1, PlayerBullet));
            }
            else if (_player.X < _enemy.X && _player.Y == _enemy.Y)
            {
                Bullets.Add(new Bullet(X, Y, 1, 0, PlayerBullet));
            }
            else if (_player.X > _enemy.X && _player.Y == _enemy.Y)
            {
                Bullets.Add(new Bullet(X, Y, -1, 0, PlayerBullet));
            }
            else if (_player.X == _enemy.X && _player.Y > _enemy.Y)
            {
                Bullets.Add(new Bullet(X, Y, 0, -1, PlayerBullet));
            }
            else if (_player.X == _enemy.X && _player.Y < _enemy.Y)
            {
                Bullets.Add(new Bullet(X, Y, 0, 1, PlayerBullet));
            }
        }


        public static void CheckCollision(Player _player, Enemy _enemy)
        {
            if (_player.X == _enemy.X && _player.Y == _enemy.Y)
            {
                Game.GameState = Game.State.Pause;
                Menu.ShowGameOver();
            }
        }
    }
}
