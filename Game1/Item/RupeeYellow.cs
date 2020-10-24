using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Game1.Item
{
    class RupeeYellow : IItem
    {
        private ISprite sprite;

        private Vector2 position;

        int timeTillSwap;
        bool frameChanged = true;
        const int timer = 250; //ms

        public RupeeYellow(Vector2 position)
        {
            sprite = ItemSpriteFactory.Instance.CreateBlueRupeeSprite();

            this.position = position;

            timeTillSwap = timer;
        }
        public void Update(GameTime gameTime)
        {
            timeTillSwap -= (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timeTillSwap <= 0)
            {
                if (frameChanged)
                    sprite = ItemSpriteFactory.Instance.CreateYellowRupeeSprite();
                else
                    sprite = ItemSpriteFactory.Instance.CreateBlueRupeeSprite();
                timeTillSwap = timer;
                frameChanged = !frameChanged;
            }
        }
        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            sprite.Draw(spriteBatch, position, color);
        }

        public Rectangle GetHitbox()
        {
            return new Rectangle((int)position.X + 10, (int)position.Y + 10, 20, 20);
        }
    }
}