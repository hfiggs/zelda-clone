/* Author: Hunter Figgs.3 */

using Game1.Controller;
using Game1.Item;
using Game1.ResolutionManager;
using Game1.Sprite;
using Game1.Util;
using Game1.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Game1.GameState.GameStateUtil;

namespace Game1.GameState
{
    class GameStatePickupItem : IGameState
    {
        private readonly Game1 game;
        private readonly List<IController> controllerList;

        private readonly ISprite playerSprite;
        private readonly ISprite itemSprite;

        private const float playerXOffset = -14f;
        private const float playerYOffset = -20f;
        private const float itemXOffset = -8f;
        private const float itemYOffset = -12f;

        private readonly Vector2 playerPosition;
        private readonly Vector2 itemPositionOffset = new Vector2(itemXOffset, itemYOffset);

        private const float roomOffset = 40f;

        private float stateTimer;
        private const float stateTime = 1000f; // ms

        private readonly Color color = Color.White;

        public GameStatePickupItem(Game1 game, PickupItem pickupItem, IItem itemToRemove, IPlayer player)
        {
            this.game = game;

            controllerList = new List<IController>
            {
                new KeyboardQuitController(game),
                new GamepadQuitController(game, PlayerIndex.One)
            };

            game.Screen.CurrentRoom.ItemList.Remove(itemToRemove);

            playerSprite = PickupUtil.GetPlayerPickupSprite(pickupItem, player);
            itemSprite = PickupUtil.GetPickupItemSprite(pickupItem);

            playerPosition = Vector2.Add(player.GetPlayerHitbox().Location.ToVector2(), new Vector2(playerXOffset, playerYOffset));

            stateTimer = stateTime;
        }

        public void Update(GameTime gameTime)
        {
            foreach (IController controller in controllerList)
            {
                controller.Update();
            }

            const int mousePosition = 150;
            Mouse.SetPosition(mousePosition, mousePosition);

            stateTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (stateTimer <= 0)
            {
                game.SetState(new GameStateRoom(game));
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, IResolutionManager resolutionManager)
        {
            game.GraphicsDevice.Clear(Color.Black);

            var drawMatrix = resolutionManager.GetResolutionMatrix();


            drawMatrix.Translation = new Vector3(0, roomOffset * resolutionManager.GetResolutionScale(), 0);

            spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, null, null, null, drawMatrix);

            game.Screen.CurrentRoom.Draw(spriteBatch, color);

            playerSprite.Draw(spriteBatch, playerPosition, color, SpriteLayerUtil.playerLayer);
            itemSprite.Draw(spriteBatch, Vector2.Add(playerPosition, itemPositionOffset), color, SpriteLayerUtil.itemLayer);

            spriteBatch.End();

            DrawUtil.DrawHUDOffset(game.HUD, spriteBatch, resolutionManager);

            drawMatrix.Translation = new Vector3(0, 0, 0);
        }
    }
}
