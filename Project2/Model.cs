using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Test
{
    public class GameCycle : IGameplayModel
    {
        public event EventHandler<GameplayEventArgs> Updated = delegate { };


        public int PlayerId { get; set; }
        public Dictionary<int, IAlive> Objects { get; set; }

        public void MovePlayer(IGameplayModel.Direction direction)
        {
            var player = (Player)Objects[PlayerId];
            switch (direction)
            {
                case IGameplayModel.Direction.Forward:
                    {
                        player.Pos += new Vector2(0, -5);
                        break;
                    }
                case IGameplayModel.Direction.Right:
                    {
                        player.Pos += new Vector2(5, 0);
                        break;
                    }
                case IGameplayModel.Direction.Backward:
                    {
                        player.Pos += new Vector2(0, 5);
                        break;
                    }
                case IGameplayModel.Direction.Left:
                    {

                        player.Pos += new Vector2(-5, 0);
                        break;
                    }
            }
        }

        public void Update()
        {
            var currentPos = Objects[PlayerId].Pos;
            foreach (var obj in Objects.Values)
                obj.Update();
            var shift = Objects[PlayerId].Pos - currentPos;
            Updated.Invoke(this, new GameplayEventArgs { Objects = this.Objects, POVShift = shift });
        }

        public void Initialize()
        {
            Objects = new Dictionary<int, IAlive>();
            var player = new Player(1, 100, new Vector2(1000, 300), new Vector2(0, 0));
            Objects.Add(1, player);
            PlayerId = 1;
            var mob = new Mob(2, 100, new Vector2(1600, 590));
            Objects.Add(2, mob);
        }
    }
}
