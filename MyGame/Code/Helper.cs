using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Code
{
    public class Helper
    {
        public static int GetDirection()
        {
            var key = Keyboard.GetState().GetPressedKeys();
            if (key.Contains(Keys.W))
                return (int)Direction.Up;
            else if (key.Contains(Keys.D))
                return (int)Direction.Right;
            else if (key.Contains(Keys.S))
                return (int)Direction.Down;
            else if (key.Contains(Keys.A))
                return (int)Direction.Left;
            return (int)Direction.None;
        }
    }
}
