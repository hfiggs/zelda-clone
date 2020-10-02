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
        public LinkedList<IItem> ItemList { get; set; }
        public LinkedList<IEnvironment> EnvironmentList { get; set; }
        public LinkedList<IEnemy> EnemyList { get; set; }
        private LinkedList<IProjectile> ProjectileList { get; set; }

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

            ProjectileList = new LinkedList<IProjectile>();

            IsMouseVisible = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            PlayerSpriteFactory.Instance.LoadAllTextures(Content);
            Player = new Player1(this, new Vector2(75, 325));

            ProjectileSpriteFactory.Instance.LoadAllTextures(Content);

            ItemSpriteFactory.Instance.LoadAllTextures(Content);
            ItemList = ItemListFactory.GetItemList();

            EnvironmentSpriteFactory.instance.LoadContent(Content);
            EnvironmentList = EnvironmentListFactory.GetEnvironmentList();

            EnemySpriteFactory.Instance.LoadAllTextures(Content);
            EnemyList = EnemyListFactory.GetEnemyList(this, spriteBatch);
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
       
            ItemList.First.Value.Update(gameTime);
            EnemyList.First.Value.Update(gameTime, new Rectangle(0, 0, 800, 400));

            foreach(IProjectile projectile in ProjectileList)
            {
                projectile.Update(gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null);

            Player.Draw(spriteBatch,Color.White);

            ItemList.First.Value.Draw(spriteBatch, Color.White);
            EnvironmentList.First.Value.Draw(spriteBatch, Color.White);
            EnemyList.First.Value.Draw(spriteBatch, Color.White);

            Texture2D _texture;
            _texture = new Texture2D(GraphicsDevice, 1, 1);
            _texture.SetData(new Color[] { Color.White });
            //spriteBatch.Draw(_texture, GetPlayerRectangle(), Color.White);

            foreach(IProjectile projectile in ProjectileList)
            {
                projectile.Draw(spriteBatch, Color.White);
            }

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

        public void SpawnProjectile(IProjectile projectile)
        {
            ProjectileList.AddLast(projectile);
        }
    }
}
