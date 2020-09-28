using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Game1.Item
{
    class Fairy : IItem
    {
        ISprite mySprite;
        int timeTillSwap;
        bool frameChanged = true;
        const int timer = 250; //ms

        public Fairy()
        {
            mySprite = ItemSpriteFactory.Instance.CreateFairySpriteOne();
            timeTillSwap = timer;
        }
        public void Update(GameTime gameTime)
        {
            timeTillSwap -= (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timeTillSwap <= 0)
            {
                if (frameChanged)
                    mySprite = ItemSpriteFactory.Instance.CreateFairySpriteTwo();
                else
                    mySprite = ItemSpriteFactory.Instance.CreateFairySpriteOne();
                timeTillSwap = timer;
                frameChanged = !frameChanged;
            }
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            mySprite.Draw(spriteBatch, position, Color.White);
        }
    }
}
