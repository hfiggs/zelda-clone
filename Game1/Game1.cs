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
        public Screen Screen { get; set; }

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

            IsMouseVisible = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            PlayerSpriteFactory.Instance.LoadAllTextures(Content);

            ProjectileSpriteFactory.Instance.LoadAllTextures(Content);

            ItemSpriteFactory.Instance.LoadAllTextures(Content);

            EnvironmentSpriteFactory.instance.LoadContent(Content);

            EnemySpriteFactory.Instance.LoadAllTextures(Content);

            ParticleSpriteFactory.Instance.LoadAllTextures(Content);

            Screen = new Screen(this, 'F', 2);
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

<<<<<<< HEAD
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

            foreach (IEnvironment interactEnvironment in EnvironmentListTop)
            {
                interactEnvironment.BehaviorUpdate(gameTime);
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

            foreach (IEnvironment nonInternactEnvironment in EnvironmentList)
            {
                nonInternactEnvironment.BehaviorUpdate(gameTime);
            }
>>>>>>> 1cb9a61... created XML room loader and created first test room
=======
            Screen.Update(gameTime);
>>>>>>> 056f5cd... added screen/room class

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, resolution.TransformationMatrix());

            Screen.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public Vector2 GetWindowDimensions()
        {
            return new Vector2(Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight);
        }

    }
}
