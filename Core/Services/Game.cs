using Battle_City.Core.Map;
using Battle_City.Core.Entities;
using Battle_City.Rendering;
using Battle_City.Windows;
using System.Security.Cryptography;

namespace Battle_City.Core.Services
{
    abstract class Game
    {
        public static readonly Random Random = new Random();
        public static int Score = 0;
        public static int Level = 1;

        private static Field _field = new Field(Level);
        private static Enemy _enemy = new Enemy(_field);
        private static Player _player = new Player(_field);

        private static Renderer _renderer = new Renderer();
        private static CancellationTokenSource _cts;

        public enum State
        {
            Pause,
            Run,
            Win,
            Defeat,
            Save,
            Exit,
            Load,
            Return
        }

        public static State GameState;

        public static void Restart()
        {
            _cts?.Cancel();
            _renderer.Stop();
            Draw.Clear(); 
            Bullet.Bullets.Clear();

            _player.Health = 5;
            Score = 0;

            _field = new Field(Level);
            _player = new Player(_field);
            _enemy = new Enemy(_field);

            if (GameState == State.Pause || GameState == State.Win)
            {
                GameForm.Instance.Show();
                GameState = State.Run;
            }
            StartGame(); 
        }

        public static void StartGame()
        {
            if (GameState != State.Load)
            {
                Reward.SetRewards(Score, _field, _player);
            }
            GameForm.PictureBoxField.Invalidate();
            Draw.DrawInfo(_player);
            GameState = State.Run;
            Task.Run(() => Run());
            _renderer.Start(_field, _player, _enemy);
        }
        public static void ReturnToGame()
        {
            _renderer.Stop();
            _renderer.Start(_field, _player, _enemy);
            GameState = State.Run;
            Task.Run(() => Run());
        }
        public static void Reset()
        {
            Draw.Clear();

            _player.Health = 5;
            Score = 0;

            _field = new Field(Level);
            _player = new Player(_field);
            _enemy = new Enemy(_field);
        }
        public static void Load()
        {
            Saver.LoadGame(_field, _player, _enemy);
            Draw.InvalidateCachedField();
            var game = new GameForm();
            game.Show();
        }
        public static async Task Run()
        {
            _cts = new CancellationTokenSource();
            var token = _cts.Token;
            while (!token.IsCancellationRequested)
            {
                switch (GameState)
                {
                    case State.Run:
                        FormInput.PlayerActions(_field, _player, _enemy);
                        Reward.CheckRewards(Score, _field);
                        Score += Reward.GetReward(_player.X, _player.Y, _field);

                        if (Score == Reward.NumberOfRewards)
                        {
                            if (Level == Field.NumberOfLevels)
                                GameState = State.Win;
                            else
                            {
                                Level++;
                                Restart();
                                return;
                            }
                        }

                        Interaction.CheckCollision(_player, _enemy);
                        break;

                    case State.Defeat:
                        return;

                    case State.Pause:
                        GameForm.Instance.Invoke(() =>
                        {
                            _cts?.Cancel();
                            _renderer.Stop();
                            GameForm.Instance.Hide();

                            var defeatForm = new DefeatForm();
                            defeatForm.Show();
                        });
                        return;

                    case State.Save:
                        _renderer.Stop();  
                        
                        Saver.SaveGame(_field, _player, _enemy);
                        DialogResult result = MessageBox.Show(
                            "Game saved successfully!",
                            "Save Game",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );

                        if (result == DialogResult.OK)
                        {
                            _renderer.Start(_field, _player, _enemy);
                            GameState = State.Run;
                        }
                        break;

                    case State.Win:
                        GameForm.Instance.Invoke(() =>
                        {
                            _cts?.Cancel();
                            _renderer.Stop();
                            GameForm.Instance.Hide();

                            var winForm = new WinForm();
                            winForm.Show();
                        });
                        return;
                    case State.Exit:
                        GameForm.Instance.Invoke(() =>
                        {
                            _cts?.Cancel();
                            _renderer.Stop();

                            var escapeForm = new EscapeForm();
                            escapeForm.ShowDialog(GameForm.Instance);
                        });

                        return;

                    default:
                        break;
                }

                Draw.DrawField(_field, _player, _enemy);
                await Task.Delay(10, token);
            }
        }
    }
}
