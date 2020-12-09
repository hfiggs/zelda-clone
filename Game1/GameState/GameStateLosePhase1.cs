/* Author: Hunter Figgs.3 */

using Game1.Audio;
using Game1.Controller;
using Game1.GameState.GameStateUtil;
using Game1.Particle;
using Game1.Player;
using Game1.ResolutionManager;
using Game1.Sprite;
using Game1.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game1.GameState
{
    class GameStateLosePhase1 : IGameState
    {
        private readonly Game1 game;
        private readonly List<IController> controllerList;

        private const float playerXOffset = -14f;
        private const float playerYOffset = -20f;

        private const float roomOffset = 40f;

        private const float initialStareTime = 1000.0f; //ms
        private float initialStareTimer = 1000.0f;

        private Color colorRoom = Color.White;
        private readonly Color colorRoomRed = new Color(new Vector4(1.0f, 0.0f, 0.0f, 0.5f)); //room "draw" color
        private readonly Color flashColorRed1 = new Color(new Vector4(0.0f, 0.0f, 0.0f, 0.5f)); //layered below flash2
        private readonly Color flashColorRed2 = new Color(new Vector4(0.44f, 0.0f, 0.0f, 0.5f)); //layered above flash1
        private readonly Color colorPlayer = Color.White;

        private const float flashOnTime = initialStareTime + 5000.0f; 
        private const float flashOffTime = 0.0f; //ms
        private const float flashInitialDelay = 0.0f; //ms

        private readonly IParticle flash1;
        private readonly IParticle flash2;

        private IPlayer player;
        private ISprite deadLink;
        private Vector2 deadLinkPosition;

        public GameStateLosePhase1(Game1 game, IPlayer player)
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

            this.player = player;

            if (player.GetType() == typeof(Player1)) {
                deadLink = PlayerSpriteFactory.Instance.CreateDeadSprite();
            } else {
                deadLink = PlayerSpriteFactory.Instance.CreateZeldaDeadSprite();
            }
            
            deadLinkPosition = Vector2.Add(player.GetPlayerHitbox().Location.ToVector2(), new Vector2(playerXOffset, playerYOffset));

            flash1 = new Flash(flashColorRed1, 1, flashOnTime, flashOffTime, flashInitialDelay);
            flash2 = new Flash(flashColorRed2, 1, flashOnTime, flashOffTime, flashInitialDelay);

            const string deathAudio = "death";
            AudioManager.StopAllMusic();
            AudioManager.StopAllSound();
            AudioManager.PlayFireForget(deathAudio, initialStareTime / 1000);
        }

        public void Update(GameTime gameTime)
        {
            foreach (IController controller in controllerList)
            {
                controller.Update();
            }

            if (initialStareTimer > 0)
            {
                initialStareTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            else
            {
                colorRoom = colorRoomRed;
                flash1.Update(gameTime);
                flash2.Update(gameTime);
                game.SetState(new GameStateLosePhase2(game, flash1, flash2, player));
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

            spriteBatch.End();

            DrawUtil.DrawHUDOffset(game.HUD, spriteBatch, resolutionManager);

            drawMatrix.Translation = new Vector3(0, 0, 0);
        }
    }
}
