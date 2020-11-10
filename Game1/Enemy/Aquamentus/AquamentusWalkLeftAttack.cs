using Game1.Projectile;
using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game1.Enemy
{
    class AquamentusWalkLeftAttack : IEnemyState
    {
        private Vector2 position;
        private int counter;
        private float totalTime;
        private const float timeOfAttack = 1;
        private const float moveSpeed = 7;
        Game1 game;
        IEnemy aquamentus;

        public ISprite Sprite { get; private set; }

        private float timeUntilNextFrame;
        private float animationTime = 150.0f;
        public AquamentusWalkLeftAttack(Game1 game, IEnemy aquamentus, Vector2 position) {
            Sprite = EnemySpriteFactory.Instance.CreateAttackAquamentusSprite();
            this.position = position;
            counter = 0;
            totalTime = 0;
            this.game = game;
            this.aquamentus = aquamentus;
            timeUntilNextFrame = animationTime;
        }

        public void Attack()
        {

        }


        public void Update(GameTime gametime, Rectangle drawingLimits)
        {
            const int maxTime = 2;
            totalTime += (float)gametime.ElapsedGameTime.TotalSeconds;
            Rectangle playerRect = game.Screen.GetPlayerRectangle();

            if (totalTime < timeOfAttack || (counter == 1 && totalTime < maxTime)) {
                if (drawingLimits.Contains(position.X - moveSpeed * (float)gametime.ElapsedGameTime.TotalSeconds, position.Y)) {
                    position.X -= moveSpeed * (float)gametime.ElapsedGameTime.TotalSeconds;
                }
            } else if(totalTime > timeOfAttack && counter == 0) {
                game.Screen.CurrentRoom.SpawnProjectile(new Fireballs(position, playerRect));
                counter++;
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
            Sprite.Draw(spriteBatch, position, color);
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public Vector2 GetDirection()
        {
            return new Vector2(-1 * moveSpeed, 0);
        }

        public List<Rectangle> GetHitboxes()
        {
            const int bodyWidth = 25, bodyHeight = 32;
            const int headWidth = 15, headHeight = 12;

            List<Rectangle> hitboxList = new List<Rectangle>();
            hitboxList.Add(new Rectangle((int)position.X, (int)position.Y, bodyWidth, bodyHeight));
            hitboxList.Add(new Rectangle((int)position.X, (int)position.Y, headWidth, headHeight));
            return hitboxList;
        }

        public void editPosition(Vector2 amount)
        {
            position = Vector2.Add(position, amount);
        }
    }
}
