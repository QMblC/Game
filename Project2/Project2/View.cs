using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Test
{
    public class GameCycleView : Game, IGameplayView
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Dictionary<int, IAlive> _objects = new Dictionary<int, IAlive>();
        private Dictionary<int, Texture2D> _textures = new Dictionary<int, Texture2D>();

        public event EventHandler CycleFinished = delegate { };
        public event EventHandler<ControlsEventArgs> PlayerMoved = delegate { };

        private readonly int WindowWidth = 1920;
        private readonly int WindowHeight = 1080;

        public GameCycleView()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.IsFullScreen = true;
            _graphics.PreferredBackBufferWidth = WindowWidth;
            _graphics.PreferredBackBufferHeight = WindowHeight;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _textures.Add(1, Content.Load<Texture2D>("rect"));
        }

        protected override void Update(GameTime gameTime)
        {
            var keys = Keyboard.GetState().GetPressedKeys();
            for (var i = 0; i < keys.Length; i++)
                ReactPressedKey(keys[i]);



            base.Update(gameTime);
            CycleFinished.Invoke(this, new EventArgs());

        }

        private void ReactPressedKey(Keys key)
        {
            var _playerPos = _objects[1].Pos;
            var _playerImage = _textures[1];
            if (key == Keys.Escape)
                Exit();
            if (key == Keys.W)
                if (_playerPos.Y > 0)
                    PlayerMoved.Invoke(this, new ControlsEventArgs { Direction = IGameplayModel.Direction.Forward });
            if (key == Keys.D)
                if (_playerPos.X < WindowWidth - _playerImage.Width)
                    PlayerMoved.Invoke(this, new ControlsEventArgs { Direction = IGameplayModel.Direction.Right });
            if (key == Keys.S)
                if (_playerPos.Y < WindowHeight - _playerImage.Height)
                    PlayerMoved.Invoke(this, new ControlsEventArgs { Direction = IGameplayModel.Direction.Backward });
            if (key == Keys.A)
                if (_playerPos.X > 0)
                    PlayerMoved.Invoke(this, new ControlsEventArgs { Direction = IGameplayModel.Direction.Left });
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            // TODO: Add your drawing code here
            base.Draw(gameTime);
            _spriteBatch.Begin();
            foreach (var o in _objects.Values)
                _spriteBatch.Draw(_textures[o.ImageId], o.Pos, Color.Black);
            _spriteBatch.End();
        }

        public void LoadGameCycleParameters(Dictionary<int, IAlive> Objects)
        {
            _objects = Objects;
        }
    }
}