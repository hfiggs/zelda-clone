using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Game1.Item
{
    class Fairy : IItem
    {
        private ISprite sprite;
        public Vector2 Position { get; set; }

        int timeTillSwap;
        bool frameChanged = true;
        const int timer = 250; //ms

        public Fairy(Vector2 Position)
        {
            sprite = ItemSpriteFactory.Instance.CreateFairySpriteOne();

            this.Position = Position;

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
            sprite.Draw(spriteBatch, Position, color);
        }

        public Rectangle GetHitbox()
        {
            return new Rectangle((int)Position.X + 10, (int)Position.Y + 10, 20, 20);
        }
    }
}
