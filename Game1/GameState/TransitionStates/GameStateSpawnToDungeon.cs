/* Author: Hunter Figgs.3 */

using Game1.Audio;
using Game1.Controller;
using Game1.GameState.GameStateUtil;
using Game1.Player;
using Game1.ResolutionManager;
using Game1.RoomLoading;
using Game1.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Game1.GameState
{
    class GameStateSpawnToDungeon : IGameState
    {
        private readonly Game1 game;
        private readonly List<IController> controllerList;

        private const float transitionSpeed = TransitionUtil.TransitionSpeed; // pixels per ms

        private const float vertRoomOffset = 40f;
        private const float vertRoomDim = 176f;

        private const int newPlayerX = 120;
        private const int newPlayerY = 142;

        private const int playerOffset = 16;

        private Color color = Color.White;

        private readonly Vector2 oldRoomStartPos = new Vector2(0, vertRoomOffset);
        private readonly Vector2 oldRoomEndPos = new Vector2(0, vertRoomOffset + vertRoomDim);
        private Vector2 oldRoomPos;

        private readonly Vector2 newRoomOffset = new Vector2(0, -vertRoomDim);

        private readonly Room newRoom;
        private readonly (char, int) newRoomKey = ('F', 2);

        private readonly Vector2 newPlayerPosition = new Vector2(newPlayerX, newPlayerY);

        private readonly (char, int) northRoomKey;

        public GameStateSpawnToDungeon(Game1 game)
        {
            this.game = game;
            game.Screen.RoomsDict.TryGetValue(newRoomKey, out newRoom);

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
                newPlayerPosition.Y -= playerOffset;
            }

            northRoomKey = RoomUtil.GetAdjacentRoomKey(game.Screen.CurrentRoomKey, CompassDirection.North);

            AudioManager.StopAllMusic();

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
                AudioManager.ClearQueue();
                AudioManager.StopAllMusic();
                newRoom.PlayMusic();

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
