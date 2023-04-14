using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class GameCycle : IGameplayModel
    {
        public event EventHandler<GameplayEventArgs> Updated = delegate { };

        private Vector2 pos = new Vector2(1000, 300);
        private int speed = 5;

        public void MovePlayer(IGameplayModel.Direction direction)
        {
            switch (direction)
            {
                case IGameplayModel.Direction.Forward:
                    {
                        pos += new Vector2(0, -speed);
                        break;
                    }
                case IGameplayModel.Direction.Right:
                    {
                        pos += new Vector2(speed, 0);
                        break;
                    }
                case IGameplayModel.Direction.Backward:
                    {
                        pos += new Vector2(0, speed);
                        break;
                    }
                case IGameplayModel.Direction.Left:
                    {

                        pos += new Vector2(-speed, 0);
                        break;
                    }
            }
        }

        public void Update()
        {
            pos += new Vector2(0, 0);
            Updated.Invoke(this, new GameplayEventArgs { PlayerPos = pos });
        }
    }
}
