using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Code
{
    public class Map
    {
        public List<string> Cells = new List<string>
        {
            "############",
            "#  #    # ##",
            "# #### ## ##",
            "# #  # #  ##",
            "# # ## # ###",
            "#   #     ##",
            "# ####### ##",
            "#  #  ##  ##",
            "# ## ### ###",
            "#         ##",
            "############"
        };
        public List<Rectangle> Rectangles = new();
        public List<Sprite> Sprites = new();
        public List<Rectangle> Visited = new();
        public Vector2 StartPos => new Vector2(Cells.Count / 2 * 200, Cells.Count / 2 * 200);
        public MiniMap MiniMap;

        public void Convert(GameTime gameTime, SpriteBatch spriteBatch, List<Texture2D> textures, Player player)
        {
            
            for (var x = 0; x < Cells[0].Length; x++)
            {
                for(var y = 0; y < Cells.Count; y++)
                {
                    var playerVision = new Rectangle((int)player.Position.X - 200, (int)player.Position.Y-200, 401, 401);
                    var rect = new Rectangle(x * 200, y * 200, 200, 200);

                    if (Cells[y][x] == '#')
                    {
                        if (rect.Intersects(playerVision))
                        {
                            if (!Rectangles.Contains(rect))
                                Rectangles.Add(rect);
                            spriteBatch.Draw(textures[3], rect, null, Color.White, 0f, new Vector2(),
                                SpriteEffects.None, 0.5f);
                            if (!Visited.Contains(rect))
                                Visited.Add(rect);
                        }
                        else
                        {
                            spriteBatch.Draw(textures[0], rect, null, Color.White, 0f, new Vector2(),
                                SpriteEffects.None, -10);
                        }
                    }                 
                    if (Cells[y][x] == ' ')
                    {
                        if (rect.Intersects(playerVision))
                        {
                            if (!Rectangles.Contains(rect))
                                Rectangles.Add(rect);
                            spriteBatch.Draw(textures[1], rect, null, Color.White, 0f, new Vector2(),
                                SpriteEffects.None, 0.5f);
                            if (!Visited.Contains(rect))
                                Visited.Add(rect);
                        }
                        else
                        {
                            spriteBatch.Draw(textures[0], rect, null, Color.White,0f, new Vector2(),
                                SpriteEffects.None, -10);
                        }
                    }                
                }
            }
        }

        public void CreateSpites(List<Texture2D> textures)
        {
            for (var x = 0; x < Cells[0].Length; x++)
            {
                for (var y = 0; y < Cells.Count; y++)
                {
                    if (Cells[y][x] == '#')
                    {
                        var sprite = new Sprite(textures[1], new Vector2(x * 200, y * 200));
                        if (!Sprites.Contains(sprite))
                            Sprites.Add(sprite);
                    }
                }
            }
        }    
    }
}
