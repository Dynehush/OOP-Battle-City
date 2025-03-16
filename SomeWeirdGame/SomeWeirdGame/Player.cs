using SomeWeirdGame;
using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Numerics;
using System.Windows.Forms;

using SomeWeirdGame;
using System;

class Player
{
    private int x = 2;
    private int y = 2;
    public readonly string PlayerFigure = "♟";

    public int X
    {
        get => x;
        private set
        {
            if (value >= 0 && value < Field.WIDTH)
            {
                x = value;
            }
        }
    }

    public int Y
    {
        get => y;
        private set
        {
            if (value >= 0 && value < Field.HEIGHT)
            {
                y = value;
            }
        }
    }

    public Player(Field field)
    {
        while (!field.IsWalkable(X, Y))
        {
            X++;
            if (X >= Field.WIDTH)
            {
                X = 2;
                Y++;
            }
        }
    }

    public void DisplayPlayer()
    {
        Console.Write(PlayerFigure);
    }

    public void Move(int x, int y, Field field)
    {
        int newX = X + x;
        int newY = Y + y;

        if (field.IsWalkable(newX, newY))
        {
            X = newX;
            Y = newY;
        }
    }
}
