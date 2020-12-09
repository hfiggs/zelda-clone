/* Author: Hunter Figgs.3 */

using Game1.Controller;
using Game1.GameState.GameStateUtil;
using Game1.ResolutionManager;
using Game1.Util;
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

        private const float transitionSpeed = TransitionUtil.TransitionSpeed; // pixels per ms

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
                new KeyboardQuitController(game),
                new GamepadQuitController(game, PlayerIndex.One)
            };
            game.IsMouseVisible = false;
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
            DrawUtil.ClearScreen(game);

            DrawUtil.DrawScreen(game.Screen, spriteBatch, resolutionManager, new Vector2(0, roomOffset));

            DrawUtil.DrawHUD(game.HUD, spriteBatch, resolutionManager, new Vector2(0, hudOffset));
        }
    }
}
