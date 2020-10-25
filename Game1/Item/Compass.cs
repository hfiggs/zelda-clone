using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Item
{
    class Compass : IItem
    {
        private ISprite sprite;

        private Vector2 position;

        public Compass(Vector2 position)
        {
            sprite = ItemSpriteFactory.Instance.CreateCompassSprite();

            this.position = position;
        }
        public void Update(GameTime gameTime)
        {

        }
        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            sprite.Draw(spriteBatch, position, color);
        }

        public Rectangle GetHitbox()
        {
            return new Rectangle((int)position.X + 10, (int)position.Y + 10, 20, 20);
        }

        public bool ShouldDelete { get; set; } = false;
    }
}
