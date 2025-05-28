using Battle_City;
using Battle_City.Core.Actor;
using Battle_City.Core.GameElements;
using Battle_City.Components;

namespace Battle_City.Core
{
    abstract class Game
    {
        public static readonly Random Random = new Random();
        public static int Score = 0;
        public static int Level = 1;

        private static Field _field = new Field(Level);
        private static Enemy _enemy = new Enemy(_field);
        private static Player _player = new Player(_field);

        public static ConsoleRenderer ConsoleRenderer = new ConsoleRenderer();

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
            ConsoleRenderer.Clear();
            ConsoleRenderer.Stop();

            _player.Health = 5;
            Score = 0;
            _field = new Field(Level);
            _player = new Player(_field);
            _enemy = new Enemy(_field);
            Reward.SetRewards(Score, _field, _player);

            GameState = State.Run;
            ConsoleRenderer.Init(_field, _player, _enemy);
            Run();
        }

        public static void StartGame()
        {
            Menu.ShowMenu(_field, _player, _enemy);
            GameState = State.Run;
            Run();
        }
        public static void Run()
        {
            Reward.SetRewards(Score, _field, _player);
            while (GameState == State.Run)
            {
                Bullet.CheckBulletOverlap(_player, _enemy.Bullets);
                Bullet.CheckBulletOverlap(_enemy, _player.Bullets);
                Reward.CheckRewards(Score, _field);
                Score += Reward.GetReward(_player.X, _player.Y, _field);

                if (Score == Reward.NumberOfRewards)
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
                ConsoleInput.PlayerActions(_field, _player, _enemy);
                Interaction.CheckCollision(_player, _enemy);

                if (GameState == State.Win)
                {
                    Menu.ShowWinMessage(_field, _player, _enemy);
                    return;
                }

                if (GameState == State.Defeat || GameState == State.Pause)
                {
                    Menu.ShowGameOver(_field, _player, _enemy);
                }

                if (GameState == State.Save)
                {
                    ConsoleRenderer.Stop();

                    Menu.SaveProgress(_field, _player, _enemy);

                    ConsoleRenderer.Init(_field, _player, _enemy);
                }

                if (GameState == State.Exit)
                {
                    ConsoleRenderer.Stop();

                    Menu.ExitGame(_field, _player, _enemy);

                    ConsoleRenderer.Init(_field, _player, _enemy);
                }

                if (GameState == State.Load)
                {
                    ConsoleRenderer.Init(_field, _player, _enemy);

                    GameState = State.Run;
                }

                Thread.Sleep(10);
            }
        }
    }
}
