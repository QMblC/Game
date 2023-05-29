using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Code
{
    public class Inventory
    {
        public Texture2D Background;
        public Vector2 Position;
        public List<Texture2D> Health;
        public List<Texture2D> Keys = new();

        public Inventory(Texture2D background, int keyCount)
        {
            Background = background;
            for (var i = 0; i < keyCount; i++)
                Keys.Add(GameView._textures[9]);
        }

        public void ChangeKeyInInventory()
        {
            for(var i = 0; i < Keys.Count; i++)
            {
                if (Keys[i] == GameView._textures[9])
                {
                    Keys[i] = GameView._textures[6];
                    break;
                }           
            }
        }

        public void Update(MiniMap miniMap)
        {
            Position = miniMap.Position + new Vector2(1280 - 200, -465);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var pos = 0;
            spriteBatch.Draw(Background, Position, null, Color.BurlyWood, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.9f);
            foreach (var key in Keys)
            {
                var p = new Vector2((int)Position.X + 50, (int)Position.Y + 50 + pos);
                spriteBatch.Draw(key,
                    p,
                    null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                pos += 70;
            }
        }
    }
}
