/* Author: Hunter Figgs.3 */

using Game1.Controller;
using Game1.GameState.GameStateUtil;
using Game1.Graphics;
using Game1.Player;
using Game1.ResolutionManager;
using Game1.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Game1.GameState
{
    class GameStateRoomToRoomEast : IGameState
    {
        private readonly Game1 game;
        private readonly List<IController> controllerList;

        private const float transitionSpeed = TransitionUtil.TransitionSpeed; // pixels per ms

        private const float vertRoomOffset = 40f;
        private const float horizRoomDim = 256f;

        private const int newPlayerX = 16;
        private const int newPlayerY = 86;
        private const int newPlayerXLockedOffset = 16;

        private readonly Vector2 oldRoomStartPos = new Vector2(0, vertRoomOffset);
        private readonly Vector2 oldRoomEndPos = new Vector2(-horizRoomDim, vertRoomOffset);
        private Vector2 oldRoomPos;

        private readonly Vector2 newRoomOffset = new Vector2(horizRoomDim, 0);

        private readonly Vector2 newPlayerPosition;

        private readonly (char, int) eastRoomKey;

        public GameStateRoomToRoomEast(Game1 game, int playerID)
        {
            this.game = game;

            controllerList = new List<IController>
            {
                new KeyboardQuitController(game),
                new GamepadQuitController(game, PlayerIndex.One)
            };

            oldRoomPos = oldRoomStartPos;

            newPlayerPosition = new Vector2(newPlayerX, newPlayerY);
            if (RoomUtil.IsAdjacentDoorClosed(game.Screen, CompassDirection.East))
            {
                newPlayerPosition.X += newPlayerXLockedOffset;
            }

            List<IPlayer> playerList = new List<IPlayer>();
            playerList.AddRange(game.Screen.Players); //copy to avoid messing up controls
            
            if (playerID != 1)
                playerList.Reverse();

            foreach (IPlayer p in playerList)
            {
                p.EditPosition(Vector2.Subtract(newPlayerPosition, p.GetPlayerHitbox().Location.ToVector2()));
                p.PlayerInventory.RefreshCandle();
                newPlayerPosition.X += newPlayerXLockedOffset;
            }

            eastRoomKey = RoomUtil.GetAdjacentRoomKey(game.Screen.CurrentRoomKey, CompassDirection.East);

            game.Screen.CurrentRoom.RoomMeta.StopRoomAmbience();
        }

        public void Update(GameTime gameTime)
        {
            foreach (IController controller in controllerList)
            {
                controller.Update();
            }

            Mouse.SetPosition(150, 150);

            var ms = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            oldRoomPos = Vector2.Subtract(oldRoomPos, new Vector2(ms * transitionSpeed, 0));

            if (oldRoomPos.X <= oldRoomEndPos.X)
            {
                game.Screen.CurrentRoomKey = eastRoomKey;
                game.SetState(new GameStateRoom(game));
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, IResolutionManager resolutionManager)
        {
            DrawUtil.ClearScreen(game);

            Texture2D shadowMaskNew = null, shadowMaskOld = null;
            if (!game.Screen.CurrentRoom.RoomMeta.IsLit)
                shadowMaskOld = ShadowMask.GetBlankShadowMask(game.GraphicsDevice, spriteBatch);
            if (!game.Screen.RoomsDict[eastRoomKey].RoomMeta.IsLit)
                shadowMaskNew = ShadowMask.GetBlankShadowMask(game.GraphicsDevice, spriteBatch);

            DrawUtil.DrawRoom(game.Screen.CurrentRoom, spriteBatch, resolutionManager, new Vector2(oldRoomPos.X, oldRoomPos.Y));

            DrawUtil.DrawRoomAndPlayers(game.Screen.RoomsDict[eastRoomKey], game.Screen.Players, spriteBatch, resolutionManager, Vector2.Add(oldRoomPos, newRoomOffset));

            if (shadowMaskOld != null)
                DrawUtil.DrawShadowMask(shadowMaskOld, spriteBatch, resolutionManager, new Vector2(oldRoomPos.X, oldRoomPos.Y));

            if (shadowMaskNew != null)
                DrawUtil.DrawShadowMask(shadowMaskNew, spriteBatch, resolutionManager, Vector2.Add(oldRoomPos, newRoomOffset));

            DrawUtil.DrawHUD(game.HUD, spriteBatch, resolutionManager);
        }
    }
}
