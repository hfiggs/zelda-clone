using Game1.Environment;
using Game1.HUD.HUDItems;
using Game1.Player.PlayerInventory;
using Game1.RoomLoading;
using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


//SCREEN SIZE: (256,176)

namespace Game1.HUD
{
    class HUDItemFactory
    {
        Texture2D selectionSquareTexture;
        Texture2D HUDIconsTexture;
        Texture2D map;
        Texture2D HUDbase;

        private static HUDItemFactory instance = new HUDItemFactory();

        public static HUDItemFactory Instance
        {
            get
            {
                return instance;
            }
        }

        public void LoadAllTextures(ContentManager content)
        {
            HUDbase = content.Load<Texture2D>("Images/HUDSprites/HUD");
            map = content.Load<Texture2D>("Images/HUDSprites/Dungeon1 Minimap");
            selectionSquareTexture = content.Load<Texture2D>("Images/HUDSprites/selection rectangles");
            HUDIconsTexture = content.Load<Texture2D>("Images/HUDSprites/HUD Icons");
        }

        public IHudItem BuildDungeonOneMap(IPlayerInventory inv)
        {
            return new DungeonOneMap(inv, new HUDSprite(map, 0, 0, 1, 1));
        }
        public IHudItem BuildHUDMap(IPlayerInventory inv)
        {
            return new HUDMap(inv, new HUDSprite(ItemSpriteFactory.Instance.CreateMapSprite()));
        }

        public IHudItem BuildHUDBase()
        {
            return new HUDBase(new HUDSprite(HUDbase, 0, 0, 1, 1));
        }

        public IHudItem BuildHUDBow(IPlayerInventory inv, Vector2 position)
        {
            return new HUDBow(inv, new HUDSprite(ItemSpriteFactory.Instance.CreateBowSprite()),position);
        }

        public IHudItem BuildHUDCompass(IPlayerInventory inv)
        {
            return new HUDCompass(inv, new HUDSprite(ItemSpriteFactory.Instance.CreateCompassSprite()));
        }

        public IHudItem BuildHUDHeartBar(IPlayerInventory inv)
        {
            HUDSprite FullHeart = new HUDSprite(HUDIconsTexture, 1, 14, 16, 3);
            HUDSprite HalfHeart = new HUDSprite(HUDIconsTexture, 1, 13, 16, 3);
            HUDSprite EmptyHeart = new HUDSprite(HUDIconsTexture, 1, 12, 16, 3);
            return new HUDHeartBar(inv,FullHeart,HalfHeart,EmptyHeart);
        }
        public IHudItem BuildHUDFlashingDot(IPlayerInventory inv, Vector2 bossPosition)
        {
            return new HUDFlashingDot(inv,bossPosition,new HUDSprite(HUDIconsTexture,2,2,16,3) ,new HUDSprite(HUDIconsTexture,2,3,16,3));
        }
        public IHudItem BuildHUDBomb(IPlayerInventory inv, Vector2 position)
        {
            return new HUDBomb(inv, new HUDSprite(ItemSpriteFactory.Instance.CreateBombSprite()),position);
        }
        public IHudItem BuildHUDArrow(IPlayerInventory inv)
        {
            return new HUDArrow(inv, new HUDSprite(ItemSpriteFactory.Instance.CreateArrowItemSprite()));
        }
        public IHudItem BuildHUDBoomerang(IPlayerInventory inv, Vector2 position)
        {
            return new HUDBoomerang(inv, new HUDSprite(ItemSpriteFactory.Instance.CreateBoomerangSprite()), position);
        }

        public IHudItem BuildRoom(Room room, Vector2 position)
        {
            int roomColumn = 0;
            foreach(IEnvironment env in room.NonInteractEnviornment)
            {
                if (env.GetType() == typeof(DoorWFloor))
                {
                    roomColumn += 1;
                }else if (env.GetType() == typeof(DoorEFloor))
                {
                    roomColumn += 2;
                }
                else if (env.GetType() == typeof(DoorSFloor))
                {
                    roomColumn += 4;
                }
                else if (env.GetType() == typeof(DoorNFloor))
                {
                    roomColumn += 8;
                }
            }

            return new HUDRoom(new HUDSprite(HUDIconsTexture,0,roomColumn,16,3),position);
        }

        public IHudItem BuildHUDRupeeTextBox(IPlayerInventory inv)
        {
            return new HUDRupeeTextBox(HUDIconsTexture,1,inv);
        }

        public IHudItem BuildHUDBombTextBox(IPlayerInventory inv)
        {
            return new HUDBombTextBox(HUDIconsTexture, 1, inv);
        }

        public IHudItem BuildHUDKeyTextBox(IPlayerInventory inv)
        {
            return new HUDKeyTextBox(HUDIconsTexture, 1, inv);
        }

        public IHudItem BuildHUDSword()
        {
            return new HUDSword(new HUDSprite(ItemSpriteFactory.Instance.CreateSwordSprite()));
        }

        public IHudItem BuildHUDSelectionSquare()
        {
            return new HUDSelectionSquare(new HUDSprite(selectionSquareTexture,0,0,2,1),new HUDSprite(selectionSquareTexture,0,1,2,1));
        }

    }
}
