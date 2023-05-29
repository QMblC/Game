using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Code
{
    public class Animation
    {
        public Texture2D Texture;
        public int CurrentFrame;
        public int FrameCount;
        public int FrameWidth => Texture.Width / FrameCount;
        public int FrameHeight => Texture.Height;
        public float FrameSpeed => 0.2f;

        public Animation(Texture2D texture, int frameCount)
        {
            Texture = texture;
            FrameCount = frameCount;
        }
    }

    public enum Animations
    {
        WalkingUp,
        WalkingRight,
        WalkingDown,
        WalkingLeft
    }
}
