/* Author: Hunter Figgs */

using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Player
{
    class PlayerStateUp : IPlayerState
    {
        private PlayerStateFactory stateFactory;
        public ISprite Sprite { get; private set; }

        private bool isMoving;
        private int frameCount;
        private Vector2 position;
        private bool isAttacking;

        private float timeUntilNextFrame; // ms
        private int moveSpeed = 15;
        private const float animationTime = 100f; // ms per frame

        public PlayerStateUp(PlayerStateFactory stateFactory, Vector2 position)
        {
            this.stateFactory = stateFactory;
            this.position = position;
            Sprite = PlayerSpriteFactory.Instance.CreateIdleUpSprite(false);

            isMoving = false;
            isAttacking = false;
            timeUntilNextFrame = animationTime;
        }

        public void Attack()
        {
            if (!isMoving && !isAttacking)
            {
                Sprite = PlayerSpriteFactory.Instance.CreateAttackUpSprite(false);
                isAttacking = true;
                frameCount = 4;
            }
        }

        public void MoveDown()
        {
            if (!isMoving && !isAttacking)
                stateFactory.SetState(new PlayerStateDown(stateFactory, position));
        }

        public void MoveLeft()
        {
            if (!isMoving && !isAttacking)
                stateFactory.SetState(new PlayerStateLeft(stateFactory, position));
        }

        public void MoveRight()
        {
            if (!isMoving && !isAttacking)
                stateFactory.SetState(new PlayerStateRight(stateFactory, position));
        }

        public void MoveUp()
        {
            if (!isMoving && !isAttacking)
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
                    if(isMoving)
                        position.Y -= moveSpeed;
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

        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, position);
        }
    }
}
