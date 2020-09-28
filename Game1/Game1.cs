/* Authors:
 * Hunter Figgs
 * Jared Perkins
 * Jeffrey Gaydos
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Game1.Controller;
using System.Collections.Generic;
using Game1.Player;
using Game1.Projectile;

namespace Game1
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager Graphics { get; private set; }
        private SpriteBatch spriteBatch;

        private List<IController> controllerList;

        public IPlayer Player { get; set; }
        // TEMP TEMP TEMP TEMP 
        private IProjectile projectile;
        // TEMP TEMP TEMP TEMP
        public Game1()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        // Initialization that does not require content
        protected override void Initialize()
        {
            controllerList = new List<IController>
            {
                new KeyboardController(this)
            };

            IsMouseVisible = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TEMP TEMP TEMP TEMP
            PlayerSpriteFactory.Instance.LoadAllTextures(Content);
            ProjectileSpriteFactory.Instance.LoadAllTextures(Content);

            Player = new Player1(this, new Vector2(400, 250), spriteBatch);
            projectile = new BombProjectile(new Point(400, 200));
            // TEMP TEMP TEMP TEMP
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

            // TEMP TEMP TEMP TEMP
            Player.Update(gameTime);
            projectile.Update();
            // TEMP TEMP TEMP TEMP

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            // TEMP TEMP TEMP TEMP
            Player.Draw(Color.White);
            projectile.Draw(spriteBatch);
            // TEMP TEMP TEMP TEMP

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
