using Game1.Projectile;
using Game1.Sprite;
using Microsoft.Xna.Framework;

namespace Game1.Enemy
{
    class AquamentusWalkRightAttack : IEnemyState
    {
        private EnemyStateMachine stateMachine;
        private Vector2 position;
        private int counter;
        private float totalTime;
        private const float timeOfAttack = 1;
        private const float moveSpeed = 10;
        Game1 game;

        public ISprite Sprite { get; private set; }
        private float timeUntilNextFrame;
        private float animationTime = 150.0f;

        public AquamentusWalkRightAttack(Game1 game, EnemyStateMachine stateMachine, Vector2 position) {
            Sprite = EnemySpriteFactory.Instance.CreateAttackAquamentusSprite();
            this.stateMachine = stateMachine;
            this.position = position;
            counter = 0;
            totalTime = 0;
            this.game = game;
            timeUntilNextFrame = animationTime;
        }

        public void Attack()
        {

        }

        public void Update(GameTime gametime, Rectangle drawingLimits) {
            totalTime += (float)gametime.ElapsedGameTime.TotalSeconds;
            Rectangle playerRect = stateMachine.GetPlayerRectangle();

            if (totalTime < timeOfAttack || (counter == 1 && totalTime < 2)) {
                position.X += moveSpeed * (float)gametime.ElapsedGameTime.TotalSeconds;
            } else if (totalTime > timeOfAttack && counter == 0) {
                game.SpawnProjectile(new Fireballs(position, playerRect));
                counter++;
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

        public void editPosition(Vector2 amount)
        {
            position = Vector2.Add(position, amount);
        }
    }
}
