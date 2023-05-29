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
        public Inventory Inventory;
        public Vector2 StartPos;
        public override SpriteType Type { get; set; } = SpriteType.Player;
        public override int Speed { get; set; } = 7;
        public bool IsAlive = true;

        public Player(Texture2D texture) : base(texture)
        {

        }

        public override Rectangle Rectangle
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width + 20, Texture.Height); }
        }

        public void Update(List<Sprite> sprites, Map map, GameTime gameTime)
        {
            if (Keyboard.GetState().GetPressedKeys().Contains(Keys.LeftAlt))
                Speed = 4;
            else
                Speed = 7;
            Move();


            if (Keyboard.GetState().GetPressedKeys().Contains(Keys.E))
                CollectKey(map);
            CheckCollision(sprites);

            CorrectDiagonalSpeed();

            Position += Velocity;
            Velocity = new Vector2();
        }

        private void CheckCollision(List<Sprite> sprites)
        {
            foreach (var sprite in sprites)
            {
                if (sprite == this)
                    continue;
                if (sprite.Type == SpriteType.Wall)
                {
                    if ((Velocity.X > 0 && IsTouchingLeft(sprite)) ||
                    (Velocity.X < 0 & IsTouchingRight(sprite)))
                        Velocity.X = 0;

                    if ((Velocity.Y > 0 && IsTouchingTop(sprite)) ||
                        (Velocity.Y < 0 && IsTouchingBottom(sprite)))
                        Velocity.Y = 0;
                }
            }
        }

        private void CollectKey(Map map)
        {
            Rectangle? delete = null;
            foreach(var key in map.Keys)
            {
                if (Math.Abs(key.Center.X - Rectangle.Center.X) <= 100 
                    && Math.Abs(key.Center.Y - Rectangle.Center.Y) <= 100)
                {
                    var str = new StringBuilder(map.Cells[key.Y / Map.tileSize]);
                    str[key.X / Map.tileSize] = ' ';
                    map.Cells[key.Y / Map.tileSize] = str.ToString();
                    var element = map.Visited.Select(r => r.Item1).First(r => r == key);
                    delete = key;
                    Inventory.ChangeKeyInInventory();
                    for (var i = 0; i < map.Visited.Count; i++)
                    {
                        if (element == map.Visited[i].Item1)
                        {
                            map.Visited.RemoveAt(i);
                            if(!map.Visited.Contains((key, GameView._textures[1])))
                                map.Visited.Add((key, GameView._textures[1]));
                            break;
                        }
                            
                    }
                }
            }
            if (delete != null)
                map.Keys.Remove(delete.Value);
        }

        private void CorrectDiagonalSpeed()
        {
            if (Math.Abs(Velocity.X) == Math.Abs(Velocity.Y))
            {
                Velocity.X *= (float)Math.Sqrt(2) / 2;
                Velocity.Y *= (float)Math.Sqrt(2) / 2;
            }
        }

        private void Move()
        {
            var key = Keyboard.GetState().GetPressedKeys();
            if (key.Contains(Keys.W))
            {
                Velocity.Y = -Speed;
            }
                            
            if (key.Contains(Keys.S))
            {
                Velocity.Y = Speed;
            }
                
            if (key.Contains(Keys.D))
            {
                Velocity.X = Speed;
            }
                
            if (key.Contains(Keys.A))
            {
                Velocity.X = -Speed;
            }
        }
    }
}
