using Battle_City.Core.Entities;
using Battle_City.Core.Map;

namespace Battle_City.Core.Interfaces
{
    public interface IGameRenderer
    {
        public void Start(Field _field, Player _player, Enemy _enemy);
        public void Stop();
    }
}
