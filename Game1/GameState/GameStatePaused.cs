/* Author: Hunter Figgs.3 */

using Game1.Controller;
using Game1.ResolutionManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Game1.GameState.GameStateUtil;

namespace Game1.GameState
{
    class GameStatePaused : IGameState
    {
        private readonly Game1 game;
        private readonly List<IController> controllerList;

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
            DrawUtil.ClearScreen(game);

            DrawUtil.DrawScreen(game.Screen, spriteBatch, resolutionManager);

            DrawUtil.DrawHUD(game.HUD, spriteBatch, resolutionManager);
        }
    }
}
