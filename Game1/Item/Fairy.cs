using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Game1.Item
{
    class Fairy : IItem
    {
        private ISprite sprite;

        private Vector2 position;

        int timeTillSwap;
        bool frameChanged = true;
        const int timer = 250; //ms

        public Fairy(Vector2 position)
        {
            sprite = ItemSpriteFactory.Instance.CreateFairySpriteOne();

            this.position = position;

            timeTillSwap = timer;
        }
        public void Update(GameTime gameTime)
        {
            timeTillSwap -= (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timeTillSwap <= 0)
            {
                if (frameChanged)
                    sprite = ItemSpriteFactory.Instance.CreateFairySpriteTwo();
                else
                    sprite = ItemSpriteFactory.Instance.CreateFairySpriteOne();
                timeTillSwap = timer;
                frameChanged = !frameChanged;
            }
        }
        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            sprite.Draw(spriteBatch, position, color);
        }
    }
}
