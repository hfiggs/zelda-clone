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
using Game1.Sprite;
using Game1.Environment;
using Game1.Item;

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

            Player.Update(gameTime);
            projectile.Update(gameTime);
            // TEMP TEMP TEMP TEMP

            environmentSprite.Update();

            itemList.First.Value.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null);

            Player.Draw(Color.White);
            projectile.Draw(spriteBatch);
            // TEMP TEMP TEMP TEMP

            environmentSprite.Draw(spriteBatch, new Vector2(150.0f, 150.0f), Color.White);

            itemList.First.Value.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
