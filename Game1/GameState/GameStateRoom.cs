﻿using Game1.Controller;
using Game1.Environment;
using Game1.ResolutionManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace Game1.GameState
{
    class GameStateRoom : IGameState
    {
        private readonly Game1 game;
        private readonly List<IController> controllerList;

        private const float roomOffset = 40f;
        private const float hudOffset = -136f;

        public GameStateRoom(Game1 game)
        {
            this.game = game;

            controllerList = new List<IController>
            {
                new KeyboardController(game),
                new GamepadController(game, PlayerIndex.One)
            };
        }

        public void Update(GameTime gameTime)
        {
            foreach (IController controller in controllerList)
            {
                controller.Update();
            }

            game.HUD.Update(gameTime);
            Mouse.SetPosition(150, 150);

            game.Screen.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, IResolutionManager resolutionManager)
        {
            game.GraphicsDevice.Clear(Color.Black);

            var drawMatrix = resolutionManager.GetResolutionMatrix();


            drawMatrix.Translation = new Vector3(0, roomOffset * resolutionManager.GetResolutionScale(), 0);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, drawMatrix);

            game.Screen.Draw(spriteBatch);

            foreach(IEnvironment env in game.Screen.CurrentRoom.InteractEnviornment)
            {
                if(env.GetType() == typeof(LoadZone))
                {
                    DebugUtil.showHitbox(((LoadZone)env).GetHitboxes().First(), spriteBatch, game);
                }
            }

            spriteBatch.End();


            drawMatrix.Translation = new Vector3(0, hudOffset * resolutionManager.GetResolutionScale(), 0);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, drawMatrix);

            game.HUD.Draw(spriteBatch, new Vector2(0, 0), Color.White);

            spriteBatch.End();


            drawMatrix.Translation = new Vector3(0, 0, 0);
        }
    }
}
