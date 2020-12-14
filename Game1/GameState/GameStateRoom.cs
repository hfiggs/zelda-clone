/* Author: Hunter Figgs.3 */

using Game1.Controller;
using Game1.GameState.GameStateUtil;
using Game1.Graphics;
using Game1.ResolutionManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Game1.GameState
{
    class GameStateRoom : IGameState
    {
        private readonly Game1 game;
        private readonly List<IController> controllerList;

        private const int mousePosition = 150;

        public GameStateRoom(Game1 game)
        {
            this.game = game;

            controllerList = new List<IController>
            {
                new KeyboardController(game),
                new GamepadController(game, PlayerIndex.One)
            };       
            
            game.Screen.CurrentRoom.RoomMeta.PlayRoomAmbience();
        }

        public void Update(GameTime gameTime)
        {
            foreach (IController controller in controllerList)
            {
                controller.Update();
            }

            Mouse.SetPosition(mousePosition, mousePosition);

            game.HUD.Update(gameTime);

            game.Screen.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, IResolutionManager resolutionManager)
        {
            DrawUtil.ClearScreen(game);

            Texture2D shadowMask = null;

            if (!game.Screen.CurrentRoom.RoomMeta.IsLit)
            {
                ShadowMask.SetMaskData(game.Screen);
                shadowMask = ShadowMask.GetShadowMask(game.GraphicsDevice, spriteBatch);
            }

            DrawUtil.DrawScreen(game.Screen, spriteBatch, resolutionManager);

            DrawUtil.DrawHUD(game.HUD, spriteBatch, resolutionManager);

            if (!game.Screen.CurrentRoom.RoomMeta.IsLit)
            {
                DrawUtil.DrawShadowMask(shadowMask, spriteBatch, resolutionManager);
            }
        }
    }
}
