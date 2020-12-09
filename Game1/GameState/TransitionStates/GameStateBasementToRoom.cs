﻿/* Author: Hunter Figgs.3 */

using Game1.Audio;
using Game1.Controller;
using Game1.GameState.GameStateUtil;
using Game1.Player;
using Game1.ResolutionManager;
using Game1.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Game1.GameState
{
    class GameStateBasementToRoom : IGameState
    {
        private readonly Game1 game;
        private readonly List<IController> controllerList;

        private const float transitionSpeed = TransitionUtil.TransitionSpeed; // pixels per ms

        private const float vertRoomOffset = 40f;
        private const float vertRoomDim = 176f;

        private const int newPlayerX = 104;
        private const int newPlayerY = 84;

        private const int playerOffset = 16;

        private Color color = Color.White;

        private readonly Vector2 oldRoomStartPos = new Vector2(0, vertRoomOffset);
        private readonly Vector2 oldRoomEndPos = new Vector2(0, vertRoomOffset + vertRoomDim);
        private Vector2 oldRoomPos;

        private readonly Vector2 newRoomOffset = new Vector2(0, -vertRoomDim);

        private readonly Vector2 newPlayerPosition = new Vector2(newPlayerX, newPlayerY);

        private readonly (char, int) northRoomKey;

        public GameStateBasementToRoom(Game1 game)
        {
            this.game = game;

            controllerList = new List<IController>
            {
                new KeyboardQuitController(game),
                new GamepadQuitController(game, PlayerIndex.One)
            };

            oldRoomPos = oldRoomStartPos;

            foreach (IPlayer p in game.Screen.Players)
            {
                p.EditPosition(Vector2.Subtract(newPlayerPosition, p.GetPlayerHitbox().Location.ToVector2()));
                p.PlayerInventory.RefreshCandle();
                p.MoveLeft();
                newPlayerPosition.X -= playerOffset;
            }

            northRoomKey = RoomUtil.GetAdjacentRoomKey(game.Screen.CurrentRoomKey, CompassDirection.North);

            const string stairsAudio = "stairs";
            AudioManager.PlayFireForget(stairsAudio);
        }

        public void Update(GameTime gameTime)
        {
            foreach (IController controller in controllerList)
            {
                controller.Update();
            }

            Mouse.SetPosition(150, 150);

            var ms = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            oldRoomPos = Vector2.Add(oldRoomPos, new Vector2(0, ms * transitionSpeed));

            if (oldRoomPos.Y >= oldRoomEndPos.Y)
            {
                game.Screen.CurrentRoomKey = northRoomKey;
                game.SetState(new GameStateRoom(game));
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, IResolutionManager resolutionManager)
        {
            game.GraphicsDevice.Clear(Color.Black);

            var drawMatrix = resolutionManager.GetResolutionMatrix();


            drawMatrix.Translation = new Vector3(oldRoomPos.X * resolutionManager.GetResolutionScale(), oldRoomPos.Y * resolutionManager.GetResolutionScale(), 0);

            spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, null, null, null, drawMatrix);

            game.Screen.CurrentRoom.Draw(spriteBatch, color);

            spriteBatch.End();


            var newRoom = game.Screen.RoomsDict[northRoomKey];

            drawMatrix.Translation = Vector3.Add(drawMatrix.Translation, new Vector3(newRoomOffset.X * resolutionManager.GetResolutionScale(), newRoomOffset.Y * resolutionManager.GetResolutionScale(), 0));

            spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, null, null, null, drawMatrix);

            newRoom.Draw(spriteBatch, color);

            foreach(IPlayer p in game.Screen.Players)
            {
                p.Draw(spriteBatch, Color.White);
            }

            spriteBatch.End();

            DrawUtil.DrawHUDOffset(game.HUD, spriteBatch, resolutionManager);

            drawMatrix.Translation = new Vector3(0, 0, 0);
        }
    }
}
