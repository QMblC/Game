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
        public readonly List<string> Cells;
        public readonly MiniMap MiniMap;
        public readonly int KeyCount;
        public readonly int SpotCount;

        public Map(List<string> cells, MiniMap miniMap, int keyCount, int spotCount)
        {
            Cells = cells;
            MiniMap = miniMap;
            KeyCount = keyCount;
            SpotCount = spotCount;
        }

        private readonly List<Rectangle> Rectangles = new();

        public List<Sprite> Sprites = new();
        public List<(Rectangle, Texture2D)> Visited = new();
        public List<Rectangle> Keys = new();
        private static readonly Random rnd = new();
        public bool IsLevelStarted = false;

        public static readonly int tileSize = 200;
        private static List<Texture2D> Textures => GameView.textures;


        private int Capacity => 36;
        public int Scale => 24 / Cells.Count * 10;

        public bool IsEveryKeyCollected => Keys.Count == 0;
        public Rectangle? Exit = null;
        public List<Mob> Mobs = new();

        public bool IsAbleToLeave(Player player)
        {
            if (Exit == null)
                return false;
            return IsEveryKeyCollected
                && Math.Abs(Exit.Value.Center.X - player.Rectangle.Center.X) <= 100
                && Math.Abs(Exit.Value.Center.Y - player.Rectangle.Center.Y) <= 100;
        }
        
        private List<int> GetSpots()
        {
            var spots = new List<int>();
            while (spots.Count != KeyCount)
            {
                var r = rnd.Next(SpotCount);
                if (spots.Contains(r))
                    continue;
                spots.Add(r);
            }
            IsLevelStarted = true;
            return spots;
        }

        public void Update(SpriteBatch spriteBatch, Player player)
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
        private static void DrawFogOfWar(Rectangle rect, SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Draw(texture, rect, null, Color.White, 0f, new Vector2(),
                                SpriteEffects.None, -10);
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

        public static Rectangle GetPlayersVision(Player player)
        {
            
            return new Rectangle((int)player.Rectangle.Center.X - 200, (int)player.Rectangle.Center.Y - 200, 400, 400); ;
        }

        public void CreateSpites(List<Texture2D> textures)
        {
            var spots = GetSpots();
            var c = 0;
            for (var x = 0; x < Cells[0].Length; x++)
            {
                for (var y = 0; y < Cells.Count; y++)
                {
                    if (Cells[y][x] == '#')
                    {
                        var sprite = new Sprite(textures[1], new Vector2(x * tileSize, y * tileSize), SpriteType.Wall) ;
                        if (!Sprites.Contains(sprite))
                            Sprites.Add(sprite);
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
                        if (!Sprites.Contains(mob))
                            Sprites.Add(mob);
                        if (!Mobs.Contains(mob))
                            Mobs.Add(mob);
                    }
                }
            }
        }
    } 
}
