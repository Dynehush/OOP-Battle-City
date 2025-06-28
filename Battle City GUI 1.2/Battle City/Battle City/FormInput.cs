using Battle_City.Core.Map;
using static Battle_City.Core.Entities.Actor;
using Battle_City.Core.Entities;
using Battle_City.Core.Interfaces;
using Battle_City.Core.Services;
using Battle_City.Rendering;


namespace Battle_City
{
    class FormInput : IControl
    {
        private static Keys? _lastKeyPressed = null;
        public static void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            _lastKeyPressed = e.KeyCode;
        }
        public static void PlayerActions(Field _field, Player _player, Enemy _enemy)
        {
            KeyEventArgs e = _lastKeyPressed.HasValue ? new KeyEventArgs(_lastKeyPressed.Value) : null;
            if (e == null) return;
            DateTime now = DateTime.Now;

            if (now - _player.LastMoveTime >= TimeSpan.FromSeconds(0.1) && Game.GameState == Game.State.Run)
            {
                Direction? dir = e.KeyCode switch
                {
                    Keys.W or Keys.Up => Direction.Up,
                    Keys.S or Keys.Down => Direction.Down,
                    Keys.A or Keys.Left => Direction.Left,
                    Keys.D or Keys.Right => Direction.Right,
                    _ => null
                };
                _player.LastMoveTime = now;

                if (dir != null)
                {
                    var (dx, dy) = _player.DirectionOffsets[dir.Value];
                    _player.Move(dx, dy, _field);
                    Images.PlayerFigure = Images.PlayerDirection[dir.Value];
                    _player.PlayerDirection = dir;

                    _lastKeyPressed = null;
                }
            }

            if (e.KeyCode == Keys.Space && now - _player.LastShootTime >= TimeSpan.FromSeconds(1))
            {
                _player.Shoot(_player.PlayerDirection, _field);
                _player.LastShootTime = now;
                _lastKeyPressed = null;
            }

            if (e.KeyCode == Keys.F10)
            {
                Game.GameState = Game.State.Save;
                _lastKeyPressed = null;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Game.GameState = Game.State.Exit;
                _lastKeyPressed = null;
            }

            Draw.DrawField(_field, _player, _enemy);
            Draw.DrawInfo(_player);
        }
    }
}
