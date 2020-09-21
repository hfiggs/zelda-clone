/* Authors:
 * Hunter Figgs
 * Jared Perkins
 * Jeffrey Gaydos
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Game1.Controller;
using Game1.Sprite;
using System.Collections.Generic;
using Game1.Player;
using System.Security.Cryptography.X509Certificates;

namespace Game1
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager Graphics { get; private set; }
        private SpriteBatch spriteBatch;

        private List<IController> controllerList;

        public ISprite Sprite { get; set; }

        public IPlayer Player { get; private set; }

        public Game1()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        // Initialization that does not require content
        protected override void Initialize()
        {
            controllerList = new List<IController>();
            controllerList.Add(new KeyboardController(this));
           // controllerList.Add(new MouseController(this));

            IsMouseVisible = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //TEMP TEMP TEMP TEMP
            PlayerSpriteFactory.Instance.LoadAllTextures(Content);
            Sprite = PlayerSpriteFactory.Instance.CreateAttackLeftSprite(false);
            //TEMP TEMP TEMP TEMP

            //TEMP TEMP TEMP TEMP
            Player = new Player1(new Vector2(400, 250), spriteBatch);
            //TEMP TEMP TEMP TEMP
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

            //Sprite.Update();

            //TEMP TEMP TEMP TEMP
            int x = (int)gameTime.TotalGameTime.TotalMilliseconds % 250;
            if (x == 0)
            {
                Sprite.Update();
            }
            //TEMP TEMP TEMP TEMP

            //TEMP TEMP TEMP TEMP
            //Player.MoveRight();
            Player.Update(gameTime);
            //TEMP TEMP TEMP TEMP

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            Sprite.Draw(spriteBatch,new Vector2(250,250));

            Player.Draw();

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
