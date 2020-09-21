/* Author: Hunter Figgs */

using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Player
{
    class PlayerStateLeft : IPlayerState
    {
        private PlayerStateFactory stateFactory;
        public ISprite Sprite { get; private set; }

        private bool isMoving;
        private bool isAttacking;
        private int frameCount;
        private Vector2 position;

        private float timeUntilNextFrame; // ms
        private int moveSpeed = 15;
        private const float animationTime = 100f; // ms per frame

        public PlayerStateLeft(PlayerStateFactory stateFactory, Vector2 position)
        {
            this.stateFactory = stateFactory;
            Sprite = PlayerSpriteFactory.Instance.CreateIdleLeftSprite(false);
            this.position = position;

            isAttacking = false;
            isMoving = false;
            timeUntilNextFrame = animationTime;
        }

        public void Attack()
        {
            if(!isMoving && !isAttacking)
            {
                Sprite = PlayerSpriteFactory.Instance.CreateAttackLeftSprite(false);
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
            {
                Sprite = PlayerSpriteFactory.Instance.CreateWalkLeftSprite(false);
                frameCount = 2;
                isMoving = true;
            }
        }

        public void MoveRight()
        {
            if (!isMoving && !isAttacking)
                stateFactory.SetState(new PlayerStateRight(stateFactory, position));
        }

        public void MoveUp()
        {
            if (!isMoving && !isAttacking)
                stateFactory.SetState(new PlayerStateUp(stateFactory, position));
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
                        position.X -= moveSpeed;

                    if(frameCount <= 0)
                    {
                        Sprite = PlayerSpriteFactory.Instance.CreateIdleLeftSprite(false);
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
