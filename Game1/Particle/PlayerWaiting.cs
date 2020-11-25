using Game1.Sprite;
using Game1.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Runtime.Serialization;

namespace Game1.Particle
{
    class PlayerWaiting : IParticle
    {
        private ISprite waiting;
        private ISprite arrow;
        private Vector2 position;
        private Vector2 arrowPosition;
        private Vector2 arrowOffsetN = new Vector2(19.0f, -23.0f);
        private Vector2 arrowOffsetE = new Vector2(67.0f, -5.0f);
        private Vector2 arrowOffsetS = new Vector2(19.0f, 12.0f);
        private Vector2 arrowOffsetW = new Vector2(-24.0f, -5.0f);

        private float timeUntilNextFrame; // ms
        private const float animationTime = 100.0f; // ms per frame
        private bool flash = true;

        private bool remove = false;        

        public PlayerWaiting(Vector2 position, int player, CompassDirection direction)
        {
            if (player == 2)
            {
                waiting = ParticleSpriteFactory.Instance.CreatePlayer2Waiting();                
            }
            else
            {
                waiting = ParticleSpriteFactory.Instance.CreatePlayer1Waiting();
            }

            switch(direction)
            {
                case CompassDirection.North:
                    arrow = ParticleSpriteFactory.Instance.CreateArrowWaitingN();
                    arrowPosition = position + arrowOffsetN;
                    break;
                case CompassDirection.East:
                    arrow = ParticleSpriteFactory.Instance.CreateArrowWaitingE();
                    arrowPosition = position + arrowOffsetE;
                    break;
                case CompassDirection.South:
                    arrow = ParticleSpriteFactory.Instance.CreateArrowWaitingS();
                    arrowPosition = position + arrowOffsetS;
                    break;
                case CompassDirection.West:
                    arrow = ParticleSpriteFactory.Instance.CreateArrowWaitingW();
                    arrowPosition = position + arrowOffsetW;
                    break;
            }

            this.position = position;

            timeUntilNextFrame = animationTime;
        }

        public void Update(GameTime gameTime)
        {
            timeUntilNextFrame -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timeUntilNextFrame <= 0)
            {
                waiting.Update();
                arrow.Update();
                timeUntilNextFrame += animationTime;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            if (flash)
            {
                waiting.Draw(spriteBatch, position, color, SpriteLayerUtil.topLayer);
                arrow.Draw(spriteBatch, arrowPosition, color, SpriteLayerUtil.topLayer);                
            }
            flash = !flash;
        }

        public void Remove()
        {
            remove = true;
        }

        public bool ShouldDelete()
        {
            return remove;
        }
    }
}
