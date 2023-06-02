using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Code
{
    public class Button
    {
        private int xMove;
        private int yMove;

        public Texture2D Texture = GameCycle.textures[4];
        private string text;
        private SpriteFont font = GameCycle.fonts[0];

        private MouseState mouseState = Mouse.GetState();
        private MouseState previousState;

        private bool IsSelected;
        
        public Rectangle Rectangle => new Rectangle(
            (int)(1280 - font.MeasureString(text).X - 30) / 2 + xMove,
            (int)(720 - font.MeasureString(text).Y - 20) / 2 + yMove,
            30 + (int)font.MeasureString(text).X,
            20 + (int)font.MeasureString(text).Y);

        public bool IsClicked
        {
            get
            {
                previousState = mouseState;
                mouseState = Mouse.GetState();

                IsSelected = false;

                var mouseRectangle = new Rectangle(mouseState.X, mouseState.Y, 1, 1);

                if (Rectangle.Intersects(mouseRectangle))
                {
                    IsSelected = true;
                    if (previousState.LeftButton == ButtonState.Pressed
                    && mouseState.LeftButton == ButtonState.Released)
                        return true;
                }
                
                return false;
            }
        }

        public Button(int xMove, int yMove, string text)
        {
            this.xMove = xMove;
            this.yMove = yMove;
            this.text = text;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var color = IsSelected ? Color.LightGray : Color.DarkGray;
            spriteBatch.Draw(Texture, Rectangle, color);
            spriteBatch.DrawString(font, text, new Vector2(
                    (1280 - font.MeasureString(text).X) / 2 + xMove,
                    (720 - font.MeasureString(text).Y) / 2 + yMove),
                    Color.Black);
        }
    }
}
