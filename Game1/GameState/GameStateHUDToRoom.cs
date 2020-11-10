/* Author: Hunter Figgs.3 */

using Game1.Controller;
using Game1.ResolutionManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game1.GameState
{
    class GameStateHUDToRoom : IGameState
    {
        private readonly Game1 game;
        private readonly List<IController> controllerList;

        private const float transitionSpeed = 0.1f; // pixels per ms

        private const float roomStartOffset = 216f;
        private const float hudStartOffset = 40f;

        private const float roomEndOffset = 40f;
        private const float hudEndOffset = -136f;

        private float roomOffset;
        private float hudOffset;

        public GameStateHUDToRoom(Game1 game)
        {
            this.game = game;

            controllerList = new List<IController>
            {
                new KeyboardTransitionController(game),
                new GamepadTransitionController(game, PlayerIndex.One)
            };

            roomOffset = roomStartOffset;
            hudOffset = hudStartOffset;
        }

        public void Update(GameTime gameTime)
        {
            foreach (IController controller in controllerList)
            {
                controller.Update();
            }

            Mouse.SetPosition(150, 150);

            roomOffset = Math.Max(roomEndOffset, roomOffset - transitionSpeed * (float)gameTime.ElapsedGameTime.TotalMilliseconds);
            hudOffset = Math.Max(hudEndOffset, hudOffset - transitionSpeed * (float)gameTime.ElapsedGameTime.TotalMilliseconds);

            if (roomOffset <= roomEndOffset && hudOffset <= hudEndOffset)
            {
                game.SetState(new GameStateRoom(game));
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, IResolutionManager resolutionManager)
        {
            game.GraphicsDevice.Clear(Color.Black);

            var drawMatrix = resolutionManager.GetResolutionMatrix();


            drawMatrix.Translation = new Vector3(0, roomOffset * resolutionManager.GetResolutionScale(), 0);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, drawMatrix);

            game.Screen.Draw(spriteBatch);

            spriteBatch.End();


            drawMatrix.Translation = new Vector3(0, hudOffset * resolutionManager.GetResolutionScale(), 0);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, drawMatrix);

            game.HUD.Draw(spriteBatch, new Vector2(0, 0), Color.White);

            spriteBatch.End();


            drawMatrix.Translation = new Vector3(0, 0, 0);
        }
    }
}
