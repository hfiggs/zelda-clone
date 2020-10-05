using Game1.Sprite;
using Microsoft.Xna.Framework;
using System;

namespace Game1.Enemy
{
    class AquamentusWalkRight : IEnemyState
    {
        private EnemyStateMachine stateMachine;
        private Vector2 position;
        private float totalTime;
        private int timeCap;
        Random random;
        private const float moveSpeed = 10;
        Game1 game;

        public ISprite Sprite { get; private set; }

        private float timeUntilNextFrame;
        private float animationTime = 150.0f;

        public AquamentusWalkRight(Game1 game, EnemyStateMachine stateMachine, Vector2 position) {
            Sprite = EnemySpriteFactory.Instance.CreateAquamentusSprite();
            this.stateMachine = stateMachine;
            this.position = position;
            random = new Random(Guid.NewGuid().GetHashCode());
            timeCap = random.Next(3);
            timeCap++;
            totalTime = 0;
            this.game = game;
            timeUntilNextFrame = animationTime;
        }

        public void Attack()
        {

        }

        public void ReceiveDamage()
        {
            
        }

        public void Update(GameTime gametime, Rectangle drawingLimits)
        {
            totalTime += (float)gametime.ElapsedGameTime.TotalSeconds;
            
            if (totalTime <= timeCap) {
                position.X += moveSpeed * (float)gametime.ElapsedGameTime.TotalSeconds;
                if (random.Next(100) < 1) {
                    stateMachine.SetState(new AquamentusWalkRightAttack(game, stateMachine, position));
                }
            } else {
                stateMachine.SetState(new AquamentusWalkLeft(game, stateMachine, position));
            }

            timeUntilNextFrame -= (float)gametime.ElapsedGameTime.TotalMilliseconds;

            if (timeUntilNextFrame <= 0)
            {
                Sprite.Update();
                timeUntilNextFrame += animationTime;
            }
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public Vector2 GetDirection()
        {
            return new Vector2(moveSpeed, 0);
        }
    }
}
