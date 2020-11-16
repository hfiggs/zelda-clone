using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Sprite
{
    class ItemSpriteFactory
    {
        private Texture2D itemSpritesheet;
        private Texture2D nothingItemSpritesheet;

        private static ItemSpriteFactory instance = new ItemSpriteFactory();

        public static ItemSpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private ItemSpriteFactory()
        {
        }

        public void LoadAllTextures(ContentManager content)
        {
            itemSpritesheet = content.Load<Texture2D>("Images/Item/items");
            nothingItemSpritesheet = content.Load<Texture2D>("Images/Item/nothingItem");
        }

        public ISprite CreateCompassSprite()
        {
            return new ItemSprite(itemSpritesheet, 2, 1);
        }

        public ISprite CreateKeySprite()
        {
            return new ItemSprite(itemSpritesheet, 7, 1);
        }

        public ISprite CreateMapSprite()
        {
            return new ItemSprite(itemSpritesheet, 6, 2);
        }

        public ISprite CreateHeartContainerSprite()
        {
            return new ItemSprite(itemSpritesheet, 6, 1);
        }

        public ISprite CreateBombSprite()
        {
            return new ItemSprite(itemSpritesheet, 5, 0);
        }

        public ISprite CreateTriforceSprite()
        {
            return new ItemSprite(itemSpritesheet, 8, 3);
        }

        public ISprite CreateHeartSprite()
        {
            return new ItemSprite(itemSpritesheet, 6, 3);
        }

        public ISprite CreateBoomerangSprite()
        {
            return new ItemSprite(itemSpritesheet, 7, 0);
        }

        public ISprite CreateFairySpriteOne()
        {
            return new ItemSprite(itemSpritesheet, 4,1);
        }

        public ISprite CreateFairySpriteTwo()
        {
            return new ItemSprite(itemSpritesheet,3,1);
        }

        public ISprite CreateBlueRupeeSprite()
        {
            return new ItemSprite(itemSpritesheet, 5, 3);
        }

        public ISprite CreateYellowRupeeSprite()
        {
            return new ItemSprite(itemSpritesheet, 4, 3);
        }

        public ISprite CreateBowSprite()
        {
            return new ItemSprite(itemSpritesheet, 8, 0);
        }

        public ISprite CreateClockSprite()
        {
            return new ItemSprite(itemSpritesheet, 9, 0);
        }

        public ISprite CreateBluePotionSprite()
        {
            return new ItemSprite(itemSpritesheet, 9, 1);
        }

        public ISprite CreateBlueCandleSprite()
        {
            return new ItemSprite(itemSpritesheet, 3, 0);
        }

        public ISprite CreateArrowItemSprite()
        {
            return new ItemSprite(itemSpritesheet, 1, 1);
        }

        public ISprite CreateSwordSprite()
        {
            return new ItemSprite(itemSpritesheet, 7, 3);
        }

        public ISprite CreateNothingItemSprite()
        {
            return new ItemSprite(nothingItemSpritesheet, 1, 1);
        }
    }
}
