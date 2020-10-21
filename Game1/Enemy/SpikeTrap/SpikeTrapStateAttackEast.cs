using Game1.Sprite;
using Microsoft.Xna.Framework;
using System;

namespace Game1.Enemy
{
    class SpikeTrapStateAttackEast : IEnemyState
    {
        private EnemyStateMachine stateMachine;

        private Vector2 homePosition;
        private Vector2 currentPosition;

        private int verticalRange;
        private int horizontalRange;

        private const int advanceSpeed = 5;
        private const int retreatSpeed = 2;

        private bool isAdvancing;

        public ISprite Sprite { get; private set; }

        public SpikeTrapStateAttackEast(EnemyStateMachine stateMachine, Vector2 homePosition, int verticalRange, int horizontalRange)
        {
            Sprite = EnemySpriteFactory.Instance.CreateSpikeTrapSprite();

            this.stateMachine = stateMachine;

            this.homePosition = homePosition;
            currentPosition = homePosition;

            this.verticalRange = verticalRange;
            this.horizontalRange = horizontalRange;

            isAdvancing = true;
        }

        public void Attack()
        {
            // Cannot attack
        }

        public Vector2 GetPosition()
        {
            return currentPosition;
        }

        public Vector2 GetDirection()
        {
            return new Vector2(1,0);
        }

        public void Update(GameTime gametime, Rectangle drawingLimits)
        {
            if(isAdvancing)
            {
                currentPosition.X += advanceSpeed;

                if (currentPosition.X >= (homePosition.X + horizontalRange))
                {
                    isAdvancing = false;
                }
            }
            else
            {
                currentPosition.X -= retreatSpeed;

                if (currentPosition.X <= homePosition.X)
                {
                    stateMachine.SetState(new SpikeTrapStateHome(stateMachine, homePosition, verticalRange, horizontalRange));
                }
            }
        }

        public void editPosition(Vector2 amount)
        {
            //can't be edited, doesn't need to be move on
        }
    }
}
