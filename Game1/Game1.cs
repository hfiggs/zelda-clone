/* Authors:
 * Hunter Figgs
 * Jared Perkins
 * Jeffrey Gaydos
 * Sergei Fedulov
 * Patrick Haughn
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Game1.Controller;
using Game1.Sprite;
using Game1.Enemy;
using System.Collections.Generic;
using Game1.Player;
using Game1.Projectile;
using Game1.Environment;
using ResolutionBuddy; // Nuget package found here: https://www.nuget.org/packages/ResolutionBuddy/2.0.4
using Game1.RoomLoading;

namespace Game1
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager Graphics { get; private set; }
        private SpriteBatch spriteBatch;

        IResolution resolution;

        private List<IController> controllerList;
        public Screen Screen { get; set; }

        public Game1()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            resolution = new ResolutionComponent(this, Graphics, new Point(256, 176), new Point(1024, 768), false, true, false);
        }

        // Initialization that does not require content
        protected override void Initialize()
        {
            controllerList = new List<IController>
            {
                new KeyboardController(this),
                new GamepadController(this, PlayerIndex.One)
            };

            IsMouseVisible = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            PlayerSpriteFactory.Instance.LoadAllTextures(Content);

            ProjectileSpriteFactory.Instance.LoadAllTextures(Content);

            ItemSpriteFactory.Instance.LoadAllTextures(Content);

            EnvironmentSpriteFactory.instance.LoadContent(Content);

            EnemySpriteFactory.Instance.LoadAllTextures(Content);

            ParticleSpriteFactory.Instance.LoadAllTextures(Content);

            Screen = new Screen(this, 'F', 2);

            //DELETE
            AudioManager.LoadContent(Content);
            AudioManager.PlayMusic("dungeon");
            //AudioManager.PlaySound("death");

            Screen.LoadAllRooms();
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            foreach(IController controller in controllerList)
            {
               controller.Update();
            }

            Screen.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, null, null, null, resolution.TransformationMatrix());

            Screen.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public Vector2 GetWindowDimensions()
        {
            return new Vector2(Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight);
        }

    }
}
