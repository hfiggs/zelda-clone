/* Author: Hunter Figgs */

using Game1.Sprite;
using Microsoft.Xna.Framework;
using System;

namespace Game1.Player
{
    class PlayerStateLeftUse : IPlayerState
    {
        private PlayerStateFactory stateFactory;
        public ISprite Sprite { get; private set; }

        private Vector2 position;

        private float timeUntilNextFrame; // ms
        private int frameCount;
        private Color color = Color.White;
        private const float animationTime = 150f; // ms per frame
        private const int animationFrames = 3;

        public PlayerStateLeftUse(PlayerStateFactory stateFactory, Vector2 position)
        {
            this.stateFactory = stateFactory;
            Sprite = PlayerSpriteFactory.Instance.CreateUseItemLeftSprite(color);

            this.position = position;

            frameCount = 0;
            timeUntilNextFrame = animationTime;
        }

        public void Attack()
        {
            // Do nothing
        }

        public void MoveDown()
        {
            // Do nothing
        }

        public void MoveLeft()
        {
            // Do nothing
        }

        public void MoveRight()
        {
            // Do nothing
        }

        public void MoveUp()
        {
            // Do nothing
        }

        public void ReceiveDamage()
        {
            // Do nothing
        }
        public void UseItem()
        {
            // Do nothing
        }
        public void Update(GameTime time)
        {
            timeUntilNextFrame -= (float)time.ElapsedGameTime.TotalMilliseconds;

            if(timeUntilNextFrame <= 0 && frameCount < animationFrames)
            {
                Sprite.Update();
                timeUntilNextFrame += animationTime;
                frameCount++;
            }
            else if(frameCount == animationFrames)
            {
                stateFactory.SetState(new PlayerStateLeft(stateFactory, position));
            }
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public char GetDirection()
        {
            return 'W';
        }
    }
}
