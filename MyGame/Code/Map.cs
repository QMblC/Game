using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Code
{
    public class Map
    {
        #region Fields
        public readonly List<string> Cells;
        public readonly MiniMap MiniMap;
        public readonly int KeyCount;
        private readonly int SpotCount;

        private readonly List<Rectangle> Rectangles = new();
        public readonly List<(Rectangle, Texture2D)> Visited = new();
        public readonly List<Rectangle> Keys = new();
        public readonly List<Mob> Mobs = new();

        public Rectangle? Exit = null;

        public bool IsLevelStarted = false;

        private static readonly Random Rnd = new();
        public const int tileSize = 200;
        #endregion
       
        #region Properties
        private static List<Texture2D> Textures => GameCycle.textures;  
        public int Scale => 24 / Cells.Count * 10;
        public bool IsEveryKeyCollected => Keys.Count == 0;
        private int Capacity
        {
            get { return Cells.Count > 0 ? 36 * 2 / (24 / Cells.Count) : 36; }
        }
        public List<int> Spots
        {
            get
            {
                var spots = new List<int>();
                while (spots.Count != KeyCount)
                {
                    var r = Rnd.Next(SpotCount);
                    if (spots.Contains(r))
                        continue;
                    spots.Add(r);
                }
                IsLevelStarted = true;
                return spots;
            }
        }
        #endregion

        public Map(MiniMap miniMap, Level level)
        {
            MiniMap = miniMap;
            Cells = level.Cells;
            KeyCount = level.KeyCount;
            SpotCount = level.SpotCount;
        }

        public bool IsAbleToLeave(Player player)
        {
            if (Exit == null)
                return false;

            return IsEveryKeyCollected
                && Math.Abs(Exit.Value.Center.X - player.Rectangle.Center.X) <= 100
                && Math.Abs(Exit.Value.Center.Y - player.Rectangle.Center.Y) <= 100;
        }  

        public void Draw(SpriteBatch spriteBatch, Player player)
        {
            for (var x = 0; x < Cells[0].Length; x++)
            {
                for(var y = 0; y < Cells.Count; y++)
                {
                    var playerVision = GetPlayersVision(player);     
                    var rect = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);

                    if (Cells[y][x] == '#')

                        if (rect.Intersects(playerVision))
                            DrawTile(rect, spriteBatch, Textures[3]);
                        else
                            DrawFogOfWar(rect, spriteBatch, Textures[0]);
                    else if (Cells[y][x] == 'K')

                        if (Keys.Contains(rect))
                            if (rect.Intersects(playerVision))
                                DrawTile(rect, spriteBatch, Textures[7]);
                            else
                                DrawFogOfWar(rect, spriteBatch, Textures[0]);
                        else
                            if (rect.Intersects(playerVision))
                                DrawTile(rect, spriteBatch, Textures[1]);
                            else
                                DrawFogOfWar(rect, spriteBatch, Textures[0]);

                    else if (Cells[y][x] == 'S')
                    {
                        if (Exit == null)
                            Exit = rect;
                        if (rect.Intersects(playerVision))
                            DrawTile(rect, spriteBatch, Textures[5]);
                        else
                            DrawFogOfWar(rect, spriteBatch, Textures[0]);
                    }
                    else if (Cells[y][x] == ' ' || Cells[y][x] == 'M')

                        if (rect.Intersects(playerVision))
                            DrawTile(rect, spriteBatch, Textures[1]);
                        else
                            DrawFogOfWar(rect, spriteBatch, Textures[0]);

                    if (Visited.Count > Capacity)
                        Visited.RemoveAt(0);
                }
            }
        }     

        private void DrawTile(Rectangle rect, SpriteBatch spriteBatch, Texture2D texture)
        {
            if (!Rectangles.Contains(rect))
                Rectangles.Add(rect);

            spriteBatch.Draw(texture, rect, null, Color.White, 0f, new Vector2(),
                SpriteEffects.None, 0.5f);

            if (Visited.Select(x => x.Item1).Contains(rect))
                Visited.RemoveAt(Visited.IndexOf((rect, texture)));

            Visited.Add((rect, texture));
        }

        private static void DrawFogOfWar(Rectangle rect, SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Draw(texture, rect, null, Color.White, 0f, new Vector2(),
                                SpriteEffects.None, -10);
        }

        public static Rectangle GetPlayersVision(Player player)
        { 
            return new Rectangle(player.Rectangle.Center.X - 200, player.Rectangle.Center.Y - 200, 400, 400); ;
        }

        public List<Sprite> CreateSpites(List<Texture2D> textures)
        {
            var sprites = new List<Sprite>();
            var spots = Spots;
            var c = 0;
            for (var x = 0; x < Cells[0].Length; x++)
            {
                for (var y = 0; y < Cells.Count; y++)
                {
                    if (Cells[y][x] == '#')
                    {
                        var sprite = new Sprite(textures[1], new Vector2(x * tileSize, y * tileSize), SpriteType.Wall) ;
                        if (!sprites.Contains(sprite))
                            sprites.Add(sprite);
                    }
                    if (Cells[y][x] == 'K')
                    {
                        var rect = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);

                        if (spots.Contains(c))
                            if (!Keys.Contains(rect))
                                Keys.Add(rect);
                        c++;
                    }
                    if(Cells[y][x] == 'M')
                    {
                        var mob = new Mob(textures[10], new Vector2(x * tileSize + tileSize / 4, y * tileSize + tileSize / 4), SpriteType.Enemy);
                        if (!sprites.Contains(mob))
                            sprites.Add(mob);
                        if (!Mobs.Contains(mob))
                            Mobs.Add(mob);
                    }
                }
            }
            return sprites;
        }
    } 
}
