using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Code
{
    public class MiniMap
    {
        public readonly Texture2D BackGround;
        public Vector2 Position;
        public Rectangle Rectangle => new Rectangle((int)Position.X, (int)Position.Y, 200, 200);
        public MiniMap(Texture2D backGround, Vector2 pos)
        {
            BackGround = backGround;
            Position = pos;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BackGround, Rectangle, null, Color.White, 0f, new Vector2(),
                                SpriteEffects.None, 1);
        }

        public void Update(Player player)
        {
            Position = player.Position + new Vector2(-615, -318);
        }
    }
}
