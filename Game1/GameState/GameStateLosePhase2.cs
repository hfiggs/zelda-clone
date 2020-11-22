/* Author: Hunter Figgs.3 */

using Game1.Audio;
using Game1.Controller;
using Game1.Particle;
using Game1.Player;
using Game1.ResolutionManager;
using Game1.Sprite;
using Game1.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Game1.GameState
{
    class GameStateLosePhase2 : IGameState
    {
        private readonly Game1 game;
        private readonly List<IController> controllerList;

        private const float playerXOffset = -14f;
        private const float playerYOffset = -20f;

        private const float roomOffset = 40f;
        private const float hudOffset = -136f;

        private const float spinTime = 2000f; // ms
        private float spinTimer;

        private const float spinAnimationTime = 80f; // ms
        private float spinAnimationTimer;

        private readonly Color colorHUD = Color.White;
        private Color colorRoom = Color.White;
        private readonly Color colorPlayer = Color.White;

        private const float fadeColorStep = 0.1666f;
        private const float fadeTimeStep = 166.66f;
        private float fadeTimer = 750.0f;
        private Color flashColorFade = new Color(new Vector4(0.0f, 0.0f, 0.0f, 0.0f));
        private const float flashOnTime = spinTime + 4000.0f/*stareTime + initialStareTime + poppedTime*/; //ms
        private const float flashOffTime = 0.0f; //ms
        private const float flashInitialDelay = 0.0f; //ms

        private readonly IParticle flash1;
        private readonly IParticle flash2;
        private IParticle fadeFlash;

        private ISprite deadLink;
        private Vector2 deadLinkPosition;

        public GameStateLosePhase2(Game1 game, IParticle flash1, IParticle flash2)
        {
            this.game = game;

            controllerList = new List<IController>
            {
                new KeyboardQuitController(game),
                new GamepadQuitController(game, PlayerIndex.One)
            };

            game.Screen.CurrentRoom.EnemyList.Clear();
            game.Screen.CurrentRoom.ItemList.Clear();
            game.Screen.CurrentRoom.ProjectileList.Clear();

            if (game.Screen.Player.GetType() == typeof(Player1)) {
                deadLink = PlayerSpriteFactory.Instance.CreateDeadSprite();
            } else {
                deadLink = PlayerSpriteFactory.Instance.CreateZeldaDeadSprite();
            }
            
            deadLinkPosition = Vector2.Add(game.Screen.Player.GetPlayerHitbox().Location.ToVector2(), new Vector2(playerXOffset, playerYOffset));

            this.flash1 = flash1;
            this.flash2 = flash2;
            fadeFlash = new Flash(flashColorFade, 1, flashOnTime, flashOffTime, flashInitialDelay);

            spinTimer = spinTime;
        }

        public void Update(GameTime gameTime)
        {
            foreach (IController controller in controllerList)
            {
                controller.Update();
            }

            if (spinTimer > 0)
            {                    
                fadeTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (fadeTimer < 0)
                {
                    Vector4 fadeColor = flashColorFade.ToVector4();
                    fadeColor.W = (fadeColor.W + fadeColorStep) > 1.0f ? 1.0f : (fadeColor.W + fadeColorStep);
                    flashColorFade = new Color(fadeColor);
                    fadeTimer += fadeTimeStep;
                    fadeFlash = new Flash(flashColorFade, 1, flashOnTime + 1000.0f, flashOffTime, flashInitialDelay);
                    fadeFlash.Update(gameTime);
                }
                flash1.Update(gameTime);
                flash2.Update(gameTime);

                spinTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                spinAnimationTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (spinAnimationTimer <= 0)
                {
                    deadLink.Update();
                    spinAnimationTimer += spinAnimationTime;
                }
            } else
            {
                game.SetState(new GameStateLosePhase3(game));
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, IResolutionManager resolutionManager)
        {
            game.GraphicsDevice.Clear(Color.Black);

            var drawMatrix = resolutionManager.GetResolutionMatrix();

            drawMatrix.Translation = new Vector3(0, roomOffset * resolutionManager.GetResolutionScale(), 0);

            spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, null, null, null, drawMatrix);

            game.Screen.CurrentRoom.Draw(spriteBatch, colorRoom);

            deadLink.Draw(spriteBatch, deadLinkPosition, colorPlayer, SpriteLayerUtil.topLayer);

            flash1.Draw(spriteBatch, colorRoom);
            flash2.Draw(spriteBatch, colorRoom);

            fadeFlash.Draw(spriteBatch, colorPlayer);

            spriteBatch.End();

            drawMatrix.Translation = new Vector3(0, hudOffset * resolutionManager.GetResolutionScale(), 0);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, drawMatrix);

            game.HUD.Draw(spriteBatch, new Vector2(0, 0), colorHUD);

            spriteBatch.End();

            drawMatrix.Translation = new Vector3(0, 0, 0);
        }
    }
}
