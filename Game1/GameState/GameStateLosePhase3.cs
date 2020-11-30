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
    class GameStateLosePhase3 : IGameState
    {
        private readonly Game1 game;
        private readonly List<IController> controllerList;

        private const float playerXOffset = -14f;
        private const float playerYOffset = -20f;

        private const float roomOffset = 40f;
        private const float hudOffset = -136f;

        private const float stareTime = 1000f; // ms
        private float stareTimer;

        private const float poppedTime = 2000f; // ms
        private float poppedTimer;

        private readonly Color colorHUD = Color.White;
        private Color colorRoom = Color.Black;
        private readonly Color colorPlayer = Color.White;

        private const float linkPopDelay = 1000.0f;
        private const int linkPopOffset = 12;

        private IParticle linkPop;

        private ISprite deadLink;
        private Vector2 deadLinkPosition;

        public GameStateLosePhase3(Game1 game, IPlayer player)
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

            if (player.GetType() == typeof(Player1)) {
                deadLink = PlayerSpriteFactory.Instance.CreateDeadSprite();
            } else {
                deadLink = PlayerSpriteFactory.Instance.CreateZeldaDeadSprite();
            }
            
            deadLinkPosition = Vector2.Add(player.GetPlayerHitbox().Location.ToVector2(), new Vector2(playerXOffset, playerYOffset));
            linkPop = new LinkPop(deadLinkPosition + new Vector2(linkPopOffset, linkPopOffset), linkPopDelay);

            stareTimer = stareTime;
            poppedTimer = poppedTime;
        }

        public void Update(GameTime gameTime)
        {
            linkPop.Update(gameTime);
            
            foreach (IController controller in controllerList)
            {
                controller.Update();
            }

            if (stareTimer > 0)
            {
                stareTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                const string linkPopAudio = "linkPop";

                if (stareTimer <= 0)
                {
                    AudioManager.PlayFireForget(linkPopAudio);
                }
            }
            else
            {
                poppedTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (poppedTimer <= 0)
                {
                    game.SetState(new GameStateSkyrim(game));
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, IResolutionManager resolutionManager)
        {
            game.GraphicsDevice.Clear(Color.Black);

            var drawMatrix = resolutionManager.GetResolutionMatrix();


            drawMatrix.Translation = new Vector3(0, roomOffset * resolutionManager.GetResolutionScale(), 0);

            spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, null, null, null, drawMatrix);

            game.Screen.CurrentRoom.Draw(spriteBatch, colorRoom);

            if (stareTimer > 0)
            {
                deadLink.Draw(spriteBatch, deadLinkPosition, colorPlayer, SpriteLayerUtil.topLayer);
            } else
            {
                linkPop.Draw(spriteBatch, colorPlayer);
            }

            spriteBatch.End();


            drawMatrix.Translation = new Vector3(0, hudOffset * resolutionManager.GetResolutionScale(), 0);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, drawMatrix);

            game.HUD.Draw(spriteBatch, new Vector2(0, 0), colorHUD);

            spriteBatch.End();


            drawMatrix.Translation = new Vector3(0, 0, 0);
        }
    }
}
