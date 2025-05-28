using Battle_City.Core.Actor;
using Battle_City.Core.GameElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Battle_City.Core.Actor.Player;

namespace Battle_City.Core.Input
{
    interface IControl
    {
        public static void PlayerActions(Field _field, Player _player, Enemy _enemy) {}
    }
}
