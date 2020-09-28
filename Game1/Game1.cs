/* Authors:
 * Hunter Figgs
 * Jared Perkins
 * Jeffrey Gaydos
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Game1.Controller;
using Game1.Sprite;
using Game1.Enemy;
using System.Collections.Generic;
using Game1.Player;

namespace Game1
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager Graphics { get; private set; }
        private SpriteBatch spriteBatch;
        private IEnemy enemy;
        private IEnemy enemy2;
        private List<IController> controllerList;

        public IPlayer Player { get; set; }

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
            EnemySpriteFactory.Instance.LoadAllTextures(Content);
            enemy = new Skeleton(new Vector2(250,250), spriteBatch);
            enemy2 = new Skeleton(new Vector2(100, 100), spriteBatch);

            // TEMP TEMP TEMP TEMP
            PlayerSpriteFactory.Instance.LoadAllTextures(Content);

            Player = new Player1(this, new Vector2(400, 250), spriteBatch);
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
            enemy.Update(gameTime, new Rectangle(0, 0, 800, 400));
            enemy2.Update(gameTime, new Rectangle(0, 0, 400, 400));

            // TEMP TEMP TEMP TEMP
            Player.Update(gameTime);
            // TEMP TEMP TEMP TEMP

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            enemy.Draw();
            enemy2.Draw();

            // TEMP TEMP TEMP TEMP
            Player.Draw(Color.White);
            // TEMP TEMP TEMP TEMP

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
