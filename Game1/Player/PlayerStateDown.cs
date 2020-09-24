/* Authors: 
 * Hunter Figgs
 * Jared Perkins
 */

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
        private Vector2 position;

        private float timeUntilNextFrame; // ms

        private const int moveSpeed = 5;
        private const float animationTime = 150f; // ms per frame

        public PlayerStateDown(PlayerStateFactory stateFactory, Vector2 position)
        {
            this.stateFactory = stateFactory;
            Sprite = PlayerSpriteFactory.Instance.CreateWalkDownSprite(false);

            isMoving = false;
            timeUntilNextFrame = animationTime;

            this.position = position;
        }

        public void Attack()
        {
            stateFactory.SetState(new PlayerStateDownAttack(stateFactory, position));
        }

        public void MoveDown()
        {
            isMoving = true;
        }

        public void MoveLeft()
        {
            if (!isMoving)
                stateFactory.SetState(new PlayerStateLeft(stateFactory, position));
        }

        public void MoveRight()
        {
            if (!isMoving)
                stateFactory.SetState(new PlayerStateRight(stateFactory, position));
        }

        public void MoveUp()
        {
            if (!isMoving)
                stateFactory.SetState(new PlayerStateUp(stateFactory, position));
        }

        public void ReceiveDamage()
        {
            throw new System.NotImplementedException();
        }

        public void UseItem()
        {
            stateFactory.SetState(new PlayerStateDownUse(stateFactory, position));
        }

        public void Update(GameTime time)
        {
            if (isMoving)
            {
                timeUntilNextFrame -= (float)time.ElapsedGameTime.TotalMilliseconds;

                if (timeUntilNextFrame <= 0)
                {
                    Sprite.Update();
                    timeUntilNextFrame += animationTime;
                }

                position.Y += moveSpeed;
            }

            isMoving = false;
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public char GetDirection()
        {
            return 'S';
        }
    }
}
