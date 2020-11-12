﻿/* Author: Hunter Figgs.3 */

using Game1.Controller;
using Game1.ResolutionManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game1.GameState
{
    class GameStatePaused : IGameState
    {
        private readonly Game1 game;
        private readonly List<IController> controllerList;

        private const float roomOffset = 40f;
        private const float hudOffset = -136f;

        private Color color = Color.White;

        public GameStatePaused(Game1 game)
        {
            this.game = game;

            controllerList = new List<IController>
            {
                new KeyboardPausedController(game),
                new GamepadPausedController(game, PlayerIndex.One)
            };
        }

        public void Update(GameTime gameTime)
        {
            foreach (IController controller in controllerList)
            {
                controller.Update();
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, IResolutionManager resolutionManager)
        {
            game.GraphicsDevice.Clear(Color.Black);

            var drawMatrix = resolutionManager.GetResolutionMatrix();


            drawMatrix.Translation = new Vector3(0, roomOffset * resolutionManager.GetResolutionScale(), 0);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, drawMatrix);

            game.Screen.Draw(spriteBatch, color);

            spriteBatch.End();


            drawMatrix.Translation = new Vector3(0, hudOffset * resolutionManager.GetResolutionScale(), 0);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, drawMatrix);

            game.HUD.Draw(spriteBatch, new Vector2(0, 0), color);

            spriteBatch.End();


            drawMatrix.Translation = new Vector3(0, 0, 0);
        }
    }
}