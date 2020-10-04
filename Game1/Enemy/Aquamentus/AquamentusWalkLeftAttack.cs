using Game1.Projectile;
using Game1.Sprite;
using Microsoft.Xna.Framework;

namespace Game1.Enemy
{
    class AquamentusWalkLeftAttack : IEnemyState
    {
        private EnemyStateMachine stateMachine;
        private Vector2 position;
        private int counter;
        private float totalTime;
        private const float timeOfAttack = 1;
        private const float moveSpeed = 10;
        Game1 game;

        public ISprite Sprite { get; private set; }

        public AquamentusWalkLeftAttack(Game1 game, EnemyStateMachine stateMachine, Vector2 position) {
            Sprite = EnemySpriteFactory.Instance.CreateAttackAquamentusSprite();
            this.stateMachine = stateMachine;
            this.position = position;
            counter = 0;
            totalTime = 0;
            this.game = game;
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
            Rectangle playerRect = stateMachine.GetPlayerRectangle();

            if (totalTime < timeOfAttack || (counter == 1 && totalTime < 2)) {
                position.X -= moveSpeed * (float)gametime.ElapsedGameTime.TotalSeconds;
                Sprite.Update();
            } else if(totalTime > timeOfAttack && counter == 0) {
                game.SpawnProjectile(new Fireballs(position, playerRect));
                Sprite.Update();
                counter++;
            } else {
                Sprite.Update();
                stateMachine.SetState(new AquamentusWalkRight(game, stateMachine, position));
            }
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public Vector2 GetDirection()
        {
            return new Vector2(-1 * moveSpeed, 0);
        }
    }
}
