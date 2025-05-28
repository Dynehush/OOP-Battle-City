using Battle_City.Core.Actor;
using Battle_City.Core.GameElements;
using System.Text.Json;

namespace Battle_City.Core
{
    static class Saver
    {
        private static string _filePath = "C:\\Uni 2 semester\\OOP\\projects\\Battle City Beta 1.6\\Battle City\\Battle City\\Core\\Field.txt";
        public static void SaveGame(Field _field, Player _player, Enemy _enemy)
        {
            var gameData = new
            {
                Game.Level,
                Game.Score,
                Lives = _player.Health,
                PlayerPosition = new { _player.X, _player.Y },
                EnemyPosition = new { _enemy.X, _enemy.Y },
                Rewards = Reward._rewardPositions.Select(pos => new { X = pos.Item1, Y = pos.Item2 }).ToList(),
                Field = _field.GetAllObjects()
                            .Select(obj => new { obj.X, obj.Y })
                            .ToList()
            };


            var json = JsonSerializer.Serialize(gameData, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }

        public static void LoadGame(Field _field, Player _player, Enemy _enemy)
        {
            var json = File.ReadAllText(_filePath);
            var gameData = JsonSerializer.Deserialize<JsonElement>(json);

            Game.Level = gameData.GetProperty("Level").GetInt32();
            Game.Score = gameData.GetProperty("Score").GetInt32();
            _player.Health = gameData.GetProperty("Lives").GetInt32();
            _player.X = gameData.GetProperty("PlayerPosition").GetProperty("X").GetInt32();
            _player.Y = gameData.GetProperty("PlayerPosition").GetProperty("Y").GetInt32();
            _enemy.X = gameData.GetProperty("EnemyPosition").GetProperty("X").GetInt32();
            _enemy.Y = gameData.GetProperty("EnemyPosition").GetProperty("Y").GetInt32();

            Reward._rewardPositions.Clear();
            foreach (var reward in gameData.GetProperty("Rewards").EnumerateArray())
            {
                Reward._rewardPositions.Add((
                    reward.GetProperty("X").GetInt32(),
                    reward.GetProperty("Y").GetInt32()));
            }

            _field = new Field(Game.Level);
            foreach (var obj in gameData.GetProperty("Field").EnumerateArray())
            {
                int x = obj.GetProperty("X").GetInt32();
                int y = obj.GetProperty("Y").GetInt32();

                if (_field[x, y] is EmptyObject)
                    _field[x, y] = new EmptyObject(x, y);
                else if (_field[x, y] is BarrierObject)
                    _field[x, y] = new BarrierObject(x, y);
                else if (_field[x, y] is Reward)
                    _field[x, y] = new Reward(x, y);
                else if (_field[x, y] is Player)
                    _field[x, y] = new Player(_field);
                else if (_field[x, y] is Enemy)
                    _field[x, y] = new Enemy(_field);
            }
        }
    }

}
