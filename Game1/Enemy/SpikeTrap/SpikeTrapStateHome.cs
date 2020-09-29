using Game1.Sprite;
using Microsoft.Xna.Framework;
using System;

namespace Game1.Enemy.SpikeTrap
{
    class SpikeTrapStateHome : IEnemyState
    {
        private EnemyStateMachine stateMachine;

        private Vector2 homePosition;

        private int verticalRange;
        private int horizontalRange;

        // The width of the imaginary cross where the spike trap can detect the player
        private const int viewWidth = 20;

        public ISprite Sprite { get; private set; }

        public SpikeTrapStateHome(EnemyStateMachine stateMachine, Vector2 homePosition, int verticalRange, int horizontalRange)
        {
            Sprite = EnemySpriteFactory.Instance.CreateSpikeTrapSprite();

            this.stateMachine = stateMachine;

            this.homePosition = homePosition;

            this.verticalRange = verticalRange;
            this.horizontalRange = horizontalRange;
        }

        public void Attack()
        {
            // Cannot attack
        }

        public Vector2 GetPosition()
        {
            return homePosition;
        }

        public Vector2 GetDirection()
        {
            return new Vector2(0, 0);
            //No direction field
        }

        public void ReceiveDamage()
        {
            // Cannot receive damage
        }

        public void Update(GameTime gametime, Rectangle drawingLimits)
        {
            Rectangle playerRect = stateMachine.GetPlayerRectangle();

            Vector2 windowDims = stateMachine.GetWindowDimensions();

            
            if(playerRect.Intersects(new Rectangle((int)(homePosition.X - windowDims.X), (int)homePosition.Y, (int)windowDims.X, viewWidth))) // Spike sees player west
            {
                stateMachine.SetState(new SpikeTrapStateAttackWest(stateMachine, homePosition, verticalRange, horizontalRange));
            }
            else if(playerRect.Intersects(new Rectangle((int)homePosition.X, (int)homePosition.Y, (int)windowDims.X, viewWidth))) // Spike sees player east
            {
                stateMachine.SetState(new SpikeTrapStateAttackEast(stateMachine, homePosition, verticalRange, horizontalRange));
            }
            else if(playerRect.Intersects(new Rectangle((int)homePosition.X, (int)(homePosition.Y - windowDims.Y), viewWidth, (int)windowDims.Y))) // Spike sees player north
            {
                stateMachine.SetState(new SpikeTrapStateAttackNorth(stateMachine, homePosition, verticalRange, horizontalRange));
            }
            else if (playerRect.Intersects(new Rectangle((int)homePosition.X, (int)homePosition.Y, viewWidth, (int)windowDims.Y))) // Spike sees player south
            {
                stateMachine.SetState(new SpikeTrapStateAttackSouth(stateMachine, homePosition, verticalRange, horizontalRange));
            }
        }
    }
}
