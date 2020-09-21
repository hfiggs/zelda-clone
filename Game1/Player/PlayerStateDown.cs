/* Author: Hunter Figgs */

using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Player
{
    class PlayerStateDown : IPlayerState
    {
        private PlayerStateFactory stateFactory;
        public ISprite Sprite { get; private set; }

        private bool isMoving;
        private bool isAttacking;
        private Vector2 position;
        private int frameCount;

        private float timeUntilNextFrame; // ms
        private int moveSpeed = 15d;
        private const float animationTime = 100f; // ms per frame

        public PlayerStateDown(PlayerStateFactory stateFactory, Vector2 position)
        {
            this.stateFactory = stateFactory;
            Sprite = PlayerSpriteFactory.Instance.CreateIdleDownSprite(false);
            this.position = position;

            isMoving = false;
            isAttacking = false;
            timeUntilNextFrame = animationTime;
        }

        public void Attack()
        {
            if(!isMoving && !isAttacking)
            {
                Sprite = PlayerSpriteFactory.Instance.CreateAttackDownSprite(false);
                frameCount = 4;
                isAttacking = true;
            }
        }

        public void MoveDown()
        {
            if (!isMoving && !isAttacking)
            {
                Sprite = PlayerSpriteFactory.Instance.CreateWalkDownSprite(false);
                frameCount = 2;
                isMoving = true;
            }
        }

        public void MoveLeft()
        {
            if (!isMoving && !isAttacking)
                stateFactory.SetState(new PlayerStateLeft(stateFactory,position));
        }

        public void MoveRight()
        {
            if (!isMoving && !isAttacking)
                stateFactory.SetState(new PlayerStateRight(stateFactory,position));
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
                    frameCount--;
                    if(isMoving)
                        position.Y += moveSpeed;
                    
                    if(frameCount <= 0)
                    {
                        Sprite = PlayerSpriteFactory.Instance.CreateIdleDownSprite(false);
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
