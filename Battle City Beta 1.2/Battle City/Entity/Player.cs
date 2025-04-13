using Battle_City;
using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Numerics;

namespace Battle_City
{
    class Player : BaseObject
    {
        private static int _x = 2;
        private static int _y = 2;
        private static readonly string _playerFigure = "🛦";
        public static readonly string _playerBullet = "🔴";
        public List<Bullet> Bullets { get; } = new List<Bullet>();

        public new int X
        {
            get => _x;
            set
            {
                if (value >= 0 && value < Field.WIDTH)
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
                if (value >= 0 && value < Field.HEIGHT)
                {
                    _y = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Player cannot move outside the field");
                }
            }
        }

        public Player(Field _field) : base(_x, _y, _playerFigure)
        {
            while (!_field.IsWalkable(X, Y))
            {
                X++;
                if (X >= Field.WIDTH)
                {
                    X = 2;
                    Y++;
                }
            }
        }

        public override void Display() { Console.Write(_playerFigure); }
        public static void MovePlayer(Player _player, Field _field, Reward _reward, Enemy _enemy)
        {

            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.W: _player.Move(0, -1, _field); break;
                case ConsoleKey.S: _player.Move(0, 1, _field); break;
                case ConsoleKey.A: _player.Move(-1, 0, _field); break;
                case ConsoleKey.D: _player.Move(1, 0, _field); break;

                case ConsoleKey.UpArrow: _player.Move(0, -1, _field); break;
                case ConsoleKey.DownArrow: _player.Move(0, 1, _field); break;
                case ConsoleKey.LeftArrow: _player.Move(-1, 0, _field); break;
                case ConsoleKey.RightArrow: _player.Move(1, 0, _field); break;

                case ConsoleKey.Spacebar: _player.Shoot(_field, _reward, _enemy, _player); break;
            }

        }

        public void Move(int dx, int dy, Field _field)
        {
            int newX = X + dx;
            int newY = Y + dy;

            if (newX >= Field.WIDTH || newY >= Field.HEIGHT || newX < 0 || newY < 0)
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
                Bullets.Add(new Bullet(X, Y, 1, 1, _playerBullet));
            }
            else if (_player.X > _enemy.X && _player.Y < _enemy.Y)
            {
                Bullets.Add(new Bullet(X, Y, -1, 1, _playerBullet));
            }
            else if (_player.X < _enemy.X && _player.Y > _enemy.Y)
            {
                Bullets.Add(new Bullet(X, Y, 1, -1, _playerBullet));
            }
            else if (_player.X > _enemy.X && _player.Y > _enemy.Y)
            {
                Bullets.Add(new Bullet(X, Y, -1, -1, _playerBullet));
            }
            else if (_player.X < _enemy.X && _player.Y == _enemy.Y)
            {
                Bullets.Add(new Bullet(X, Y, 1, 0, _playerBullet));
            }
            else if (_player.X > _enemy.X && _player.Y == _enemy.Y)
            {
                Bullets.Add(new Bullet(X, Y, -1, 0, _playerBullet));
            }
            else if (_player.X == _enemy.X && _player.Y > _enemy.Y)
            {
                Bullets.Add(new Bullet(X, Y, 0, -1, _playerBullet));
            }
            else if (_player.X == _enemy.X && _player.Y < _enemy.Y)
            {
                Bullets.Add(new Bullet(X, Y, 0, 1, _playerBullet));
            }

            Bullet.UpdateBullets(Bullets, _field, _reward);
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
