/* Author: Hunter Figgs */

using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Player
{
    class PlayerStateRight : IPlayerState
    {
        private PlayerStateFactory stateFactory;
        public ISprite Sprite { get; private set; }

        private int frameCount;
        private bool isMoving;
        private bool isAttacking;
        private Vector2 position;

        private float timeUntilNextFrame; // ms
        private int moveSpeed = 15;
        private const float animationTime = 100f; // ms per frame

        public PlayerStateRight(PlayerStateFactory stateFactory, Vector2  position)
        {
            this.stateFactory = stateFactory;
            Sprite = PlayerSpriteFactory.Instance.CreateIdleRightSprite(false);

            this.position = position;
            isAttacking = false;
            isMoving = false;
            timeUntilNextFrame = animationTime;
        }

        public void Attack()
        {
            if (!isMoving && !isAttacking)
            {
                Sprite = PlayerSpriteFactory.Instance.CreateAttackRightSprite(false);
                frameCount = 4;
                isAttacking = true;
            }
        }

        public void MoveDown()
        {
            if (!isMoving && !isAttacking)
                stateFactory.SetState(new PlayerStateDown(stateFactory,position));
        }

        public void MoveLeft()
        {
            if (!isMoving && !isAttacking)
                stateFactory.SetState(new PlayerStateLeft(stateFactory,position));
        }

        public void MoveRight()
        {
            if (!isMoving && !isAttacking)
            {
                Sprite = PlayerSpriteFactory.Instance.CreateWalkRightSprite(false);
                frameCount = 2;
                isMoving = true;
            }
        }

        public void MoveUp()
        {
            if (!isMoving && !isAttacking)
                stateFactory.SetState(new PlayerStateUp(stateFactory,position));
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

                    if(isMoving)
                        position.X += moveSpeed;
                    frameCount--;


                    if (frameCount <= 0)
                    {
                        Sprite = PlayerSpriteFactory.Instance.CreateIdleRightSprite(false);
                        isMoving = false;
                        isAttacking = false;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, position);
        }
    }
}
