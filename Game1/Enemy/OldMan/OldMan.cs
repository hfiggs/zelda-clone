using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Enemy
{
    class OldMan : IEnemy
    {
        ISprite sprite;

        Vector2 position;

        public OldMan(Vector2 position)
        {
            sprite = EnemySpriteFactory.Instance.CreateOldManSprite();

            this.position = position;
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            sprite.Draw(spriteBatch, position, color);
        }

        public void ReceiveDamage()
        {
            // Cannot receive damage
        }

        public void Update(GameTime gameTime, Rectangle drawingLimits5)
        {
            // TODO: Logic for determining text and when to fade out sprite
        }

        public void SetState(IEnemyState state)
        {
            //Do Nothing
        }
    }
}
