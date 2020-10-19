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
<<<<<<< HEAD
using System;
=======
using Game1.RoomLoading;
>>>>>>> 1cb9a61... created XML room loader and created first test room

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
        public LinkedList<IProjectile> ProjectileList { get; set; }

        //DELETE ME
        private Room Room1;

        //DELETE ME
        private MovableBlock block;

        public Game1()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            resolution = new ResolutionComponent(this, Graphics, new Point(256, 176), new Point(1024, 768), false, true, false);
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

            //temp
            RoomParser room = new RoomParser(this);

            PlayerSpriteFactory.Instance.LoadAllTextures(Content);
            Player = new Player1(this, new Vector2(40, 100));

            ProjectileSpriteFactory.Instance.LoadAllTextures(Content);

            ItemSpriteFactory.Instance.LoadAllTextures(Content);
            ItemList = room.GetItems();

            EnvironmentSpriteFactory.instance.LoadContent(Content);
            EnvironmentList = room.GetNonInteractableEnvinornment();
            EnvironmentListTop = room.GetInteractableEnvinornment();

            EnemySpriteFactory.Instance.LoadAllTextures(Content);
            EnemyList = room.GetEnemies();

            ParticleSpriteFactory.Instance.LoadAllTextures(Content);

            //DELETE ME
            Room1 = new Room(this);

            //DELETE ME
            block = new MovableBlock(new Vector2(50.0f, 50.0f));
            block.Move(new Vector2(4.0f, 4.0f), 1.0f);
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

            //foreach (IEnemy enemy in EnemyList)
            //{
            //    enemy.Update(gameTime, new Rectangle(32, 32, 224, 144));
            //}

            LinkedList<IProjectile> projectilesToRemove = new LinkedList<IProjectile>();

            foreach (IProjectile projectile in ProjectileList)
            {
                if (projectile.Update(gameTime))
                {
                    projectilesToRemove.AddFirst(projectile);
                }
            }

            foreach (IProjectile projectile in projectilesToRemove)
            {
                ProjectileList.Remove(projectile);
            }

            foreach (IEnvironment internactEnvironment in EnvironmentList)
            {
                internactEnvironment.BehaviorUpdate(gameTime);
            }

<<<<<<< HEAD
            //DELETE ME
            block.BehaviorUpdate(gameTime);
            Console.WriteLine(Player.GetPlayerHitbox().ToString());
=======
            foreach (IItem item in ItemList)
            {
                item.Update(gameTime);
            }

            foreach (IEnvironment nonInternactEnvironment in EnvironmentListTop)
            {
                nonInternactEnvironment.BehaviorUpdate(gameTime);
            }
>>>>>>> 1cb9a61... created XML room loader and created first test room

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, resolution.TransformationMatrix());

            foreach (IEnvironment nonInternactEnvironment in EnvironmentList)
            {
                nonInternactEnvironment.Draw(spriteBatch, Color.White);
            }

            foreach (IEnvironment internactEnvironment in EnvironmentListTop)
            {
                internactEnvironment.Draw(spriteBatch, Color.White);
            }

            foreach (IItem item in ItemList)
            {
                item.Draw(spriteBatch, Color.White);
            }

            foreach (IEnemy enemy in EnemyList)
            {
                enemy.Draw(spriteBatch, Color.White);
            }
            
            Player.Draw(spriteBatch, Color.White);

            foreach (IProjectile projectile in ProjectileList)
            {
                projectile.Draw(spriteBatch, Color.White);
            }

            //DELETE ME
            block.Draw(spriteBatch, Color.White);

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

        public void Reset()
        {
            Player = new Player1(this, new Vector2(40, 100));
            ProjectileList = new LinkedList<IProjectile>();
            ItemList = ItemListFactory.GetItemList();
            EnvironmentList = EnvironmentListFactory.GetEnvironmentList();
            EnemyList = EnemyListFactory.GetEnemyList(this);
        }
    }
}
