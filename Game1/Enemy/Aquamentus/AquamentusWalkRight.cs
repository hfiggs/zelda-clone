using Game1.Sprite;
using Microsoft.Xna.Framework;
using System;

namespace Game1.Enemy.Aquamentus
{
    class AquamentusWalkRight : IEnemyState
    {
        
        private EnemyStateMachine stateMachine;
        private Vector2 position, currentPosition;
        private float totalTime;
        private int timeCap;
        Random random;
        private const float moveSpeed = 10;

        public ISprite Sprite { get; private set; }

        public AquamentusWalkRight(EnemyStateMachine stateMachine, Vector2 position) {
            Sprite = EnemySpriteFactory.Instance.CreateAquamentusSprite();
            this.stateMachine = stateMachine;
            this.position = position;
            currentPosition = position;
            random = new Random(Guid.NewGuid().GetHashCode());
            timeCap = random.Next(3);
            timeCap++;
            totalTime = 0;
        }

        public void Attack()
        {

        }

        public Vector2 GetPosition()
        {
            return currentPosition;
        }

        public void ReceiveDamage()
        {
            
        }

        public void Update(GameTime gametime, Rectangle drawingLimits)
        {
            totalTime += (float)gametime.ElapsedGameTime.TotalSeconds;
            
            if (totalTime <= timeCap) {
                position.X += moveSpeed * (float)gametime.ElapsedGameTime.TotalSeconds;
                Sprite.Update();
                if (random.Next(100) < 1) {
                    stateMachine.SetState(new AquamentusWalkRightAttack(stateMachine, position));
                }
            } else {
                Sprite.Update();
                stateMachine.SetState(new AquamentusWalkLeft(stateMachine, position));
            }
        }
        
    }
}
