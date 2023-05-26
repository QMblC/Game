using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Code
{
    public enum Direction
    {
        None,
        Up,
        Right,
        Down,
        Left
    }

    public class Player : Sprite
    {
        public Player(Texture2D texture) : base(texture)
        {

        }

        public override Rectangle Rectangle
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height); }
        }

        public void Update(List<Sprite> sprites)
        {
            Move();

            foreach (var sprite in sprites)
            {
                if (sprite == this)
                    continue;
                if ((Velocity.X > 0 && IsTouchingLeft(sprite)) ||
                    (Velocity.X < 0 & IsTouchingRight(sprite)))
                    Velocity.X = 0;

                if ((Velocity.Y > 0 && IsTouchingTop(sprite)) ||
                    (Velocity.Y < 0 & IsTouchingBottom(sprite)))
                    Velocity.Y = 0;
            }
            Position += Velocity;
            Velocity = new Vector2();
        }

        private void Move()
        {
            var direction = Helper.GetDirection();

            if (direction == 1)
                Velocity.Y = -Speed;
            else if (direction == 3)
                Velocity.Y = Speed;
            else if (direction == 2)
                Velocity.X = Speed;
            else if (direction == 4)
                Velocity.X = -Speed;        
        }
    }
}
