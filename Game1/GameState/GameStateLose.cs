/* Author: Hunter Figgs.3 */

using Game1.Audio;
using Game1.Controller;
using Game1.Player;
using Game1.ResolutionManager;
using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game1.GameState
{
    class GameStateLose : IGameState
    {
        private readonly Game1 game;
        private readonly List<IController> controllerList;

        private const float roomOffset = 40f;
        private const float hudOffset = -136f;

        private const float spinTime = 2000f; // ms
        private float spinTimer;

        private const float stareTime = 1000f; // ms
        private float stareTimer;

        private const float poppedTime = 1000f; // ms
        private float poppedTimer;

        private const float spinAnimationTime = 100f; // ms
        private float spinAnimationTimer;

        private readonly Color colorHUD = Color.White;
        private Color colorRoom = Color.White;
        private readonly Color colorRoomRed = Color.Red;
        private readonly Color colorPlayer = Color.White;

        private ISprite deadLink;
        private Vector2 deadLinkPosition;

        public GameStateLose(Game1 game)
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

            deadLink = PlayerSpriteFactory.Instance.CreateDeadSprite();
            deadLinkPosition = game.Screen.Player.GetLocation().Location.ToVector2();

            spinTimer = spinTime;
            stareTimer = stareTime;
            spinAnimationTimer = spinAnimationTime;
            poppedTimer = poppedTime;

            AudioManager.StopAllMusic();
            AudioManager.StopAllSound();
            AudioManager.PlayFireForget("death");
        }

        public void Update(GameTime gameTime)
        {
            foreach (IController controller in controllerList)
            {
                controller.Update();
            }

            if (spinTimer > 0)
            {
                spinTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                spinAnimationTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (spinAnimationTimer <= 0)
                {
                    deadLink.Update();
                    spinAnimationTimer += spinAnimationTime;
                }

                if (spinTimer <= 0)
                {
                    deadLink = PlayerSpriteFactory.Instance.CreateDeadSprite();
                    colorRoom = colorRoomRed;
                }
            }
            else if (stareTimer > 0)
            {
                stareTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (stareTimer <= 0)
                {
                    AudioManager.PlayFireForget("linkPop");
                }
            }
            else
            {
                poppedTimer -=(float)gameTime.ElapsedGameTime.TotalMilliseconds;

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

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, drawMatrix);

            game.Screen.CurrentRoom.Draw(spriteBatch, colorRoom);

            if (stareTimer > 0)
                deadLink.Draw(spriteBatch, deadLinkPosition, colorPlayer);

            spriteBatch.End();


            drawMatrix.Translation = new Vector3(0, hudOffset * resolutionManager.GetResolutionScale(), 0);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, drawMatrix);

            game.HUD.Draw(spriteBatch, new Vector2(0, 0), colorHUD);

            spriteBatch.End();


            drawMatrix.Translation = new Vector3(0, 0, 0);
        }
    }
}
