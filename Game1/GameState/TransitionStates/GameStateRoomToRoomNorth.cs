/* Author: Hunter Figgs.3 */

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
    class GameStateRoomToRoomNorth : IGameState
    {
        private readonly Game1 game;
        private readonly List<IController> controllerList;

        private const float transitionSpeed = TransitionUtil.TransitionSpeed; // pixels per ms

        private const float vertRoomOffset = 40f;
        private const float vertRoomDim = 176f;

        private const int newPlayerX = 120;
        private const int newPlayerY = 142;
        private const int newPlayerYLockedOffset = -16;

        private readonly Vector2 oldRoomStartPos = new Vector2(0, vertRoomOffset);
        private readonly Vector2 oldRoomEndPos = new Vector2(0, vertRoomOffset + vertRoomDim);
        private Vector2 oldRoomPos;

        private readonly Vector2 newRoomOffset = new Vector2(0, -vertRoomDim);

        private readonly Vector2 newPlayerPosition;

        private readonly (char, int) northRoomKey;

        public GameStateRoomToRoomNorth(Game1 game, int playerID)
        {
            this.game = game;

            controllerList = new List<IController>
            {
                new KeyboardQuitController(game),
                new GamepadQuitController(game, PlayerIndex.One)
            };

            oldRoomPos = oldRoomStartPos;

            newPlayerPosition = new Vector2(newPlayerX, newPlayerY);
            if (RoomUtil.IsAdjacentDoorClosed(game.Screen, CompassDirection.North))
            {
                newPlayerPosition.Y += newPlayerYLockedOffset;
            }

            List<IPlayer> playerList = new List<IPlayer>();
            playerList.AddRange(game.Screen.Players); //copy to avoid messing up controls

            if (playerID != 1)
                playerList.Reverse();

            foreach (IPlayer p in playerList)
            {
                p.EditPosition(Vector2.Subtract(newPlayerPosition, p.GetPlayerHitbox().Location.ToVector2()));
                p.PlayerInventory.RefreshCandle();
                newPlayerPosition.Y += newPlayerYLockedOffset;
            }

            northRoomKey = RoomUtil.GetAdjacentRoomKey(game.Screen.CurrentRoomKey, CompassDirection.North);

            game.Screen.CurrentRoom.StopRoomAmbience();
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
            DrawUtil.ClearScreen(game);

            DrawUtil.DrawRoom(game.Screen.CurrentRoom, spriteBatch, resolutionManager, new Vector2(oldRoomPos.X, oldRoomPos.Y));

            DrawUtil.DrawRoomAndPlayers(game.Screen.RoomsDict[northRoomKey], game.Screen.Players, spriteBatch, resolutionManager, Vector2.Add(oldRoomPos, newRoomOffset));

            DrawUtil.DrawHUD(game.HUD, spriteBatch, resolutionManager);
        }
    }
}
