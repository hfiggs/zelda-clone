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
    class GameStateRoomToHUD : IGameState
    {
        private readonly Game1 game;
        private readonly List<IController> controllerList;

        private const float transitionSpeed = 0.1f; // pixels per ms

        private const float roomStartOffset = 40f;
        private const float hudStartOffset = -136f;

        private const float roomEndOffset = 216f;
        private const float hudEndOffset = 40f;

        private Color color = Color.White;

        private float roomOffset;
        private float hudOffset;

        public GameStateRoomToHUD(Game1 game)
        {
            this.game = game;

            controllerList = new List<IController>
            {
                new KeyboardQuitController(game),
                new GamepadQuitController(game, PlayerIndex.One)
            };
            game.IsMouseVisible = true;
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

            roomOffset = Math.Min(roomEndOffset, roomOffset + transitionSpeed * (float)gameTime.ElapsedGameTime.TotalMilliseconds);
            hudOffset = Math.Min(hudEndOffset, hudOffset + transitionSpeed * (float)gameTime.ElapsedGameTime.TotalMilliseconds);

            if (roomOffset >= roomEndOffset && hudOffset >= hudEndOffset)
            {
                game.SetState(new GameStateHUD(game));
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
