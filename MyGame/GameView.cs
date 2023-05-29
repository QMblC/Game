using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.Code;
using System.Collections.Generic;
using System.Linq;

namespace MyGame
{
    public class GameView : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Camera camera;
        private Player player;
        private Map map;
        private List<Sprite> sprites = new();
        private MiniMap miniMap;

        public static List<Texture2D> _textures;

        public static readonly int ScreenWidth = 1280;
        public static readonly int ScreenHeight = 720;

        private LevelId ActiveLevel = LevelId.FirstLevel;

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
                Content.Load<Texture2D>("Screenshot_11"),//0
                Content.Load<Texture2D>("floor_1"),//1
                Content.Load<Texture2D>("Boy"),//2
                Content.Load<Texture2D>("wall"),//3
                Content.Load<Texture2D>("WhiteSquare"),//4
                Content.Load<Texture2D>("stairs"),//5
                Content.Load<Texture2D>("key"),//6
                Content.Load<Texture2D>("tileWithKey"),//7
                Content.Load<Texture2D>("Inventory"),//8
                Content.Load<Texture2D>("blackKey"),//9
                Content.Load<Texture2D>("ghost"),
                Content.Load<Texture2D>("ghostR")
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

            miniMap = new MiniMap(_textures[4], new Vector2(-300, -300));
            map = new Map(Levels.FirstLevel.Cells, miniMap, Levels.FirstLevel.KeyCount, Levels.FirstLevel.SpotCount);

            player = new Player(_textures[2]);
            player.Position = Levels.FirstLevel.StartPos;
            player.Inventory = new Inventory(_textures[8], Levels.FirstLevel.KeyCount);

            camera = new Camera();


            sprites.Add(player);
            map.CreateSpites(_textures);
            sprites.AddRange(map.Sprites);

            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            camera.Follow(player);
            map.MiniMap.Update(player);

            player.Update(sprites, map, gameTime);
            player.Inventory.Update(map.MiniMap);
            foreach (var mob in map.Mobs)
                mob.Update(sprites, map, player);

            if (!player.IsAlive)
            {
                miniMap = new MiniMap(_textures[4], new Vector2(-300, -300));
                map = new Map(Levels.FirstLevel.Cells, miniMap, Levels.FirstLevel.KeyCount, Levels.FirstLevel.SpotCount);
                player = new Player(_textures[2]);
                player.Position = Levels.FirstLevel.StartPos;
                player.Inventory = new Inventory(_textures[8], Levels.FirstLevel.KeyCount);

                sprites = new();
                sprites.Add(player);
                map.CreateSpites(_textures);
                sprites.AddRange(map.Sprites);
            }

            if ((map.IsAbleToLeave(player) && Keyboard.GetState().GetPressedKeys().Contains(Keys.E)))
            {
                if (ActiveLevel == LevelId.FirstLevel)
                {
                    ActiveLevel = LevelId.SecondLevel;
                    miniMap = new MiniMap(_textures[4], new Vector2(-300, -300));
                    map = new Map(Levels.SecondLevel.Cells, miniMap, Levels.SecondLevel.KeyCount, Levels.SecondLevel.SpotCount);
                    player = new Player(_textures[2]);
                    player.Position = Levels.SecondLevel.StartPos;
                    player.Inventory = new Inventory(_textures[8], Levels.SecondLevel.KeyCount);

                    sprites = new();
                    sprites.Add(player);
                    map.CreateSpites(_textures);
                    sprites.AddRange(map.Sprites);
                }
            }



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            _spriteBatch.Begin(SpriteSortMode.FrontToBack, transformMatrix: camera.Transform);


            map.MiniMap.Draw(_spriteBatch, map.Visited, map.Scale);
            map.Update(_spriteBatch, player);

            foreach (var mob in map.Mobs)
            {
                if(Map.GetPlayersVision(player).Intersects(mob.Rectangle))
                    mob.Draw(gameTime, _spriteBatch);  
            }
                

            player.Draw(gameTime, _spriteBatch);
            player.Inventory.Draw(_spriteBatch);

            
            _spriteBatch.End();
            base.Draw(gameTime);
            
        }
    }
}