﻿using Game1.Controller;
using Game1.ResolutionManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Game1.GameState
{
    class GameStateRoomToRoomWest : IGameState
    {
        private readonly Game1 game;
        private readonly List<IController> controllerList;

        private const float transitionSpeed = 0.1f; // pixels per ms

        private const float hudOffset = -136f;
        private const float vertRoomOffset = 40f;
        private const float horizRoomDim = 256f;

        private const int newPlayerX = 224;
        private const int newPlayerY = 86;

        private readonly Vector2 oldRoomStartPos = new Vector2(0, vertRoomOffset);
        private readonly Vector2 oldRoomEndPos = new Vector2(horizRoomDim, vertRoomOffset);
        private Vector2 oldRoomPos;

        private readonly Vector2 newRoomOffset = new Vector2(-horizRoomDim, 0);

        private readonly Vector2 newPlayerPosition = new Vector2(newPlayerX, newPlayerY);

        public GameStateRoomToRoomWest(Game1 game)
        {
            this.game = game;

            controllerList = new List<IController>
            {
                new KeyboardTransitionController(game),
                new GamepadTransitionController(game, PlayerIndex.One)
            };

            oldRoomPos = oldRoomStartPos;

            game.Screen.Player.EditPosition(Vector2.Subtract(newPlayerPosition, game.Screen.Player.GetPlayerHitbox().Location.ToVector2()));
        }

        public void Update(GameTime gameTime)
        {
            foreach (IController controller in controllerList)
            {
                controller.Update();
            }

            Mouse.SetPosition(150, 150);

            var ms = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            oldRoomPos = Vector2.Add(oldRoomPos, new Vector2(ms * transitionSpeed, 0));

            if (oldRoomPos.X >= oldRoomEndPos.X)
            {
                game.Screen.CurrentRoomKey = GetWestRoomKey();
                game.SetState(new GameStateRoom(game));
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, IResolutionManager resolutionManager)
        {
            game.GraphicsDevice.Clear(Color.Black);

            var drawMatrix = resolutionManager.GetResolutionMatrix();


            drawMatrix.Translation = new Vector3(oldRoomPos.X * resolutionManager.GetResolutionScale(), oldRoomPos.Y * resolutionManager.GetResolutionScale(), 0);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, drawMatrix);

            game.Screen.CurrentRoom.Draw(spriteBatch);

            spriteBatch.End();


            var newRoom = game.Screen.RoomsDict[GetWestRoomKey()];

            drawMatrix.Translation = Vector3.Add(drawMatrix.Translation, new Vector3(newRoomOffset.X * resolutionManager.GetResolutionScale(), newRoomOffset.Y * resolutionManager.GetResolutionScale(), 0));

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, drawMatrix);

            newRoom.Draw(spriteBatch);

            game.Screen.Player.Draw(spriteBatch, Color.White);

            spriteBatch.End();


            drawMatrix.Translation = new Vector3(0, hudOffset * resolutionManager.GetResolutionScale(), 0);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, drawMatrix);

            game.HUD.Draw(spriteBatch, new Vector2(0, 0), Color.White);

            spriteBatch.End();


            drawMatrix.Translation = new Vector3(0, 0, 0);
        }

        private (char, int) GetWestRoomKey()
        {
            var oldRoomKey = game.Screen.CurrentRoomKey;

            var newRoomKey = (oldRoomKey.Item1, oldRoomKey.Item2);
                    
            newRoomKey.Item2--;

            return newRoomKey;
        }
    }
}
