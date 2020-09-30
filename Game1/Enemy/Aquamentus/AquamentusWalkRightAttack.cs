using Game1.Sprite;
using Microsoft.Xna.Framework;

namespace Game1.Enemy.Aquamentus
{
    class AquamentusWalkRightAttack : IEnemyState
    {

        private EnemyStateMachine stateMachine;
        private Vector2 position, currentPosition;
        private int counter;
        private float totalTime;
        private const float timeOfAttack = 1;
        private const float moveSpeed = 10;

        public ISprite Sprite { get; private set; }

        public AquamentusWalkRightAttack(EnemyStateMachine stateMachine, Vector2 position) {
            Sprite = EnemySpriteFactory.Instance.CreateAttackAquamentusSprite();
            this.stateMachine = stateMachine;
            this.position = position;
            currentPosition = position;
            counter = 0;
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
            Rectangle playerRect = stateMachine.GetPlayerRectangle();

            if (totalTime < timeOfAttack || (counter == 1 && totalTime < 2)) {
                position.X += moveSpeed * (float)gametime.ElapsedGameTime.TotalSeconds;
                Sprite.Update();
            } else if (totalTime > timeOfAttack && counter == 0) {
                // Create Projectile in this case
                Sprite.Update();
                counter++;
            } else {
                Sprite.Update();
                stateMachine.SetState(new AquamentusWalkLeft(stateMachine, position));
            }
        }
    }
}

