using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Code
{
    public class Mob : Sprite
    {
        #region Fields
        private readonly new int Speed = 3;
        public Vector2 Destination;
        #endregion

        #region Properties
        public override SpriteType Type { get; set; }

        private bool IsAchievedDestination =>
            Math.Abs(Destination.X - Rectangle.Center.X) < 5 && Math.Abs(Destination.Y - Rectangle.Center.Y) < 5;

        public override Rectangle Rectangle
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height); }
        }

        public Rectangle Vision
        {
            get { return new Rectangle((int)Position.X - 40, (int)Position.Y - 40, Texture.Width + 80, Texture.Height + 80); }
        }
        #endregion

        public Mob(Texture2D texture, Vector2 position, SpriteType type) : base(texture, position, type)
        {

        }

        public void Update(List<Sprite> sprites, Map map, Player player)
        {
            var contrX = 0;
            var contrY = 0;
            foreach (var sprite in sprites)
            {
                if (sprite == this)
                    continue;

                SetVelocity(map, player);

                if (sprite.Type == SpriteType.Wall)
                {
                    if ((Velocity.X > 0 && IsTouchingLeft(sprite)) ||
                    (Velocity.X < 0 & IsTouchingRight(sprite)))
                    {
                        contrX = -(int)Velocity.X;
                        Destination = Vector2.Zero;
                    }
                    if ((Velocity.Y > 0 && IsTouchingTop(sprite)) ||
                        (Velocity.Y < 0 && IsTouchingBottom(sprite)))
                    {
                        contrY = -(int)Velocity.Y;
                        Destination = Vector2.Zero;
                    }
                }

                else if (sprite.Type == SpriteType.Player)
                {
                    if ((IsTouchingLeft(sprite))
                        || (IsTouchingRight(sprite))
                        || (IsTouchingTop(sprite))
                        || (IsTouchingBottom(sprite)))
                        player.IsAlive = false;
                }
            }

            CreateAnimation();

            Position += Velocity + new Vector2(contrX, contrY);
            Velocity = new Vector2();
        }

        private void SetVelocity(Map map, Player player)
        {
            if (Destination == Vector2.Zero)
                Destination = new Vector2(Rectangle.Center.X, Rectangle.Center.Y);

            if (Math.Abs(Rectangle.X - player.Rectangle.X) < 300 && Math.Abs(Rectangle.Y - player.Rectangle.Y) < 300)
                Destination = new Vector2(player.Rectangle.Center.X, player.Rectangle.Center.Y);

            else if (IsAchievedDestination)
                SetDestination(map);

            if (!IsAchievedDestination)
                FollowDestination();

            CorrectVelocity();
        }

        private void CreateAnimation()
        {
            if (Velocity.X > 0)
                Texture = GameCycle.textures[11];
            if (Velocity.X < 0)
                Texture = GameCycle.textures[10];
        }

        private void CorrectVelocity()
        {
            if (Math.Abs(Destination.X - Rectangle.Center.X) < Speed)
                Velocity.X /= Speed;
            if (Math.Abs(Destination.Y - Rectangle.Center.Y) < Speed)
                Velocity.Y /= Speed;
        }  

        private void FollowDestination()
        {
            if (Rectangle.Center.X - Destination.X < 0)
                Velocity.X = Speed;
            if (Rectangle.Center.X - Destination.X > 0)
                Velocity.X = -Speed;
            if (Rectangle.Center.Y - Destination.Y > 0)
                Velocity.Y = -Speed;
            if (Rectangle.Center.Y - Destination.Y < 0)
                Velocity.Y = Speed;
        }

        private void SetDestination(Map map)
        {
            var rnd = new Random();
            var xMove = 0;
            var yMove = 0;
            while (Math.Abs(xMove) == Math.Abs(yMove))
            {
                xMove = rnd.Next(-1, 2);
                yMove = rnd.Next(-1, 2);
                if ((int)Position.Y / Map.tileSize + yMove > map.Cells.Count - 1
                    || (int)Position.X / Map.tileSize + xMove > map.Cells.Count - 1
                    || (int)Position.X / Map.tileSize + xMove < 0
                    || (int)Position.Y / Map.tileSize + yMove < 0)
                    continue;
                if (map.Cells[(int)Position.Y / Map.tileSize + yMove][(int)Position.X / Map.tileSize + xMove] == '#')
                    continue;
            }
            Destination = new Vector2(
                (Rectangle.Center.X / Map.tileSize + xMove) * Map.tileSize + 100,
                (Rectangle.Center.Y / Map.tileSize + yMove) * Map.tileSize + 100);
        }
    }

}
