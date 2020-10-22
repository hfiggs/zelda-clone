using Game1.Sprite;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Enemy
{
    class AquamentusWalkLeft : IEnemyState
    {
        private Vector2 position;
        private float totalTime;
        private int timeCap;
        private Random random;
        private const float moveSpeed = 10;
        private Game1 game;
        private IEnemy aquamentus;

        public ISprite Sprite { get; private set; }

        private float timeUntilNextFrame;
        private float animationTime = 150.0f;

        public AquamentusWalkLeft(Game1 game, IEnemy aquamentus, Vector2 position) {
            Sprite = EnemySpriteFactory.Instance.CreateAquamentusSprite();
            this.position = position;
            random = new Random(Guid.NewGuid().GetHashCode());
            timeCap = random.Next(3);
            timeCap++;
            totalTime = 0;
            this.game = game;
            this.aquamentus = aquamentus;
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
                if (drawingLimits.Contains(position.X - moveSpeed * (float)gametime.ElapsedGameTime.TotalSeconds, position.Y)) {
                    position.X -= moveSpeed * (float)gametime.ElapsedGameTime.TotalSeconds;
                }
                if (random.Next(100) < 1) {
                    aquamentus.SetState(new AquamentusWalkLeftAttack(game, aquamentus, position));
                }
            } else {
                aquamentus.SetState(new AquamentusWalkRight(game, aquamentus, position));
            }
            
            timeUntilNextFrame -= (float)gametime.ElapsedGameTime.TotalMilliseconds;

            if (timeUntilNextFrame <= 0)
            {
                Sprite.Update();
                timeUntilNextFrame += animationTime;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            Sprite.Draw(spriteBatch, position, Color.White);
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public Vector2 GetDirection()
        {
            return new Vector2(-1 * moveSpeed, 0);
        }

        public Rectangle GetHitbox()
        {
            return new Rectangle((int)position.X, (int)position.Y, 16, 13);
        }
    }
}
