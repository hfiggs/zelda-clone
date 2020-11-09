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

namespace Game1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private IGameState state;

        private readonly IResolutionManager resolutionManager;
        private Point baseResolution = new Point(256, 216);
        private const int scale = 4;

        public Screen Screen { get; private set; }
        public HUDInterface HUD { get; private set; }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            state = new GameStateRoom(this);

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

            ContentUtil.LoadAllContent(Content);

            //Move this to game state maybe?
            //AudioManager.PlayLooped("dungeon");

            Screen = new Screen(this);
            Screen.LoadAllRooms();

            HUD = new HUDInterface(Screen.Player.PlayerInventory, Screen);
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            state.Update(gameTime);

            AudioManager.Update(gameTime);
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            state.Draw(gameTime, spriteBatch, resolutionManager);
            
            base.Draw(gameTime);
        }

        public void SetState(IGameState state)
        {
            this.state = state;
        }

        public Vector2 GetWindowDimensions()
        {
            return new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
        }
    }
}
