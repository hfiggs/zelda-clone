//Authors: Jared Perkins

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
        private ISprite creditsSprite;

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
            controllerList.Add(new MouseController(this));
            

            IsMouseVisible = true;

            base.Initialize();
        }

        //TEMP TEMP TEMP TEMP
        PlayerSpriteFactory playerFactory = PlayerSpriteFactory.Instance;
        //TEMP TEMP TEMP TEMP
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            var spriteTexture = Content.Load<Texture2D>("Images/guyRun");

            //TEMP TEMP TEMP TEMP
            playerFactory.LoadAllTextures(Content);
            Sprite = playerFactory.CreateTwoHandItemSprite(new Vector2(100, 100));
            //TEMP TEMP TEMP TEMP

            var creditsFont = Content.Load<SpriteFont>("Credits");
            creditsSprite = new TextSprite(creditsFont, new Vector2(220, 400), "Credits\nProgram Made By: Hunter Figgs.3\nSprites from: Created them myself on PiskelApp.com");
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            foreach(IController controller in controllerList)
            {
               // controller.Update();
            }

            //Sprite.Update();

            //TEMP TEMP TEMP TEMP
            int x = (int)gameTime.TotalGameTime.TotalMilliseconds % 250;
            if (x == 0)
            {
                Sprite.Update();
            }

            x++;
            //TEMP TEMP TEMP TEMP
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            Sprite.Draw(spriteBatch);

            creditsSprite.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
