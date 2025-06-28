using Battle_City.Core.Map;
using Battle_City.Core.Entities;
using static Battle_City.Core.Entities.Actor;

namespace Battle_City.Rendering
{
    class Renderer
    {
        private CancellationTokenSource _enemyLoopCts;
        private CancellationTokenSource _bulletLopopCts;
        private CancellationTokenSource _hitLoopCts;
        private Control _control = GameForm.PictureBoxField;
        private readonly object _renderLock = new();

        public Renderer(){}

        public void Start(Field _field, Player _player, Enemy _enemy)
        {
            StartEnemyLoop(_field, _player, _enemy);
            StartBulletLoop(_field, _player, _enemy);
            StartHitLoop(_field, _player, _enemy);
        }

        public void Stop()
        {
            _enemyLoopCts?.Cancel();
            _bulletLopopCts?.Cancel();
            _hitLoopCts?.Cancel();
        }
        private void StartHitLoop(Field _field, Player _player, Enemy _enemy)
        {
            _hitLoopCts = new CancellationTokenSource();
            Task.Run(async () =>
            {
                var token = _hitLoopCts.Token;

                while (!token.IsCancellationRequested)
                {
                    lock (_renderLock)
                    {
                        Bullet.CheckBulletOverlap(_field, _enemy, Bullet.Bullets);
                        Bullet.CheckBulletOverlap(_field, _player, Bullet.Bullets);
                    }
                    await Task.Delay(1, token);
                }
            });
        }
        private void StartEnemyLoop(Field _field, Player _player, Enemy _enemy)
        {
            _enemyLoopCts = new CancellationTokenSource();

            Task.Run(async () =>
            {
                var token = _enemyLoopCts.Token;

                while (!token.IsCancellationRequested)
                {
                    lock (_renderLock)
                    {
                        _enemy.Move(_field, _player);
                        Direction? dir = _enemy.EnemyDirection;
                        Images.EnemyFigure = Images.EnemyDirection[dir.Value];

                        if (_enemy.X - _player.X == 0 || _enemy.Y - _player.Y == 0)
                        {
                            _enemy.Shoot(_enemy.GetDirectionToEntity(_player), _field);
                        }

                        Draw.DrawField(_field, _player, _enemy);
                        Draw.DrawInfo(_player);

                        if (_control != null)
                            _control.BeginInvoke(() => _control.Invalidate());
                    }

                    await Task.Delay(500, token);
                }
            });
        }
        private void StartBulletLoop(Field _field, Player _player, Enemy _enemy)
        {
            _bulletLopopCts = new CancellationTokenSource();
            Task.Run(async () =>
            {
                var token = _bulletLopopCts.Token;

                while (!token.IsCancellationRequested)
                {
                    lock (_renderLock) 
                    {
                        Bullet.UpdateBullets(Bullet.Bullets, _field, _player, _enemy);

                        Draw.DrawField(_field, _player, _enemy);
                        Draw.DrawInfo(_player);

                        if (_control != null)
                        {
                            _control.BeginInvoke(() => _control.Invalidate());
                        }
                    }

                    await Task.Delay(200, token);
                }
            });
        }
    }
}
