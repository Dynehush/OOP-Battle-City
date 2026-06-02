using Battle_City.Core.Map;
using static Battle_City.Core.Entities.Actor;
using System.Numerics;
using System.Windows.Forms;
using Battle_City.Core.Entities;
using Battle_City.Core.Services;

namespace Battle_City.Rendering
{
    class Draw
    {
        private const int _cellSize = 32;
        private static Bitmap cachedFieldImage;

        private static readonly object _imageLock = new object();
        public static void DrawField(Field _field, Player _player, Enemy _enemy)
        {
            lock (_imageLock)
            {
                if (cachedFieldImage == null)
                {
                    cachedFieldImage = new Bitmap(Field.Width * _cellSize, Field.Height * _cellSize);
                    using (Graphics g = Graphics.FromImage(cachedFieldImage))
                    {
                        for (int x = 0; x < Field.Width; x++)
                        {
                            for (int y = 0; y < Field.Height; y++)
                            {
                                var obj = _field[x, y];
                                Image img = null;

                                if (obj is BarrierObject)
                                    img = Images.BarrierFigure;
                                else if (obj is EmptyObject)
                                    img = Images.EmptyFigure;

                                if (img != null)
                                {
                                    g.DrawImage(img, x * _cellSize, y * _cellSize, _cellSize, _cellSize);
                                }
                            }
                        }
                    }
                }
                Bitmap finalImage = new Bitmap(cachedFieldImage);

                using (Graphics g = Graphics.FromImage(finalImage))
                {
                    for (int x = 0; x < Field.Width; x++)
                    {
                        for (int y = 0; y < Field.Height; y++)
                        {
                            var obj = _field[x, y];
                            if (obj is Reward)
                            {
                                g.DrawImage(Images.RewardFigure, x * _cellSize, y * _cellSize, _cellSize, _cellSize);
                            }
                        }
                    }

                    if (Bullet.Bullets != null)
                    {
                        foreach (var bullet in Bullet.Bullets)
                        {
                            if (Images.BulletDirection.TryGetValue(bullet.Direction.Value, out Image value))
                            {
                                g.DrawImage(value, bullet.X * _cellSize, bullet.Y * _cellSize, _cellSize, _cellSize);
                            }
                        }
                    }

                    g.DrawImage(Images.PlayerFigure, _player.X * _cellSize, _player.Y * _cellSize, _cellSize, _cellSize);
                    g.DrawImage(Images.EnemyFigure, _enemy.X * _cellSize, _enemy.Y * _cellSize, _cellSize, _cellSize);
                }

                if (GameForm.PictureBoxField.Image != null)
                    GameForm.PictureBoxField.Image.Dispose();

                GameForm.PictureBoxField.Image = finalImage;
            }

        }

        public static void DrawInfo(Player _player)
        {
            Bitmap bmp = new Bitmap(GameForm.GameInfo.Width, GameForm.GameInfo.Height);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Black);
                g.DrawString($"Level: {Game.Level}\n"+
                    $"Score: {Game.Score}\n"+
                    $"Lives: {_player.Health}",
                             new Font("Times New Roman", 24, FontStyle.Bold),
                             Brushes.Red,
                             new PointF(10, 10)); 
            }

            GameForm.GameInfo.Image = bmp;
        }
        public static void Clear()
        {
            lock (_imageLock)
            {
                if (GameForm.PictureBoxField.Image != null)
                {
                    GameForm.PictureBoxField.Image.Dispose();
                    GameForm.PictureBoxField.Image = null;
                }

                if (cachedFieldImage != null)
                {
                    cachedFieldImage.Dispose();
                    cachedFieldImage = null;
                }

                if (GameForm.GameInfo.Image != null)
                {
                    GameForm.GameInfo.Image.Dispose();
                    GameForm.GameInfo.Image = null;
                }
            }
        }
        public static void InvalidateCachedField()
        {
            lock (_imageLock)
            {
                if (cachedFieldImage != null)
                {
                    cachedFieldImage.Dispose();
                    cachedFieldImage = null;
                }
            }
        }

    }
}
