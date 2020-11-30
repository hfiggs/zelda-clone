using Game1.Audio;
using Game1.Controller;
using Game1.Enemy;
using Game1.HUD;
using Game1.Particle;
using Game1.ResolutionManager;
using Game1.RoomLoading;
using Game1.Sprite;
using Game1.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Game1.GameState
{
    class GameStateStart : IGameState
    {
        private readonly Game1 game;
        private readonly List<IController> controllerList;

        private readonly Color color = Color.White;

        private readonly ISprite background;
        private readonly ISprite cursor;
        private const int optionsNum = 3;
        private readonly List<ISprite> optionList = new List<ISprite>(optionsNum);
        private readonly List<Vector2> optionPositions = new List<Vector2>(optionsNum);
        private readonly List<Color> optionColors = new List<Color>(optionsNum);
        private readonly Color optionHighlightColor = new Color(new Vector3(0.0f, 0.0f, 0.7373f));
        private readonly Vector2 optionPosition = new Vector2(52.0f, 148.0f);
        private readonly Vector2 optionPositionOffset = new Vector2(85.0f, 10.0f);
        private int cursorPosition = 0;
        private readonly Vector2 cursorOffset = new Vector2(-30.0f, -15.0f);
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
            cursor = StartSpriteFactory.Instance.CreateCursor();
            for (int i = 0; i < optionsNum; i++)
            {
                optionList.Add(StartSpriteFactory.Instance.CreateOption(i));
                optionPositions.Add(optionPosition);
                optionColors.Add(Color.Black);
                optionPosition += calculateNextOffset(i, optionPositionOffset);
            }
            optionColors[cursorPosition] = optionHighlightColor;

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
            
            // game.HUD = new HUDInterface(game.Screen.Player.PlayerInventory, game.Screen.Player2.PlayerInventory, game.Screen); TODO remove

            curtain = new Curtain(game, true);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, IResolutionManager resolutionManager)
        {
            game.GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, resolutionManager.GetResolutionMatrix());

            background.Draw(spriteBatch, backgroundPosition, color);

            cursor.Draw(spriteBatch, optionPositions[cursorPosition] + cursorOffset, Color.White, 1.0f);

            for (int i = 0; i < optionList.Count; i++)
            {
                optionList[i].Draw(spriteBatch, optionPositions[i], optionColors[i]);
            }

            waterfallParticles.ForEach(p => p.Draw(spriteBatch, color));

            curtain.Draw(spriteBatch, color);

            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            const string titleAudio = "title";

            if (!isMusicStarted)
            {
                AudioManager.PlayLooped(titleAudio);
                isMusicStarted = true;
            }

            waterfallParticles.ForEach(p => p.Update(gameTime));

            foreach (IController controller in controllerList)
            {
                controller.Update();
            }

            curtain.Update(gameTime);
            cursor.Update();
        }

        private Vector2 calculateNextOffset(int i, Vector2 offsetVector)
        {
            const float YFactor = -2.0f;
            float offsetX = offsetVector.X * (float)(Math.Pow(-1.0, (double)i));
            float offsetY = offsetVector.Y * (float)((Math.Pow(-1.0, (double)i)) - 1.0f) / YFactor;
            return new Vector2(offsetX, offsetY);
        }

        public void MoveCursor()
        {
            optionColors[cursorPosition] = Color.Black;
            cursorPosition = (cursorPosition + 1) % optionsNum;
            optionColors[cursorPosition] = optionHighlightColor;
        }

        public int GetOption()
        {
            return cursorPosition;
        }
    }
}
