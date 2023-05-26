using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Code
{
    public interface IGameObject
    {
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        public void Update(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
