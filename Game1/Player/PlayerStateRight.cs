/* Authors: 
 * Hunter Figgs
 * Jared Perkins
 */

using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Player
{
    class PlayerStateRight : IPlayerState
    {
        private PlayerStateFactory stateFactory;
        public ISprite Sprite { get; private set; }

        private bool isMoving;
        private Vector2 position;

        private float timeUntilNextFrame; // ms

        private const int moveSpeed = 5;
        private const float animationTime = 150f; // ms per frame

        public PlayerStateRight(PlayerStateFactory stateFactory, Vector2  position)
        {
            this.stateFactory = stateFactory;
            Sprite = PlayerSpriteFactory.Instance.CreateWalkRightSprite();

            isMoving = false;
            timeUntilNextFrame = animationTime;

            this.position = position;
        }

        public void Attack()
        {
            stateFactory.SetState(new PlayerStateRightAttack(stateFactory, position));
        }

        public void MoveDown()
        {
            if (!isMoving)
                stateFactory.SetState(new PlayerStateDown(stateFactory, position));
        }

        public void MoveLeft()
        {
            if (!isMoving)
                stateFactory.SetState(new PlayerStateLeft(stateFactory, position));
        }

        public void MoveRight()
        {
            isMoving = true;
        }

        public void MoveUp()
        {
            if (!isMoving)
                stateFactory.SetState(new PlayerStateUp(stateFactory, position));
        }

        public void UseItem()
        {
            stateFactory.SetState(new PlayerStateRightUse(stateFactory, position));
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

                position.X += moveSpeed;
            }

            isMoving = false;
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public char GetDirection()
        {
            return 'E';
        }
    }
}
