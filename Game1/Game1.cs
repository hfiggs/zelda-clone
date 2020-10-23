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
using Game1.Item;
using ResolutionBuddy; // Nuget package found here: https://www.nuget.org/packages/ResolutionBuddy/2.0.4
using Game1.CollisionDetection;

namespace Game1
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager Graphics { get; private set; }
        private SpriteBatch spriteBatch;

        IResolution resolution;

        private List<IController> controllerList;
        
        public IPlayer Player { get; set; }
        public LinkedList<IItem> ItemList { get; set; }
        public LinkedList<IEnvironment> EnvironmentList { get; set; }
        public LinkedList<IEnvironment> EnvironmentListTop { get; set; }
        public LinkedList<IEnemy> EnemyList { get; set; }
        public List<IProjectile> ProjectileList { get; set; }

        //DELETE ME
        private Room Room1;

        public Game1()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            resolution = new ResolutionComponent(this, Graphics, new Point(256, 176), new Point(1024, 704), false, true, false);
        }

        // Initialization that does not require content
        protected override void Initialize()
        {
            controllerList = new List<IController>
            {
                new KeyboardController(this)
            };

            ProjectileList = new List<IProjectile>();

            IsMouseVisible = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            PlayerSpriteFactory.Instance.LoadAllTextures(Content);
            Player = new Player1(this, new Vector2(40, 100));

            ProjectileSpriteFactory.Instance.LoadAllTextures(Content);

            ItemSpriteFactory.Instance.LoadAllTextures(Content);
            ItemList = ItemListFactory.GetItemList();

            EnvironmentSpriteFactory.instance.LoadContent(Content);
            EnvironmentList = EnvironmentListFactory.GetEnvironmentList();
            EnvironmentListTop = EnvironmentListTopFactory.GetEnvironmentList();

            EnemySpriteFactory.Instance.LoadAllTextures(Content);
            EnemyList = EnemyListFactory.GetEnemyList(this);

            ParticleSpriteFactory.Instance.LoadAllTextures(Content);

            //DELETE ME
            Room1 = new Room(this);
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
            EnemyList.First.Value.Update(gameTime, new Rectangle(0, 0, 256, 176));
            if(EnemyList.First.Value.shouldRemove())
                EnemyList.RemoveFirst();
            EnvironmentList.First.Value.BehaviorUpdate();
            
            foreach (IProjectile projectile in ProjectileList)
            {
                projectile.Update(gameTime);
            }

            ProjectileList.RemoveAll(p => p.ShouldDelete());
            
            //DELETE ME
            Room1.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, resolution.TransformationMatrix());

            ItemList.First.Value.Draw(spriteBatch, Color.White);
            EnvironmentList.First.Value.Draw(spriteBatch, Color.White);
            EnemyList.First.Value.Draw(spriteBatch, Color.White);

            Player.Draw(spriteBatch, Color.White);

            EnvironmentListTop.First.Value.Draw(spriteBatch, Color.White);

            foreach (IProjectile projectile in ProjectileList)
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
            ProjectileList.Add(projectile);
        }

        public void Reset()
        {
            Player = new Player1(this, new Vector2(40, 100));
            ProjectileList = new List<IProjectile>();
            ItemList = ItemListFactory.GetItemList();
            EnvironmentList = EnvironmentListFactory.GetEnvironmentList();
            EnemyList = EnemyListFactory.GetEnemyList(this);
        }
    }
}
