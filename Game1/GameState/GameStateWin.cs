/* Author: Hunter Figgs.3 */

using Game1.Audio;
using Game1.Controller;
using Game1.Item;
using Game1.Particle;
using Game1.ResolutionManager;
using Game1.Sprite;
using Game1.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game1.GameState
{
    class GameStateWin : IGameState
    {
        private readonly Game1 game;
        private readonly List<IController> controllerList;

        private readonly ISprite playerSprite;
        private readonly ISprite itemSprite;

        private readonly IParticle curtain;
        private readonly IParticle flash;

        private const float playerXOffset = -14f;
        private const float playerYOffset = -20f;
        private const float itemXOffset = -1f;
        private const float itemYOffset = -12f;

        private readonly Vector2 playerPosition;
        private readonly Vector2 itemPositionOffset = new Vector2(itemXOffset, itemYOffset);

        private const float roomOffset = 40f;
        private const float hudOffset = -136f;

        private float stateTimer;
        private const float curtainDelay = 4000f; // ms
        private const float stateTime = 8000f; // ms

        private readonly Color flashColor = new Color(new Vector4(1.0f, 1.0f, 1.0f, 0.5f));
        private const int flashes = 4;
        private const float flashOnTime = 35.0f; //ms
        private const float flashOffTime = 150.0f; //ms
        private const float flashInitialDelay = 1000.0f; //ms

        private readonly Color color = Color.White;

        public GameStateWin(Game1 game, PickupItem pickupItem, IItem itemToRemove)
        {
            this.game = game;

            controllerList = new List<IController>
            {
                new KeyboardQuitController(game),
                new GamepadQuitController(game, PlayerIndex.One)
            };

            game.Screen.CurrentRoom.ItemList.Remove(itemToRemove);

            playerSprite = GameStateUtil.GetPlayerPickupSprite(pickupItem, game.Screen.Player);
            itemSprite = GameStateUtil.GetPickupItemSprite(pickupItem);

            playerPosition = Vector2.Add(game.Screen.Player.GetPlayerHitbox().Location.ToVector2(), new Vector2(playerXOffset, playerYOffset));

            curtain = new Curtain(game, false);
            flash = new Flash(flashColor, flashes, flashOnTime, flashOffTime, flashInitialDelay);

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
                AudioManager.ResetAudioManager();
                game.SetState(new GameStateStart(game));
            }
            else if (stateTime - stateTimer >= curtainDelay)
            {
                curtain.Update(gameTime);
            }

            flash.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, IResolutionManager resolutionManager)
        {
            game.GraphicsDevice.Clear(Color.Black);

            var drawMatrix = resolutionManager.GetResolutionMatrix();


            drawMatrix.Translation = new Vector3(0, roomOffset * resolutionManager.GetResolutionScale(), 0);

            spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, null, null, null, drawMatrix);

            game.Screen.CurrentRoom.Draw(spriteBatch, color);

            playerSprite.Draw(spriteBatch, playerPosition, color, SpriteLayerUtil.topLayer);
            itemSprite.Draw(spriteBatch, Vector2.Add(playerPosition, itemPositionOffset), color, SpriteLayerUtil.topLayer);
            curtain.Draw(spriteBatch, color);
            flash.Draw(spriteBatch, color);

            spriteBatch.End();


            drawMatrix.Translation = new Vector3(0, hudOffset * resolutionManager.GetResolutionScale(), 0);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, drawMatrix);

            game.HUD.Draw(spriteBatch, new Vector2(0, 0), color);

            spriteBatch.End();


            drawMatrix.Translation = new Vector3(0, 0, 0);
        }
    }
}
