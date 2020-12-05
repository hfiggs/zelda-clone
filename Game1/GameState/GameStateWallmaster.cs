/* Author: Hunter Figgs.3 */

using Game1.Controller;
using Game1.Util;
using Game1.ResolutionManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Game1.Particle;
using Game1.Sprite;
using Game1.Player;
using Game1.Enemy;
using Game1.RoomLoading;

namespace Game1.GameState
{
    class GameStateWallmaster : IGameState
    {
        private readonly Game1 game;
        private readonly List<IController> controllerList;

        private ISprite playerSprite;
        private ISprite wallmaster;

        private Vector2 playerPosition;
        private Vector2 wallmasterPosition;

        private const float roomOffset = 40f;
        private const float hudOffset = -136f;

        private const float playerXOffset = -14f;
        private const float playerYOffset = -20f;

        private const float wallmasterXOffset = 8f;
        private const float wallmasterYOffset = 8f;

        private const float wallmasterYSpeed = 1; // px per second

        private const float finishY = 200f;

        private Color color = Color.White;

        public GameStateWallmaster(Game1 game, IPlayer player)
        {
            this.game = game;

            controllerList = new List<IController>
            {
                new KeyboardPausedController(game),
                new GamepadPausedController(game, PlayerIndex.One)
            };

            game.Screen.CurrentRoom.EnemyList.Clear();
            game.Screen.CurrentRoom.ItemList.Clear();
            game.Screen.CurrentRoom.ProjectileList.Clear();

            if (player.playerID == 1)
            {
                playerSprite = PlayerSpriteFactory.Instance.CreateWalkDownSprite();
            }
            else
            {
                playerSprite = PlayerSpriteFactory.Instance.CreateZeldaWalkDownSprite();
            }
            wallmaster = EnemySpriteFactory.Instance.CreateHandSprite();

            playerPosition = Vector2.Add(player.GetPlayerHitbox().Location.ToVector2(), new Vector2(playerXOffset, playerYOffset));
            wallmasterPosition = Vector2.Add(playerPosition, new Vector2(wallmasterXOffset, wallmasterYOffset));

            game.Screen.ResurrectEnemies();
            game.Screen.UnclockRooms();
        }

        public void Update(GameTime gameTime)
        {
            foreach (IController controller in controllerList)
            {
                controller.Update();
            }

            playerPosition.Y += wallmasterYSpeed;
            wallmasterPosition.Y += wallmasterYSpeed;

            if (playerPosition.Y >= finishY)
            {
                game.SetState(new GameStatePlayerToStart(game));
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, IResolutionManager resolutionManager)
        {
            game.GraphicsDevice.Clear(Color.Black);

            var drawMatrix = resolutionManager.GetResolutionMatrix();


            drawMatrix.Translation = new Vector3(0, roomOffset * resolutionManager.GetResolutionScale(), 0);

            spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, null, null, null, drawMatrix);

            game.Screen.CurrentRoom.Draw(spriteBatch, color);

            playerSprite.Draw(spriteBatch, playerPosition, color);

            wallmaster.Draw(spriteBatch, wallmasterPosition, color, SpriteLayerUtil.topLayer);

            spriteBatch.End();


            drawMatrix.Translation = new Vector3(0, hudOffset * resolutionManager.GetResolutionScale(), 0);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, drawMatrix);

            game.HUD.Draw(spriteBatch, new Vector2(0, 0), color);

            spriteBatch.End();


            drawMatrix.Translation = new Vector3(0, 0, 0);
        }
    }
}
