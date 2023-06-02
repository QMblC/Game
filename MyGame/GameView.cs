using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MyGame.Code;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace MyGame
{
    public class GameCycle : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Camera camera;
        private Player player;
        private Map map;
        private List<Sprite> sprites = new();
        private MiniMap miniMap;
        private GameState gameState;
        

        public static List<Texture2D> textures;
        public static List<Texture2D> animation;
        public static List<Level> levels;
        public static List<SpriteFont> fonts;
        public static Song song;


        public static readonly int ScreenWidth = 1280;
        public static readonly int ScreenHeight = 720;

        private LevelId ActiveLevel = LevelId.Menu;
        private LevelId previousLevel;

        private Button startButton;
        private Button exitButton;

        private Button continueButton;
        private Button backToMenuButton;

        private Button restartButton;

        private TextWindow textWindow;

        public GameCycle()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.IsBorderless = true;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            #region LoadingTextures
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

            #endregion

            #region LoadingLevels
            levels = new()
            {
                new Menu(),
                new FirstLevel(),
                new SecondLevel(),
                new ThirdLevel()
            };
            #endregion

            #region LoadingFonts

            fonts = new()
            {
                Content.Load<SpriteFont>("font"),
                Content.Load<SpriteFont>("font1"),
            };

            #endregion

            base.Initialize();


            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = ScreenWidth;
            _graphics.PreferredBackBufferHeight = ScreenHeight;
            _graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            song = Content.Load<Song>("music");
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.1f;

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            camera = new Camera();

            startButton = new Button(0, -75, "Start");
            exitButton = new Button(0, +75, "Quit");

            continueButton = new Button(0, -75, "Continue");
            backToMenuButton = new Button(0, +75, "Menu");

            restartButton = new Button(0, 0, "Restart");

            textWindow = new TextWindow();
        }

        protected override void Update(GameTime gameTime)
        {

            if (ActiveLevel == LevelId.Menu)
                ReactMenuActions();
            else if (ActiveLevel == LevelId.Pause)

                ReactPauseActions();

            else if (ActiveLevel == LevelId.Death)
            {
                ReactDeathActions();
            }

            else
            {
                if (gameState.State == GameStates.LevelsIsGenerating)
                    return;

                if (gameState.State == GameStates.IsPaused)
                {
                    previousLevel = ActiveLevel;
                    ActiveLevel = LevelId.Pause;
                }

                if (!textWindow.IsRead && ActiveLevel == LevelId.FirstLevel)
                {
                    if (Keyboard.GetState().GetPressedKeyCount() > 0)
                        textWindow.IsRead = true;
                }



                camera.Follow(player);
                map.MiniMap.Update(player);

                player.Update(sprites, map, gameTime);

                foreach (var mob in map.Mobs)
                    mob.Update(sprites, map, player);

                if (gameState.State == GameStates.PlayerIsDead)
                {
                    ActiveLevel = LevelId.Death;

                    return;
                }

                if (gameState.State == GameStates.PlayerIsAbleToFinish)
                {
                    ActiveLevel += 1;
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
            gameState = new GameState(player, map);
            sprites = new(){ player };
            sprites.AddRange(map.CreateSpites(textures));
        }

        private void ReactMenuActions()
        {
            if (startButton.IsClicked)
            {
                ActiveLevel += 1;
                SetLevelSettings();
            }
            if (exitButton.IsClicked)
                Exit();
        }

        private void ReactPauseActions()
        {
            if (continueButton.IsClicked)
                ActiveLevel = previousLevel;
            if (backToMenuButton.IsClicked)
            {
                ActiveLevel = LevelId.Menu;
            }
        }

        private void ReactDeathActions()
        {
            if (restartButton.IsClicked)
            {
                ActiveLevel = LevelId.FirstLevel;
                SetLevelSettings();
            }


        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            if(ActiveLevel == LevelId.Menu)
            {
                _spriteBatch.Begin(SpriteSortMode.FrontToBack);
                _spriteBatch.Draw(textures[12], new Rectangle(0, 0, 1280, 720), Color.White);
                startButton.Draw(_spriteBatch);
                exitButton.Draw(_spriteBatch);
            }

            else if (ActiveLevel == LevelId.Pause)
            {
                _spriteBatch.Begin(SpriteSortMode.FrontToBack);
                _spriteBatch.Draw(textures[12], new Rectangle(0, 0, 1280, 720), Color.White);

                continueButton.Draw(_spriteBatch);
                backToMenuButton.Draw(_spriteBatch);
            }
            else if(ActiveLevel == LevelId.Death)
            {
                _spriteBatch.Begin(SpriteSortMode.FrontToBack);
                _spriteBatch.Draw(textures[12], new Rectangle(0, 0, 1280, 720), Color.White);
                restartButton.Draw(_spriteBatch);
            }
            else
            {
                _spriteBatch.Begin(SpriteSortMode.FrontToBack, transformMatrix: camera.Transform);

                if (gameState.State == GameStates.LevelsIsGenerating)
                    return;

                if(!textWindow.IsRead && ActiveLevel == LevelId.FirstLevel)
                    textWindow.Draw(_spriteBatch);

                

                map.MiniMap.Draw(_spriteBatch, map);
                map.Draw(_spriteBatch, player);

                foreach (var mob in map.Mobs)
                    if (Map.GetPlayersVision(player).Intersects(mob.Vision))
                        mob.Draw(gameTime, _spriteBatch);

                player.Draw(gameTime, _spriteBatch);
                player.Inventory.Draw(_spriteBatch);

                

            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}