using Game1.Sprite;
using Game1.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Runtime.Serialization;

namespace Game1.Particle
{
    class Flash : IParticle
    {
        private ISprite sprite;
        private Vector2 position = new Vector2(0.0f, 0.0f);

        private bool flash = false;

        private float timeCounter = 0; // ms

        private int flashNum; //max number of flashes
        private int flashesCount = 0;
        private float onTime; //ms
        private float offTime; //ms
        private float initDelay; //ms - time before the first flash
        private bool init = false; //prevents init delay from over flashing

        private bool remove = false;

        public Flash(Color color, int flashNum, float onTime, float offTime, float initDelay)
        {
            sprite = ParticleSpriteFactory.Instance.CreateFlashOverlay(color);

            this.flashNum = flashNum;
            this.onTime = onTime;
            this.offTime = offTime;
            this.initDelay = initDelay;
        }

        public void Update(GameTime gameTime)
        {
            if (flashesCount < flashNum)
            {
                timeCounter += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            } else
            {
                remove = true;
            }

            if (!init && timeCounter > initDelay)
            {
                //turn on initially
                flash = true;
                init = true;
            }
            else if (timeCounter > (initDelay + onTime * (flashesCount + 1) + (offTime * (flashesCount))))
            {
                //turn off
                flash = false;
                flashesCount++;
            }
            else if (timeCounter > (initDelay + (onTime * flashesCount) + (offTime * flashesCount)))
            {
                //turn on
                flash = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            if (flash && !remove)
            {
                sprite.Draw(spriteBatch, position, color, SpriteLayerUtil.particleLayer);
            }
        }

        public bool ShouldDelete()
        {
            return remove;
        }
    }
}
