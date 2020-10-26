using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Enemy
{
    public class Merchant : IEnemy
    {
        ISprite mySprite;
        Vector2 position;

        public Merchant(Vector2 position)
        {
            mySprite = EnemySpriteFactory.Instance.CreateMerchantSprite();
            this.position = position;
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            mySprite.Draw(spriteBatch, position, color);
        }

        public void ReceiveDamage(float amount, Vector2 direction)
        {
            // Do Nothing
        }

        public void Update(GameTime gameTime, Rectangle drawingLimits5)
        {
            //Do Nothing
        }
        public void SpawnAnimation()
        {
        }
        public void editPosition(Vector2 amount)
        {
            //Do Nothing
        }

        public void SetState(IEnemyState state)
        {
            //Do Nothing
        }

        public bool shouldRemove()
        {
            return false;
        }

        public Rectangle GetHitbox()
        {
            return new Rectangle((int)position.X, (int)position.Y, 14, 16);
        }
    }

}