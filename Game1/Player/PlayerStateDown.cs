/* Author: Hunter Figgs */

using Game1.Sprite;
using Microsoft.Xna.Framework;

namespace Game1.Player
{
    class PlayerStateDown : IPlayerState
    {
        private PlayerStateFactory stateFactory;
        public ISprite Sprite { get; private set; }

        private bool isMoving;
        private float timeUntilNextFrame; // ms

        private const float animationTime = 150f; // ms per frame

        public PlayerStateDown(PlayerStateFactory stateFactory)
        {
            this.stateFactory = stateFactory;
            Sprite = PlayerSpriteFactory.Instance.CreateWalkDownSprite(false);

            isMoving = false;
            timeUntilNextFrame = animationTime;
        }

        public void Attack()
        {
            throw new System.NotImplementedException();
        }

        public void MoveDown()
        {
            isMoving = true;
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
            stateFactory.SetState(new PlayerStateUp(stateFactory));
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
