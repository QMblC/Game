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

        #region Fields
        public Inventory Inventory;
        public Vector2 StartPos;
       
        public bool IsAlive = true;

        private float AnimationElapsed;
        private readonly float AnimationDelay = 100f;
        private bool IsNeedToFlip = false;
        private int Frame = 0;

        #endregion


        #region Properties
        public override SpriteType Type { get; set; } = SpriteType.Player;
        public override int Speed { get; set; } = 7;

        public override Rectangle Rectangle
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width + 20, Texture.Height); }
        }

        #endregion 

        public Player(Texture2D texture) : base(texture)
        {

        }

        public Player(Texture2D texture, Inventory inventory, Vector2 position) : base(texture)
        {
            Inventory = inventory;
            Position = position;
        }

        

        public void Update(List<Sprite> sprites, Map map, GameTime gameTime)
        {
            Move();

            FlipImage();

            if (Keyboard.GetState().GetPressedKeys().Contains(Keys.E))
                CollectKey(map);
            CheckCollision(sprites);

            if (Velocity == Vector2.Zero)
                Frame = 4;
            else
                CreateAnimation(gameTime);

            CorrectDiagonalSpeed();

            Position += Velocity;
            Velocity = new Vector2();

            Inventory.Update(map.MiniMap);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!IsNeedToFlip)
                spriteBatch.Draw(GameCycle.animation[Frame], Rectangle, null, Color.White, 0f, new Vector2(),
                    SpriteEffects.None, 1);
            else
                spriteBatch.Draw(GameCycle.animation[Frame], Rectangle, null, Color.White, 0f, new Vector2(),
                    SpriteEffects.FlipHorizontally, 1);
            Inventory.Draw(spriteBatch);
        }

        private void CreateAnimation(GameTime gameTime)
        {
            var animationDelay = AnimationDelay / (Speed * 0.5) + 50;
            AnimationElapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (AnimationElapsed >= animationDelay)
            {
                Frame = (Frame + 1) % 9;
                AnimationElapsed = 0;
            }
        }

        private void FlipImage()
        {
            if (Velocity.X < 0)
                IsNeedToFlip = true;
            else if (Velocity.X > 0)
                IsNeedToFlip = false;
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

        private void CollectKey(Map map)//Нужно вынести в map замену К
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
                            if(!map.Visited.Contains((key, GameCycle.textures[1])))
                                map.Visited.Add((key, GameCycle.textures[1]));
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
                Velocity.Y = -Speed;
                            
            if (key.Contains(Keys.S))
                Velocity.Y = Speed;
                
            if (key.Contains(Keys.D))
                Velocity.X = Speed;
                
            if (key.Contains(Keys.A))
                Velocity.X = -Speed;
        }
    }
}
