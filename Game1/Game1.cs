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
using Game1.Projectile;
using Game1.Enemy.SpikeTrap;
using System.Runtime.CompilerServices;
using Game1.Enemy.OldMan;
using Game1.Environment;
using Game1.Item;
using Game1.Enemy.Snake;

namespace Game1
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager Graphics { get; private set; }
        private SpriteBatch spriteBatch;
        private IEnemy skeleton;
        private IEnemy oldMan;
        private IEnemy merchant;
        private IEnemy spikeTrap;
        private IEnemy snake;
        private List<IController> controllerList;

        public IPlayer Player { get; set; }
        // TEMP TEMP TEMP TEMP 
        private IProjectile projectile;
        // TEMP TEMP TEMP TEMP
        
        public ISprite environmentSprite;

        public LinkedList<IItem> itemList;

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

            skeleton = new Goriya(this, spriteBatch, new Vector2(250, 250));
            oldMan = new OldMan(spriteBatch, new Vector2(100, 100));
            merchant = new Merchant(new Vector2(250,250), spriteBatch);
            spikeTrap = new SpikeTrap(this, spriteBatch, new Vector2(100, 250), 100, 100);
            snake = new Snake(this, spriteBatch, new Vector2(300, 250));

            PlayerSpriteFactory.Instance.LoadAllTextures(Content);
            Player = new Player1(this, new Vector2(400, 250), spriteBatch);

            ProjectileSpriteFactory.Instance.LoadAllTextures(Content);
            projectile = new Boomerang('E', Player);

            EnvironmentSpriteFactory.instance.LoadContent(Content);
            environmentSprite = EnvironmentSpriteFactory.instance.createFloor();

            ItemSpriteFactory.Instance.LoadAllTextures(Content);
            itemList = ItemListFactory.GetItemList();
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

            skeleton.Update(gameTime, new Rectangle(0, 0, 400, 400));
            oldMan.Update(gameTime, new Rectangle(0, 0, 400, 400));
            merchant.Update(gameTime, new Rectangle(0, 0, 400, 400));
            spikeTrap.Update(gameTime, new Rectangle(0, 0, 800, 400));
            snake.Update(gameTime, new Rectangle(0, 0, 800, 400));

            // TEMP TEMP TEMP TEMP
            Player.Update(gameTime);
            projectile.Update(gameTime);
            // TEMP TEMP TEMP TEMP

            environmentSprite.Update();

            itemList.First.Value.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null);

            skeleton.Draw();
            oldMan.Draw();
            merchant.Draw();
            spikeTrap.Draw();
            snake.Draw();

            Player.Draw(Color.White);
            projectile.Draw(spriteBatch);
            // TEMP TEMP TEMP TEMP

            environmentSprite.Draw(spriteBatch, new Vector2(150.0f, 150.0f), Color.White);

            itemList.First.Value.Draw(spriteBatch);


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
