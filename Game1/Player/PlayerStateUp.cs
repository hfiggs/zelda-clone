/* Author: Hunter Figgs */

using Game1.Sprite;
using Microsoft.Xna.Framework;

namespace Game1.Player
{
    class PlayerStateUp : IPlayerState
    {
        private PlayerStateFactory stateFactory;
        public ISprite Sprite { get; private set; }

        private bool isMoving;
        private bool isAttacking;
        private int frameCount;
        private float timeUntilNextFrame; // ms

        private const float animationTime = 150f; // ms per frame

        public PlayerStateUp(PlayerStateFactory stateFactory)
        {
            this.stateFactory = stateFactory;
            Sprite = PlayerSpriteFactory.Instance.CreateIdleUpSprite(false);

            isMoving = false;
            isAttacking = false;
            timeUntilNextFrame = animationTime;
        }

        public void Attack()
        {
            if (!isAttacking && !isMoving)
            {
                Sprite = PlayerSpriteFactory.Instance.CreateAttackUpSprite(false);
                isAttacking = true;
                frameCount = 4;
            }
        }

        public void MoveDown()
        {
            if(!isAttacking)
                stateFactory.SetState(new PlayerStateDown(stateFactory));
        }

        public void MoveLeft()
        {
            if(!isAttacking)
                stateFactory.SetState(new PlayerStateLeft(stateFactory));
        }

        public void MoveRight()
        {
            if(!isAttacking)
                stateFactory.SetState(new PlayerStateRight(stateFactory));
        }

        public void MoveUp()
        {
            if (!isAttacking && !isMoving)
            {
                Sprite = PlayerSpriteFactory.Instance.CreateWalkUpSprite(false);
                isMoving = true;
                frameCount = 2;
            }
        }

        public void ReceiveDamage()
        {
            throw new System.NotImplementedException();
        }
        public void UseItem()
        {
            throw new System.NotImplementedException();
        }
        public void Update(GameTime time)
        {

            if(isMoving || isAttacking)
            {
                timeUntilNextFrame -= (float)time.ElapsedGameTime.TotalMilliseconds;

                if(timeUntilNextFrame <= 0)
                {
                    Sprite.Update();
                    timeUntilNextFrame += animationTime;

                    frameCount--;

                    if (frameCount <= 0)
                    {
                        Sprite = PlayerSpriteFactory.Instance.CreateIdleUpSprite(false);
                        isMoving = false;
                        isAttacking = false;
                    }
                }
            }

        }
    }
}
