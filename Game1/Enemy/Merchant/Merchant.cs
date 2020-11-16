using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game1.Enemy
{
    public class Merchant : IEnemy
    {
        public int StunnedTimer { get; set; } = 0;

        private ISprite mySprite;
        private Vector2 position;

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
        public void EditPosition(Vector2 amount)
        {
            //Do Nothing
        }

        public void SetState(IEnemyState state)
        {
            //Do Nothing
        }

        public bool ShouldRemove()
        {
            return false;
        }
        public Vector2 GetPosition()
        {
            return position;
        }
        public List<Rectangle> GetHitboxes()
        {
            List <Rectangle> hitboxList= new List<Rectangle>();
            const int width = 14;
            const int height = 16;

            hitboxList.Add(new Rectangle((int)position.X, (int)position.Y, width, height));
            return hitboxList;
        }
    }

}