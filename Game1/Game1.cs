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
using Game1.Particle;

namespace Game1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public IGameState State { get; private set; }

        private readonly IResolutionManager resolutionManager;
        private Point baseResolution = new Point(256, 216);
        private const int scale = 4;

        public Screen Screen { get; set; }
        public HUDInterface HUD { get; set; }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            resolutionManager = new ResolutionManager1(this, graphics, baseResolution, scale);
        }

        // Initialization that does not require content
        protected override void Initialize()
        {
            IsMouseVisible = false;
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ContentUtil.LoadAllContent(Content, GraphicsDevice);

            Screen = new Screen(this);
            Screen.LoadAllRooms();

            HUD = new HUDInterface(Screen.Player.PlayerInventory, Screen);

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
            State.Draw(gameTime, spriteBatch, resolutionManager);

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
            return new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight) / scale;
        }
    }
}
