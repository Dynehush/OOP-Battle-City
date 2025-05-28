using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_City.Core
{
    public interface IGameRenderer
    {
        public void Start();
        public void Stop();
        public void Clear();
    }
}
