using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MyGame.Code
{
    public class MiniMap
    {
        #region Fields
        public readonly Texture2D BackGround;
        public Vector2 Position;
        #endregion
        public Rectangle Rectangle => new Rectangle((int)Position.X, (int)Position.Y, 250, 250);
        public MiniMap(Texture2D backGround, Vector2 pos)
        {
            BackGround = backGround;
            Position = pos;
        }
        public void Draw(SpriteBatch spriteBatch, Map map)
        {

            var customScale = map.Scale;
            spriteBatch.Draw(
                BackGround, Rectangle, null, Color.BurlyWood, 0f, 
                new Vector2(), SpriteEffects.None, 0.8f);
            foreach (var item in map.Visited)
            {
                var rect = new Rectangle(
                    item.Item1.X / Map.tileSize * customScale + (int)Position.X + 5,
                    item.Item1.Y / Map.tileSize * customScale + (int)Position.Y + 5,
                    customScale, customScale);

                spriteBatch.Draw(item.Item2, rect, null,
                    Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1);
            }
        }

        public void Update(Player player)
        {
            Position = player.Position + new Vector2(-605, 169);
        }
    }
}
