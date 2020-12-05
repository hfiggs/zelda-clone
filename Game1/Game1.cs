/* Authors:
 * Hunter Figgs
 * Jared Perkins
 * Jeffrey Gaydos
 * Sergei Fedulov
 * Patrick Haughn
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Game1.RoomLoading;
using Game1.ResolutionManager;
using Game1.GameState;
using Game1.HUD;
using Game1.Util;
using Game1.Audio;

namespace Game1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public IGameState State { get; private set; }
        public int Mode { get; set; }

        public IResolutionManager ResolutionManager { get; private set; }
        private Point virtualResolution = new Point(256, 216);
        private Point targetResolution = new Point(1024, 864);

        public Screen Screen { get; set; }
        public HUDInterface HUD { get; set; }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            ResolutionManager = new ResolutionManager1(this, graphics, virtualResolution, targetResolution);
        }

        // Initialization that does not require content
        protected override void Initialize()
        {
            IsMouseVisible = false;

            AudioManager.InitializeAudioManager(this);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ContentUtil.LoadAllContent(Content, GraphicsDevice);

            Screen = new Screen(this);

            RoomUtil.constructRoomUtil(Screen);
            Screen.LoadAllRooms(0);


            State = new GameStateStart(this);

        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            State.Update(gameTime);

            AudioManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            State.Draw(gameTime, spriteBatch, ResolutionManager);

            base.Draw(gameTime);
        }

        public void SetState(IGameState state)
        {
            this.State = state;
        }

        public Vector2 GetWindowDimensions()
        {
            return new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
        }

        public Vector2 GetWindowDimensionsScaled()
        {
            return new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight) / ResolutionManager.GetResolutionScale();
        }

        public void SetMode(int gameMode)
        {
            Mode = gameMode;
            Screen.HandleGameMode();
            RoomUtil.constructRoomUtil(Screen);
        }
    }
}
