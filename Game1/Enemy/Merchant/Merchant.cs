using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Enemy
{
    public class Merchant : IEnemy
    {
        ISprite mySprite;
        Vector2 position;

        public Merchant(SpriteBatch spriteBatch, Vector2 position)
        {
            mySprite = EnemySpriteFactory.Instance.CreateMerchantSprite();
            this.position = position;
        }

        public void Attack()
        {
            // Do Nothing
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            mySprite.Draw(spriteBatch, position, color);
        }

        public void ReceiveDamage()
        {
            // Do Nothing
        }

        public void Update(GameTime gameTime, Rectangle drawingLimits5)
        {
            //Do Nothing
        }
    }

}