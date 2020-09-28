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
using Game1.Enemy.SpikeTrap;
using System.Runtime.CompilerServices;

namespace Game1
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager Graphics { get; private set; }
        private SpriteBatch spriteBatch;
        private IEnemy enemy;
        private IEnemy enemy2;
        private IEnemy spikeTrapEnemy;
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
            enemy = new Merchant(new Vector2(250,250), spriteBatch);
            enemy2 = new Skeleton(new Vector2(100, 100), spriteBatch);

            spikeTrapEnemy = new SpikeTrap(this, spriteBatch, new Vector2(100, 250), 100, 100);

            // TEMP TEMP TEMP TEMP
            PlayerSpriteFactory.Instance.LoadAllTextures(Content);

            Player = new Player1(this, new Vector2(0, 0), spriteBatch);
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

            spikeTrapEnemy.Update(gameTime, new Rectangle(0, 0, 800, 400));

            // TEMP TEMP TEMP TEMP
            Player.Update(gameTime);
            // TEMP TEMP TEMP TEMP

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null);
            enemy.Draw();
            enemy2.Draw();

            spikeTrapEnemy.Draw();

            // TEMP TEMP TEMP TEMP
            Player.Draw(Color.White);
            // TEMP TEMP TEMP TEMP


            Texture2D _texture;
            _texture = new Texture2D(GraphicsDevice, 1, 1);
            _texture.SetData(new Color[] { Color.White });
            spriteBatch.Draw(_texture, GetPlayerRectangle(), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public Rectangle GetPlayerRectangle()
        {
            return Player.GetLocation();
        }

        public Vector2 GetWindowDimensions()
        {
            return new Vector2(Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight);
        }
    }
}
