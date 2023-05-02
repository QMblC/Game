using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Xna.Framework;

namespace Test
{
    public interface IGameplayModel
    {
        public event EventHandler<GameplayEventArgs> Updated;
        public int PlayerId { get; set; }
        public Dictionary<int, IAlive> Objects { get; set; }

        public void Update();
        public void MovePlayer(Direction direction);
        public void Initialize();

        enum Direction
        {
            Forward,
            Right,
            Backward,
            Left
        }
    }

    public class ControlsEventArgs : EventArgs
    {
        public IGameplayModel.Direction Direction { get; set; }
    }

    public interface IGameplayView
    {
        event EventHandler CycleFinished;
        event EventHandler<ControlsEventArgs> PlayerMoved;

        void LoadGameCycleParameters(Dictionary<int, IAlive> _objects, Vector2 POVShift);
        void Run();
    }

    public class GameplayEventArgs : EventArgs
    {
        public Dictionary<int, IAlive> Objects { get; set; }
        public Vector2 POVShift { get; set; }
    }

    public interface IAlive
    {
        public int ImageId { get; set; }
        public int Health { get; set; }
        public Vector2 Pos { get; set; }

        public void Update();
    }

    public class Mob : IAlive
    {
        public int ImageId { get; set; }
        public int Health { get; set; }
        public Vector2 Pos { get; set; }

        public Vector2 Speed = new Vector2(-3, 0);

        public void Update()
        {
            
        }

        public Mob(int imageId, int health, Vector2 pos)
        {
            ImageId = imageId;
            Health = health;
            Pos = pos;
        }
    }

    public class Player : IAlive
    {

        public int ImageId { get; set; }
        public int Health { get; set; }
        public Vector2 Pos { get; set; }
        public Vector2 Speed { get; set; }

        public Player(int imageId, int health, Vector2 pos, Vector2 speed)
        {
            ImageId = imageId;
            Health = health;
            Pos = pos;
            Speed = speed;
        }

        public void Update()
        {

        }
    }
}
