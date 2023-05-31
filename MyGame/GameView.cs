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

        public static List<Texture2D> textures;
        public static List<Texture2D> animation;
        public static List<Level> levels;

        public static readonly int ScreenWidth = 1280;
        public static readonly int ScreenHeight = 720;

        private LevelId ActiveLevel = LevelId.Menu;

        public GameView()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.IsBorderless = true;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

            textures = new List<Texture2D>
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
                Content.Load<Texture2D>("ghostR"),
                Content.Load<Texture2D>("dungeon")

            };
            animation = new()
            {
                Content.Load<Texture2D>("Man1"),
                Content.Load<Texture2D>("Man2"),
                Content.Load<Texture2D>("Man3"),
                Content.Load<Texture2D>("Man4"),
                Content.Load<Texture2D>("Man5"),
                Content.Load<Texture2D>("Man6"),
                Content.Load<Texture2D>("Man7"),
                Content.Load<Texture2D>("Man8"),
                Content.Load<Texture2D>("Man9")

            };
            levels = new()
            {
                new Menu(),
                new FirstLevel(),
                new SecondLevel(),
                new ThirdLevel()
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

            camera = new Camera();

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (ActiveLevel == LevelId.Menu)
            {
                if (Keyboard.GetState().GetPressedKeys().Length > 0)
                {
                    ActiveLevel = LevelId.FirstLevel;
                    SetLevelSettings();
                }
            }
            else
            {
                if (!map.IsLevelStarted && map.Keys.Count != map.KeyCount)
                    return;

                camera.Follow(player);
                map.MiniMap.Update(player);

                player.Update(sprites, map, gameTime);

                player.Inventory.Update(map.MiniMap);
                foreach (var mob in map.Mobs)
                    mob.Update(sprites, map, player);

                if (!player.IsAlive)
                {
                    ActiveLevel = LevelId.FirstLevel;
                    SetLevelSettings();
                }

                if ((map.IsAbleToLeave(player) && Keyboard.GetState().GetPressedKeys().Contains(Keys.E)))
                {
                    if (ActiveLevel == LevelId.FirstLevel)
                        ActiveLevel = LevelId.SecondLevel;
                    else if (ActiveLevel == LevelId.SecondLevel)
                        ActiveLevel = LevelId.ThirdLevel;
                    SetLevelSettings();
                }
            }

            base.Update(gameTime);
        }

        private void SetLevelSettings()
        {
            miniMap = new MiniMap(textures[4], new Vector2(-300, -300));
            map = new Map(miniMap, levels[(int) ActiveLevel]);
            player = new Player(animation[0],
                new Inventory(textures[8], levels[(int)ActiveLevel]),
                levels[(int)ActiveLevel].StartPos);

            sprites = new(){ player };
            sprites.AddRange(map.CreateSpites(textures));
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            if(ActiveLevel == LevelId.Menu)
            {
                _spriteBatch.Begin(SpriteSortMode.FrontToBack);
                _spriteBatch.Draw(textures[12], new Rectangle(0,0, 1280, 720), Color.White);
            }
            else
            {
                _spriteBatch.Begin(SpriteSortMode.FrontToBack, transformMatrix: camera.Transform);

                if (!map.IsLevelStarted && map.Keys.Count != map.KeyCount)
                    return;

                map.MiniMap.Draw(_spriteBatch, map);
                map.Update(_spriteBatch, player);

                foreach (var mob in map.Mobs)
                {
                    if (Map.GetPlayersVision(player).Intersects(mob.Vision))
                        mob.Draw(gameTime, _spriteBatch);
                }

                player.Draw(gameTime, _spriteBatch);
                player.Inventory.Draw(_spriteBatch);
     
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}