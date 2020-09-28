using Game1.Sprite;
using Microsoft.Xna.Framework;
using System;

namespace Game1.Enemy.SpikeTrap
{
    class SpikeTrapStateHome : IEnemyState
    {
        private EnemyStateMachine stateMachine;

        private Vector2 homePosition;
        private Vector2 currentPosition;

        public ISprite Sprite { get; private set; }

        public SpikeTrapStateHome(EnemyStateMachine stateMachine, Vector2 homePosition)
        {
            Sprite = EnemySpriteFactory.Instance.CreateSpikeTrapSprite();

            this.stateMachine = stateMachine;

            this.homePosition = homePosition;
            currentPosition = homePosition;
        }

        public void Attack()
        {
            // Cannot attack
        }

        public Vector2 GetPosition()
        {
            throw new NotImplementedException();
        }

        public void ReceiveDamage()
        {
            // Cannot receive damage
        }

        public void Update(GameTime gametime, Rectangle drawingLimits)
        {
            //
        }
    }
}
