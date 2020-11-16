using Game1.Audio;
using Game1.Controller;
using Game1.Enemy;
using Game1.HUD;
using Game1.Particle;
using Game1.ResolutionManager;
using Game1.RoomLoading;
using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game1.GameState
{
    class GameStateStart : IGameState
    {
        private readonly Game1 game;
        private readonly List<IController> controllerList;

        private readonly Color color = Color.White;

        private readonly ISprite background;
        private readonly Vector2 backgroundPosition = new Vector2(0, 0);

        private readonly List<IParticle> waterfallParticles;

        private readonly Vector2 waterfallSprayPosition = new Vector2(80, 160);
        private const int waterfallParticleOffset = -4;
        private const int waterfallParticleSpacing = 16;
        private const int numWaterfallParticles = 4;

        private bool isMusicStarted;

        private readonly IParticle curtain;

        public GameStateStart(Game1 game)
        {
            this.game = game;

            controllerList = new List<IController>
            {
                new KeyboardStartController(game),
                new GamepadStartController(game, PlayerIndex.One)
            };

            background = StartSpriteFactory.Instance.CreateStartBackgroundSprite();

            waterfallParticles = new List<IParticle>()
            {
                new WaterfallSpray(waterfallSprayPosition)
            };

            const int offset = 2;
            for(int i = 0; i < numWaterfallParticles; i++)
            {
                waterfallParticles.Add(new Waterfall(Vector2.Add(waterfallSprayPosition, new Vector2(0, (i + 1) * waterfallParticleSpacing + waterfallParticleOffset)), i == 0 ? 0 : offset));
            }

            isMusicStarted = false;

            game.Screen = new Screen(game);
            game.Screen.LoadAllRooms();

            game.HUD = new HUDInterface(game.Screen.Player.PlayerInventory, game.Screen);

            curtain = new Curtain(true, game);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, IResolutionManager resolutionManager)
        {
            game.GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, resolutionManager.GetResolutionMatrix());

            background.Draw(spriteBatch, backgroundPosition, color);

            waterfallParticles.ForEach(p => p.Draw(spriteBatch, color));

            curtain.Draw(spriteBatch, color);

            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            if (!isMusicStarted)
            {
                AudioManager.PlayLooped("title");
                isMusicStarted = true;
            }

            waterfallParticles.ForEach(p => p.Update(gameTime));

            foreach (IController controller in controllerList)
            {
                controller.Update();
            }

            curtain.Update(gameTime);
        }
    }
}
