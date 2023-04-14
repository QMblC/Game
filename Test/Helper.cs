using System;
using Microsoft.Xna.Framework;

namespace Test
{
    public interface IGameplayModel
    {
        event EventHandler<GameplayEventArgs> Updated;

        void Update();
        void MovePlayer(Direction direction);

        enum Direction
        {
            Forward,
            Right,
            Backward,
            Left
        }
        
        enum MovementState
        {
            Staying,
            Walking,
            Running
        }
    }

    public interface IGameplayView
    {
        event EventHandler CycleFinished;
        event EventHandler<ControlsEventArgs> PlayerMoved;

        void LoadGameCycleParameters(Vector2 pos);
        void Run();
    }

    public class ControlsEventArgs : EventArgs
    {
        public IGameplayModel.Direction Direction { get; set; }
    }

    public class GameplayEventArgs : EventArgs
    {
        public Vector2 PlayerPos { get; set; }
    }
}
