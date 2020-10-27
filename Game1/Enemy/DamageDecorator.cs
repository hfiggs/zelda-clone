using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Enemy
{
    class EnemyDamageDecorator : IEnemy
    {
        public int StunnedTimer { get; set; } = 0;

        private IEnemy original;
        private float damagedTimer;
        private Color[] flickers = { Color.LightBlue, Color.Orange, Color.Red };
        private int currentFlicker = 0;
        private float timeTillFlickerSwap;
        private Game1 game;

        Vector2 knockbackMagnitude = new Vector2(1, 1);
        public EnemyDamageDecorator( IEnemy Original, Vector2 direction, Game1 game)
        {
            this.original = Original;
            damagedTimer = 250f; //ms
            timeTillFlickerSwap = 50f;
            knockbackMagnitude = Vector2.Multiply(knockbackMagnitude, direction);
            this.game = game;
        }

        public void ReceiveDamage(float amount, Vector2 direction)
        {
            //For now enemies will be unable to be damaged after being struck for the short period of time that this is wrapping the enemy.
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            original.Draw(spriteBatch, flickers[currentFlicker]);
        }

        public void Update(GameTime gameTime, Rectangle drawingLimits)
        {
            Vector2 slideAmount = Vector2.Multiply(knockbackMagnitude, (float)gameTime.ElapsedGameTime.TotalMilliseconds);
            damagedTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            timeTillFlickerSwap -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if(damagedTimer > 200)
            {
                original.EditPosition(slideAmount);
            }
            if(timeTillFlickerSwap <= 0)
            {
                currentFlicker++;
                if (currentFlicker > 2)
                    currentFlicker = 0;
            }

            if(damagedTimer <= 0)
            {
                game.Screen.CurrentRoom.EnemyList.Add(original);
                game.Screen.CurrentRoom.EnemyList.Remove(this);
            }
            original.Update(gameTime, drawingLimits);
        }

        public void EditPosition(Vector2 amount)
        {
            original.EditPosition(amount);
        }

        public bool ShouldRemove()
        {
            return original.ShouldRemove();
        }

        public Rectangle GetHitbox()
        {
            return original.GetHitbox();
        }

        public void SetState(IEnemyState state)
        {
            original.SetState(state);
        }
    }
}
