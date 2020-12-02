using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Sprite
{
    class ItemSpriteFactory
    {
        private Texture2D itemSpritesheet;
        private Texture2D nothingItemSpritesheet;
        private Texture2D portalGunSpritesheet;
        private const string itemFilePath = "Images/Item/items", nothingItemFilePath = "Images/Item/nothingItem", portalGunItemFilePath = "Images/Item/PortalGun";

        private const int CompassColumn = 2, CompassRow = 1, KeyColumn = 7, KeyRow = 1;
        private const int MapColumn = 6, MapRow = 2, HeartContainerColumn = 6, HeartContainerRow = 1;
        private const int BombColumn = 5, BombRow = 0, TriforceColumn = 8, TriforceRow = 3;
        private const int HeartColumn = 6, HeartRow = 3, BoomerangColumn = 7, BoomerangRow = 0;
        private const int Fairy1Column = 4, Fairy1Row = 1, Fairy2Column = 3, Fairy2Row = 1;
        private const int BlueRupeeColumn = 5, BlueRupeeRow = 3, YellowRupeeColumn = 4, YellowRupeeRow = 3;
        private const int BowColumn = 8, BowRow = 0, ClockColumn = 9, ClockRow = 0;
        private const int BluePotionColumn = 9, BluePotionRow = 1, BlueCandleColumn = 3, BlueCandleRow = 0;
        private const int ArrowColumn = 1, ArrowRow = 1, SwordColumn = 7, SwordRow = 3, NothingColumn = 1, NothingRow = 1;


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
            itemSpritesheet = content.Load<Texture2D>(itemFilePath);
            nothingItemSpritesheet = content.Load<Texture2D>(nothingItemFilePath);
            portalGunSpritesheet = content.Load<Texture2D>(portalGunItemFilePath);
        }
        
        public ISprite CreateCompassSprite()
        {
            return new ItemSprite(itemSpritesheet, CompassColumn, CompassRow);
        }

        public ISprite CreateKeySprite()
        {
            return new ItemSprite(itemSpritesheet, KeyColumn, KeyRow);
        }
        

        public ISprite CreateMapSprite()
        {
            return new ItemSprite(itemSpritesheet, MapColumn, MapRow);
        }

        public ISprite CreateHeartContainerSprite()
        {
            return new ItemSprite(itemSpritesheet, HeartContainerColumn, HeartContainerRow);
        }

        public ISprite CreateBombSprite()
        {
            return new ItemSprite(itemSpritesheet, BombColumn, BombRow);
        }

        public ISprite CreateTriforceSprite()
        {
            return new ItemSprite(itemSpritesheet, TriforceColumn, TriforceRow);
        }
        
        public ISprite CreateHeartSprite()
        {
            return new ItemSprite(itemSpritesheet, HeartColumn, HeartRow);
        }

        public ISprite CreateBoomerangSprite()
        {
            return new ItemSprite(itemSpritesheet, BoomerangColumn, BoomerangRow);
        }

        public ISprite CreatePortalGunSprite()
        {
            return new ParticleSprite(portalGunSpritesheet, 0, 0, 1, 1, 1);
        }

        public ISprite CreateFairySpriteOne()
        {
            return new ItemSprite(itemSpritesheet, Fairy1Column, Fairy1Row);
        }

        public ISprite CreateFairySpriteTwo()
        {
            return new ItemSprite(itemSpritesheet, Fairy2Column, Fairy2Row);
        }
        
        public ISprite CreateBlueRupeeSprite()
        {
            return new ItemSprite(itemSpritesheet, BlueRupeeColumn, BlueRupeeRow);
        }

        public ISprite CreateYellowRupeeSprite()
        {
            return new ItemSprite(itemSpritesheet, YellowRupeeColumn, YellowRupeeRow);
        }

        public ISprite CreateBowSprite()
        {
            return new ItemSprite(itemSpritesheet, BowColumn, BowRow);
        }

        public ISprite CreateClockSprite()
        {
            return new ItemSprite(itemSpritesheet, ClockColumn, ClockRow);
        }

        public ISprite CreateBluePotionSprite()
        {
            return new ItemSprite(itemSpritesheet, BluePotionColumn, BluePotionRow);
        }

        public ISprite CreateBlueCandleSprite()
        {
            return new ItemSprite(itemSpritesheet, BlueCandleColumn, BlueCandleRow);
        }
        
        public ISprite CreateArrowItemSprite()
        {
            return new ItemSprite(itemSpritesheet, ArrowColumn, ArrowRow);
        }

        public ISprite CreateSwordSprite()
        {
            return new ItemSprite(itemSpritesheet, SwordColumn, SwordRow);
        }

        public ISprite CreateNothingItemSprite()
        {
            return new ItemSprite(nothingItemSpritesheet, NothingColumn, NothingRow);
        }
    }
}
