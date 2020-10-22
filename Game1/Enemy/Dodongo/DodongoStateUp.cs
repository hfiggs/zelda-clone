using Game1.Sprite;
using Microsoft.Xna.Framework;
using System;

namespace Game1.Enemy
{
    class DodongoStateUp : IEnemyState
    {
        private EnemyStateMachine stateMachine;

        private Vector2 position;

        private bool isDead;

        public ISprite Sprite { get; private set; }

        private float timeUntilNextFrame; // ms
        private const float animationTime = 150f; // ms per frame

        private float timeUntilNewDirection;
        private const float moveTime = 1000f; // ms

        private const int moveSpeed = 1;

        public DodongoStateUp(EnemyStateMachine stateMachine, Vector2 position)
        {
            Sprite = EnemySpriteFactory.Instance.CreateDodongoUpSprite();

            this.stateMachine = stateMachine;

            this.position = position;

            isDead = false;

            timeUntilNextFrame = animationTime;

            timeUntilNewDirection = moveTime;
        }

        public void Attack()
        {
            // Cannot attack
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public Vector2 GetDirection()
        {
            return new Vector2(0,-1);
        }

        public Rectangle GetHitbox()
        {
            return new Rectangle((int)position.X + 9, (int)position.Y + 8, 15, 16);
        }

        public void Update(GameTime gametime, Rectangle drawingLimits)
        {
            if (!isDead)
            {
                position.Y -= moveSpeed;

                // Sprite updating
                timeUntilNextFrame -= (float)gametime.ElapsedGameTime.TotalMilliseconds;

                if (timeUntilNextFrame <= 0)
                {
                    Sprite.Update();
                    timeUntilNextFrame += animationTime;
                }

                // State updating
                timeUntilNewDirection -= (float)gametime.ElapsedGameTime.TotalMilliseconds;

                if (timeUntilNewDirection <= 0)
                {
                    switch((new Random()).Next(4))
                    {
                        case 0:
                            timeUntilNewDirection += moveTime;
                            break;
                        case 1:
                            stateMachine.SetState(new DodongoStateDown(stateMachine, position));
                            break;
                        case 2:
                            stateMachine.SetState(new DodongoStateLeft(stateMachine, position));
                            break;
                        case 3:
                            stateMachine.SetState(new DodongoStateRight(stateMachine, position));
                            break;
                    }
                }

                // TODO: Determine if dodongo will swallow a 2nd bomb, if so set isDead = true
            }
            else
            {
                Sprite = EnemySpriteFactory.Instance.CreateDodongoUpDeadSprite();
            }
        }

        public void editPosition(Vector2 amount)
        {
            position = Vector2.Add(position, amount);
        }
    }
}
