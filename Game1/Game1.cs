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

            // TEMP TEMP TEMP TEMP
            PlayerSpriteFactory.Instance.LoadAllTextures(Content);
            EnvironmentSpriteFactory.instance.LoadContent(Content);
            environmentSprite = EnvironmentSpriteFactory.instance.createFloor();

            ItemSpriteFactory.Instance.LoadAllTextures(Content);
            var itemPosition = new Vector2(200, 200);
            itemList = new LinkedList<IItem>();
            itemList.AddLast(new Bomb(itemPosition));
            itemList.AddLast(new Bow(itemPosition));
            itemList.AddLast(new Clock(itemPosition));
            itemList.AddLast(new Compass(itemPosition));
            itemList.AddLast(new Fairy(itemPosition));
            itemList.AddLast(new Heart(itemPosition));
            itemList.AddLast(new HeartContainer(itemPosition));
            itemList.AddLast(new ItemBoomerang(itemPosition));
            itemList.AddLast(new Key(itemPosition));
            itemList.AddLast(new Map(itemPosition));
            itemList.AddLast(new RupeeBlue(itemPosition));
            itemList.AddLast(new RupeeYellow(itemPosition));
            itemList.AddLast(new Triforce(itemPosition));

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

            // TEMP TEMP TEMP TEMP
            Player.Update(gameTime);

            environmentSprite.Update();

            itemList.First.Value.Update(gameTime);
            // TEMP TEMP TEMP TEMP

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null);

            // TEMP TEMP TEMP TEMP
            Player.Draw(Color.White);

            environmentSprite.Draw(spriteBatch, new Vector2(150.0f, 150.0f), Color.White);

            itemList.First.Value.Draw(spriteBatch);
            // TEMP TEMP TEMP TEMP

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
