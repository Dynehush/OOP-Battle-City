using Battle_City.Components;
using Battle_City.Core;
using Battle_City.Core.Actor;
using Battle_City.Core.GameElements;
using Battle_City.Core.Input;
using static Battle_City.Core.Actor.Entity;
using static Battle_City.Core.Actor.Player;

namespace Battle_City
{
    class ConsoleInput : IControl
    {
        public static void PlayerActions(Field _field, Player _player, Enemy _enemy)
        {
            var key = Console.ReadKey(true).Key;
            DateTime shot = DateTime.Now;
            DateTime move = DateTime.Now;
            if (move - _player.LastMoveTime >= TimeSpan.FromSeconds(0.05))
            {
                Direction? dir = key switch
                {
                    ConsoleKey.W or ConsoleKey.UpArrow => Direction.Up,
                    ConsoleKey.S or ConsoleKey.DownArrow => Direction.Down,
                    ConsoleKey.A or ConsoleKey.LeftArrow => Direction.Left,
                    ConsoleKey.D or ConsoleKey.RightArrow => Direction.Right,
                    _ => null
                };
                _player.LastMoveTime = move;
                if (dir != null)
                {
                    var (dx, dy) = _player.DirectionOffsets[dir.Value];
                    _player.PlayerDirection = dir;
                    _player.Move(dx, dy, _field);
                }
            }
            switch (key)
            {
                case ConsoleKey.Spacebar:
                    DateTime shoot = DateTime.Now;
                    if (shot - _player.LastShootTime >= TimeSpan.FromSeconds(1))
                    {
                        _player.Shoot(_enemy);
                        _player.LastShootTime = shot;
                    }
                    break;

                case ConsoleKey.F10:
                    Game.GameState = Game.State.Save;
                    break;

                case ConsoleKey.Escape:
                    Game.GameState = Game.State.Exit;
                    break;
            }
        }
    }
}
