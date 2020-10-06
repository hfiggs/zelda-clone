﻿/* Authors:
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
using Game1.Particle;
using ResolutionBuddy;

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
        public LinkedList<IEnemy> EnemyList { get; set; }
        public LinkedList<IProjectile> ProjectileList { get; set; }

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

            ProjectileList = new LinkedList<IProjectile>();

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

            EnemySpriteFactory.Instance.LoadAllTextures(Content);
            EnemyList = EnemyListFactory.GetEnemyList(this, spriteBatch);

            ParticleSpriteFactory.Instance.LoadAllTextures(Content);
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
            EnvironmentList.First.Value.BehaviorUpdate(gameTime);

            LinkedList<IProjectile> projectilesToRemove = new LinkedList<IProjectile>();
            foreach(IProjectile projectile in ProjectileList)
            {
                if(projectile.Update(gameTime))
                {
                    projectilesToRemove.AddFirst(projectile);
                }
            }

            foreach(IProjectile projectile in projectilesToRemove)
            {
                ProjectileList.Remove(projectile);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, resolution.TransformationMatrix());

            Player.Draw(spriteBatch,Color.White);

            ItemList.First.Value.Draw(spriteBatch, Color.White);
            EnvironmentList.First.Value.Draw(spriteBatch, Color.White);
            EnemyList.First.Value.Draw(spriteBatch, Color.White);

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

        public bool ProjectileContainedInList(IProjectile proj)
        {
            return ProjectileList.Contains(proj);
        }

        public void Reset()
        {
            Player = new Player1(this, new Vector2(75, 325));
            ProjectileList = new LinkedList<IProjectile>();
            ItemList = ItemListFactory.GetItemList();
            EnvironmentList = EnvironmentListFactory.GetEnvironmentList();
            EnemyList = EnemyListFactory.GetEnemyList(this, spriteBatch);
        }
    }
}
