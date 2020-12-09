/* Author: Hunter Figgs.3 */

using Game1.Controller;
using Game1.GameState.GameStateUtil;
using Game1.Particle;
using Game1.Player;
using Game1.ResolutionManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Game1.GameState
{
    class GameStatePlayerToStart : IGameState
    {
        private readonly Game1 game;
        private readonly List<IController> controllerList;

        private readonly IParticle curtain;

        private const float vertRoomOffset = 40f;

        private const int newPlayerX = 120;
        private const int newPlayerY = 142;
        private const int newPlayerOffset = 16;

        private Color color = Color.White;

        private readonly Vector2 oldRoomStartPos = new Vector2(0, vertRoomOffset);
        private Vector2 oldRoomPos;

        private readonly Vector2 newPlayerPosition;

        private const char startRoomChar = 'F';
        private const int startRoomInt = 2;
        private readonly (char, int) startRoomKey = (startRoomChar, startRoomInt);

        public GameStatePlayerToStart(Game1 game)
        {
            this.game = game;

            controllerList = new List<IController>
            {
                new KeyboardQuitController(game),
                new GamepadQuitController(game, PlayerIndex.One)
            };

            curtain = new Curtain(game, true);

            oldRoomPos = oldRoomStartPos;

            newPlayerPosition = new Vector2(newPlayerX, newPlayerY);

            foreach(IPlayer p in game.Screen.Players)
            {
                p.EditPosition(Vector2.Subtract(newPlayerPosition, p.GetPlayerHitbox().Location.ToVector2()));
                p.MoveUp();
                p.PlayerInventory.RefreshCandle();
                newPlayerPosition.Y -= newPlayerOffset;
            }

            game.Screen.CurrentRoom.StopRoomAmbience();

            game.Screen.CurrentRoomKey = startRoomKey;
        }

        public void Update(GameTime gameTime)
        {
            foreach (IController controller in controllerList)
            {
                controller.Update();
            }

            Mouse.SetPosition(150, 150);

            if (curtain.ShouldDelete())
            {
                game.SetState(new GameStateRoom(game));
            }

            curtain.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, IResolutionManager resolutionManager)
        {
            game.GraphicsDevice.Clear(Color.Black);

            var drawMatrix = resolutionManager.GetResolutionMatrix();


            drawMatrix.Translation = new Vector3(oldRoomPos.X * resolutionManager.GetResolutionScale(), oldRoomPos.Y * resolutionManager.GetResolutionScale(), 0);

            spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, null, null, null, drawMatrix);

            game.Screen.CurrentRoom.Draw(spriteBatch, color);

            curtain.Draw(spriteBatch, color);

            spriteBatch.End();

            DrawUtil.DrawHUD(game.HUD, spriteBatch, resolutionManager);

            drawMatrix.Translation = new Vector3(0, 0, 0);
        }
    }
}
