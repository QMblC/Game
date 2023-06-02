using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Code
{
    public enum SpriteType
    {
        Wall,
        Player,
        Enemy
    }

    public class Sprite : IGameObject
    {
        #region Fields
        public Texture2D Texture;
        public Vector2 Velocity;
        public Vector2 Position;
        #endregion

        #region Properties
        public virtual SpriteType Type { get; set; }
        public virtual int Speed { get; set; }

        public virtual Rectangle Rectangle
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, 200, 200);  }
        }
        #endregion
        public Sprite(Texture2D texture)
        {
            Texture = texture;
        }

        public Sprite(Texture2D texture, Vector2 position, SpriteType type)
        {
            Texture = texture;
            Position = position;
            Type = type;
        }

        public void Update(GameTime gameTime, SpriteBatch sprite)
        {

        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(Texture, Rectangle, null, Color.White, 0f, new Vector2(),
                                SpriteEffects.None,0.98f);

        }

        #region Collision
        public bool IsTouchingLeft(Sprite sprite)
        {
            return Rectangle.Right + Velocity.X > sprite.Rectangle.Left &&
              Rectangle.Left < sprite.Rectangle.Left &&
              Rectangle.Bottom > sprite.Rectangle.Top &&
              Rectangle.Top < sprite.Rectangle.Bottom;
        }

        public bool IsTouchingRight(Sprite sprite)
        {
            return Rectangle.Left + Velocity.X < sprite.Rectangle.Right &&
              Rectangle.Right > sprite.Rectangle.Right &&
              Rectangle.Bottom > sprite.Rectangle.Top &&
              Rectangle.Top < sprite.Rectangle.Bottom;
        }

        public bool IsTouchingTop(Sprite sprite)
        {
            return Rectangle.Bottom + Velocity.Y > sprite.Rectangle.Top &&
              Rectangle.Top < sprite.Rectangle.Top &&
              Rectangle.Right > sprite.Rectangle.Left &&
              Rectangle.Left < sprite.Rectangle.Right;
        }

        public bool IsTouchingBottom(Sprite sprite)
        {
            return Rectangle.Top + Velocity.Y < sprite.Rectangle.Bottom &&
              Rectangle.Bottom > sprite.Rectangle.Bottom &&
              Rectangle.Right > sprite.Rectangle.Left &&
              Rectangle.Left < sprite.Rectangle.Right;
        }
        #endregion
    }
}
