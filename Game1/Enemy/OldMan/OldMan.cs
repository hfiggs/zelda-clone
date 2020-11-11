using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Game1.Enemy
{
    class OldMan : IEnemy
    {
        public int StunnedTimer { get; set; } = 0;

        private ISprite sprite;

        private Vector2 position;

        public OldMan(Vector2 position)
        {
            sprite = EnemySpriteFactory.Instance.CreateOldManSprite();
            this.position = position;
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            sprite.Draw(spriteBatch, position, color);
        }

        public void ReceiveDamage(float amount, Vector2 direction)
        {
            // Cannot receive damage
        }

        public void Update(GameTime gameTime, Rectangle drawingLimits5)
        {
            // TODO: Logic for determining text and when to fade out sprite
        }

        public void EditPosition(Vector2 amount)
        {
            //Does nothing!
        }

        public bool ShouldRemove()
        {
            return false;
        }

        public void SetState(IEnemyState state)
        {
            //Do Nothing
        }
        public Vector2 GetPosition()
        {
            return position;
        }
        public List<Rectangle> GetHitboxes()
        {
            List<Rectangle> hitboxList = new List<Rectangle>();
            const int widthAndHeight = 16;
            hitboxList.Add(new Rectangle((int)position.X, (int)position.Y, widthAndHeight, widthAndHeight));
            return hitboxList;
        }
    }
}
