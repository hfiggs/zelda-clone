/* Author: Hunter Figgs.3 */

using Game1.Controller;
using Game1.GameState.GameStateUtil;
using Game1.ResolutionManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game1.GameState
{
    class GameStateHUD : IGameState
    {
        private readonly Game1 game;
        private readonly List<IController> controllerList;

        private const float hudOffset = 40f;

        public GameStateHUD(Game1 game)
        {
            this.game = game;

            controllerList = new List<IController>
            {
                new HUDKeyboardController(game),
                new HUDGamepadController(game, PlayerIndex.One)
            };
        }

        public void Update(GameTime gameTime)
        {
            foreach (IController controller in controllerList)
            {
                controller.Update();
            }

            game.HUD.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, IResolutionManager resolutionManager)
        {
            DrawUtil.ClearScreen(game);

            DrawUtil.DrawHUD(game.HUD, spriteBatch, resolutionManager, new Vector2(0, hudOffset));
        }
    }
}
