using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_City
{
    class BaseObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Symbol { get; set; }
        public BaseObject(int x, int y, string symbol)
        {
            X = x;
            Y = y;
            Symbol = symbol;
        }
        public virtual void Display()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(Symbol);
        }
    }
}
