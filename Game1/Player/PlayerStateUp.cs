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
        private float timeUntilNextFrame; // ms

        private const float animationTime = 150f; // ms per frame

        public PlayerStateUp(PlayerStateFactory stateFactory)
        {
            this.stateFactory = stateFactory;
            Sprite = PlayerSpriteFactory.Instance.CreateWalkUpSprite(false);

            isMoving = false;
            timeUntilNextFrame = animationTime;
        }

        public void Attack()
        {
            stateFactory.SetState(new PlayerStateUpAttack(stateFactory));
        }

        public void MoveDown()
        {
                stateFactory.SetState(new PlayerStateDown(stateFactory));
        }

        public void MoveLeft()
        {
                stateFactory.SetState(new PlayerStateLeft(stateFactory));
        }

        public void MoveRight()
        {
                stateFactory.SetState(new PlayerStateRight(stateFactory));
        }

        public void MoveUp()
        {
            isMoving = true;
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

            if(isMoving)
            {
                timeUntilNextFrame -= (float)time.ElapsedGameTime.TotalMilliseconds;

                if(timeUntilNextFrame <= 0)
                {
                    Sprite.Update();
                    timeUntilNextFrame += animationTime;
                }
            }

            isMoving = false;

        }
    }
}
