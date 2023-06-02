using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Code
{
    public class TextWindow
    {
        private Texture2D texture = GameCycle.textures[4];
        private SpriteFont font = GameCycle.fonts[1];
        public bool IsRead = false;
        private const string text = "WASD - Movement";
        private const string text2 = "E - Interaction";
        private const string text3 = "Press any button";
        private const string text4 = "to close";
        private const string text5 = "Coollect keys";
        private const string text6 = "and find an exit";

        public Rectangle Rectangle => new Rectangle(1200 - 100, 1200, 400, 200);

        public TextWindow()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Rectangle, null,  Color.LemonChiffon, 0f, Vector2.Zero, SpriteEffects.None, 1f);
            spriteBatch.DrawString(font, text, new Vector2(Rectangle.X + 50, Rectangle.Y + 20 ), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            spriteBatch.DrawString(font, text2, new Vector2(Rectangle.X + 50, Rectangle.Y + 40), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);

            spriteBatch.DrawString(font, text5, new Vector2(Rectangle.X + 50, Rectangle.Y + 75), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            spriteBatch.DrawString(font, text6, new Vector2(Rectangle.X + 50, Rectangle.Y + 95), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);

            spriteBatch.DrawString(font, text3, new Vector2(Rectangle.X + 50, Rectangle.Y + 130), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            spriteBatch.DrawString(font, text4, new Vector2(Rectangle.X + 50, Rectangle.Y + 150), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
        }
    }
}
