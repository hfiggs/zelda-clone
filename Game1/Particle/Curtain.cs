using Game1.Sprite;
using Game1.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Runtime.Serialization;

namespace Game1.Particle
{
    class Curtain : IParticle
    {
        private ISprite curtainLeftSprite;
        private ISprite curtainRightSprite;
        private Rectangle curtainLeftArea;
        private Rectangle curtainRightArea;

        private float timeCounter = 75f; // ms
        private const float stepTime = 75f; // ms
        private const int stepSize = 7; //pixels

        private readonly bool opening;

        private bool update = true;
        private bool remove = false;

        private Vector2 screenDimensions;

        public Curtain(Game1 game, bool opening)
        {
            screenDimensions = game.GetWindowDimensionsScaled();
            const int divideBy2 = 2;
            curtainLeftArea = opening ? new Rectangle(0, 0, (int)screenDimensions.X / divideBy2, (int)screenDimensions.Y) : new Rectangle(-(int)screenDimensions.X, 0, (int)screenDimensions.X, (int)screenDimensions.Y);
            curtainRightArea = opening ? new Rectangle((int)screenDimensions.X / divideBy2, 0, (int)screenDimensions.X / divideBy2, (int)screenDimensions.Y) : new Rectangle((int)screenDimensions.X, 0, (int)screenDimensions.X, (int)screenDimensions.Y);
            curtainLeftSprite = ParticleSpriteFactory.Instance.CreateCurtain(Color.Black, curtainLeftArea);
            curtainRightSprite = ParticleSpriteFactory.Instance.CreateCurtain(Color.Black, curtainRightArea);
            this.opening = opening;
        }

        public void Update(GameTime gameTime)
        {
            if (update)
            {
                if ((timeCounter -= (float)gameTime.ElapsedGameTime.TotalMilliseconds) <= 0.0f)
                {
                    timeCounter = stepTime;
                    if (opening)
                    {
                        curtainLeftArea.X -= stepSize;
                        curtainRightArea.X += stepSize;
                        if (curtainRightArea.X > screenDimensions.X)
                            update = false;
                    }
                    else
                    {
                        curtainLeftArea.X += stepSize;
                        curtainRightArea.X -= stepSize;
                        if (curtainLeftArea.X > 0)
                            update = false;
                    }

                    curtainLeftSprite = ParticleSpriteFactory.Instance.CreateCurtain(Color.Black, curtainLeftArea);
                    curtainRightSprite = ParticleSpriteFactory.Instance.CreateCurtain(Color.Black, curtainRightArea);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
                curtainLeftSprite.Draw(spriteBatch, curtainLeftArea.Location.ToVector2(), color, SpriteLayerUtil.particleLayer);
                curtainRightSprite.Draw(spriteBatch, curtainRightArea.Location.ToVector2(), color, SpriteLayerUtil.particleLayer);
        }

        public bool ShouldDelete()
        {
            if(opening && !update)
                return true;
            if (!opening && remove && !update)
                return true;
            return false;
        }

        //used only for when the curtains are closing
        public void BeginDespawn()
        {
            remove = true;
        }
    }
}
