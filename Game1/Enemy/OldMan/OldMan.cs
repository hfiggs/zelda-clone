using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game1.Enemy
{
    class OldMan : IEnemy
    {
        ISprite sprite;

        SpriteBatch spritebatch;
        Vector2 position;

        public OldMan(SpriteBatch spritebatch, Vector2 position)
        {
            sprite = EnemySpriteFactory.Instance.CreateOldManSprite();

            this.spritebatch = spritebatch;

            this.position = position;
        }

        public void Attack()
        {
            // Cannot attack
        }

        public void Draw()
        {
            sprite.Draw(spritebatch, position, Color.White);
        }

        public void ReceiveDamage()
        {
            // Cannot receive damage
        }

        public void Update(GameTime gameTime, Rectangle drawingLimits5)
        {
            // TODO: Logic for determining text and when to fade out sprite
        }
    }
}
