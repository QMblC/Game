using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.Code;
using System.Collections.Generic;

namespace MyGame
{
    public class GameView : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Camera _camera;
        private Player _player;
        private Map _map;
        private List<Sprite> _sprites = new();
        private MiniMap _miniMap;

        public static List<Texture2D> _textures;

        public static readonly int ScreenWidth = 1280;
        public static readonly int ScreenHeight = 720;

        public GameView()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.IsBorderless = true;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

            _textures = new List<Texture2D>
            {
                Content.Load<Texture2D>("Screenshot_11"),
                Content.Load<Texture2D>("floor_1"),
                Content.Load<Texture2D>("Boy"),
                Content.Load<Texture2D>("wall"),
                Content.Load<Texture2D>("WhiteSquare")
            };

            base.Initialize();


            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = ScreenWidth;
            _graphics.PreferredBackBufferHeight = ScreenHeight;
            _graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _miniMap = new MiniMap(_textures[4], new Vector2(-300,-300));
            _camera = new Camera();

            _player = new Player(_textures[2]);
            

            _map = new Map();
            _map.MiniMap = _miniMap;

            _sprites.Add(_player);
            _map.CreateSpites(_textures);

            _sprites.AddRange(_map.Sprites);

            _player.Position = _map.StartPos;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            _camera.Follow(_player);
            _map.MiniMap.Update(_player);
            _player.Update(_sprites);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            _spriteBatch.Begin(SpriteSortMode.FrontToBack, transformMatrix: _camera.Transform);


            _map.MiniMap.Draw(gameTime, _spriteBatch);
            _map.Convert(gameTime, _spriteBatch, _textures, _player);
            _player.Draw(gameTime, _spriteBatch);
            

            
            _spriteBatch.End();
            base.Draw(gameTime);
            
        }
    }
}