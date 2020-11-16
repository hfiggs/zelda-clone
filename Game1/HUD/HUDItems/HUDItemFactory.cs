using Game1.Environment;
using Game1.HUD.HUDItems;
using Game1.Player.PlayerInventory;
using Game1.RoomLoading;
using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


//SCREEN SIZE: (256,176)

namespace Game1.HUD
{
    class HUDItemFactory
    {
        Texture2D selectionSquareTexture;
        Texture2D HUDIconsTexture;
        Texture2D map;
        Texture2D HUDbase;
        private const int bowXPosition = 177, bowYPosition = -9, blueCandleXPosition = 190, blueCandleYPosition = -9, bluePotionXPosition = 170, bluePotionYPosition = 11, bombXPosition = 150, bombYPosition = -9, boomerangXPosition = 130, boomerangYPosition = -9, flashingDotXPosition = 65, flashingDotYPosition = 149;
        private const int textBoxRow = 1;
        private const string HUDBaseFilePath = "Images/HUD/HUD", mapFilePath = "Images/HUD/Dungeon1 Minimap", selectionSquareFilePath = "Images/HUD/selection rectangles", HUDIconFilePath = "Images/HUD/HUD Icons";
        private static HUDItemFactory instance = new HUDItemFactory();

        public static HUDItemFactory Instance
        {
            get
            {
                return instance;
            }
        }

        public List<IHudItem> buildHUDList(IPlayerInventory playerInventory)
        {
            List<IHudItem> Items = new List<IHudItem>();

            Items.Add(HUDItemFactory.Instance.BuildHUDBase());
            Items.Add(HUDItemFactory.Instance.BuildDungeonOneMap(playerInventory));
            Items.Add(HUDItemFactory.Instance.BuildHUDRupeeTextBox(playerInventory));
            Items.Add(HUDItemFactory.Instance.BuildHUDArrow(playerInventory));
            Items.Add(HUDItemFactory.Instance.BuildHUDKeyTextBox(playerInventory));
            Items.Add(HUDItemFactory.Instance.BuildHUDBombTextBox(playerInventory));
            Items.Add(HUDItemFactory.Instance.BuildHUDBow(playerInventory, new Vector2(bowXPosition, bowYPosition)));
            Items.Add(HUDItemFactory.Instance.BuildHUDBlueCandle(playerInventory, new Vector2(blueCandleXPosition, blueCandleYPosition)));
            Items.Add(HUDItemFactory.Instance.BuildHUDCompass(playerInventory));
            Items.Add(HUDItemFactory.Instance.BuildHUDMap(playerInventory));
            Items.Add(HUDItemFactory.Instance.BuildHUDBluePotion(playerInventory, new Vector2(bluePotionXPosition, bluePotionYPosition)));
            Items.Add(HUDItemFactory.Instance.BuildHUDBomb(playerInventory, new Vector2(bombXPosition, bombYPosition)));
            Items.Add(HUDItemFactory.Instance.BuildHUDBoomerang(playerInventory, new Vector2(boomerangXPosition, boomerangYPosition)));
            Items.Add(HUDItemFactory.Instance.BuildHUDHeartBar(playerInventory));
            Items.Add(HUDItemFactory.Instance.BuildHUDFlashingDot(playerInventory, new Vector2(flashingDotXPosition, flashingDotYPosition)));
            Items.Add(HUDItemFactory.Instance.BuildHUDSword());

            return Items;
        }
        public void LoadAllTextures(ContentManager content)
        {
            HUDbase = content.Load<Texture2D>(HUDBaseFilePath);
            map = content.Load<Texture2D>(mapFilePath);
            selectionSquareTexture = content.Load<Texture2D>(selectionSquareFilePath);
            HUDIconsTexture = content.Load<Texture2D>(HUDIconFilePath);
        }

        public IHudItem BuildDungeonOneMap(IPlayerInventory inv)
        {
            const int mapColumn = 0, mapRow = 0, mapColumns = 1, mapRows = 1;
            return new DungeonOneMap(inv, new HUDSprite(map, mapColumn, mapRow, mapColumns, mapRows));
        }
        public IHudItem BuildHUDMap(IPlayerInventory inv)
        {
            return new HUDMap(inv, new HUDSprite(ItemSpriteFactory.Instance.CreateMapSprite()));
        }

        public IHudItem BuildHUDBase()
        {
            const int HUDBaseColumn = 0, HUDBaseRow = 0, HUDBaseColumns = 1, HUDBaseRows = 1;
            return new HUDBase(new HUDSprite(HUDbase, HUDBaseColumn, HUDBaseRow, HUDBaseColumns, HUDBaseRows));
        }

        public IHudItem BuildHUDBow(IPlayerInventory inv, Vector2 position)
        {
            return new HUDBow(inv, new HUDSprite(ItemSpriteFactory.Instance.CreateBowSprite()),position);
        }

        public IHudItem BuildHUDBlueCandle(IPlayerInventory inv, Vector2 position)
        {
            return new HUDBlueCandle(inv, new HUDSprite(ItemSpriteFactory.Instance.CreateBlueCandleSprite()), position);
        }

        public IHudItem BuildHUDCompass(IPlayerInventory inv)
        {
            return new HUDCompass(inv, new HUDSprite(ItemSpriteFactory.Instance.CreateCompassSprite()));
        }

        public IHudItem BuildHUDHeartBar(IPlayerInventory inv)
        {
            const int fullHeartColumn = 1, fullHeartRow = 14, halfHeartColumn = 1, halfHeartRow = 13, emptyHeartColumn = 1, emptyHeartRow = 12, heartColumns = 16, heartRows = 3;
            HUDSprite FullHeart = new HUDSprite(HUDIconsTexture, fullHeartColumn, fullHeartRow, heartColumns, heartRows);
            HUDSprite HalfHeart = new HUDSprite(HUDIconsTexture, halfHeartColumn, halfHeartRow, heartColumns, heartRows);
            HUDSprite EmptyHeart = new HUDSprite(HUDIconsTexture, emptyHeartColumn, emptyHeartRow, heartColumns, heartRows);
            return new HUDHeartBar(inv,FullHeart,HalfHeart,EmptyHeart);
        }

        public IHudItem BuildHUDFlashingDot(IPlayerInventory inv, Vector2 bossPosition)
        {
            const int flashingDot1Column = 2, flashingDot1Row = 2, flashingDot2Column = 2, flashingDot2Row = 3, flashingDotColumns = 16, flashingDotRows = 3;
            return new HUDFlashingDot(inv,bossPosition,new HUDSprite(HUDIconsTexture,flashingDot1Column,flashingDot1Row,flashingDotColumns,flashingDotRows) ,new HUDSprite(HUDIconsTexture,flashingDot2Column,flashingDot2Row,flashingDotColumns,flashingDotRows));
        }

        public IHudItem BuildHUDBomb(IPlayerInventory inv, Vector2 position)
        {
            return new HUDBomb(inv, new HUDSprite(ItemSpriteFactory.Instance.CreateBombSprite()),position);
        }

        public IHudItem BuildHUDBluePotion(IPlayerInventory inv, Vector2 position)
        {
            return new HUDBluePotion(inv, new HUDSprite(ItemSpriteFactory.Instance.CreateBluePotionSprite()), position);
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
            const int westColumnModifier = 2, eastColumnModifier = 1, southColumnModifier = 4, northColumnModifier = 8, roomRow = 0, roomColumns = 16, roomRows = 3;

            int roomColumn = 0;
            foreach(IEnvironment env in room.NonInteractEnviornment)
            {
                if (env.GetType() == typeof(DoorWFloor))
                {
                    roomColumn += westColumnModifier;
                }else if (env.GetType() == typeof(DoorEFloor))
                {
                    roomColumn += eastColumnModifier;
                }
                else if (env.GetType() == typeof(DoorSFloor))
                {
                    roomColumn += southColumnModifier;
                }
                else if (env.GetType() == typeof(DoorNFloor))
                {
                    roomColumn += northColumnModifier;
                }
            }

            return new HUDRoom(new HUDSprite(HUDIconsTexture,roomRow,roomColumn,roomColumns,roomRows),position);
        }

        public IHudItem BuildHUDRupeeTextBox(IPlayerInventory inv)
        {
            return new HUDRupeeTextBox(HUDIconsTexture,textBoxRow,inv);
        }

        public IHudItem BuildHUDBombTextBox(IPlayerInventory inv)
        {
            return new HUDBombTextBox(HUDIconsTexture, textBoxRow, inv);
        }

        public IHudItem BuildHUDKeyTextBox(IPlayerInventory inv)
        {
            return new HUDKeyTextBox(HUDIconsTexture, textBoxRow, inv);
        }

        public IHudItem BuildHUDSword()
        {
            return new HUDSword(new HUDSprite(ItemSpriteFactory.Instance.CreateSwordSprite()));
        }

        public IHudItem BuildHUDSelectionSquare()
        {
            const int square1Column = 0, square1Row = 0, square2Column = 0, square2Row = 1, squareColumns = 2, squareRows = 1;
            return new HUDSelectionSquare(new HUDSprite(selectionSquareTexture,square1Column,square1Row,squareColumns,squareRows),new HUDSprite(selectionSquareTexture,square2Column,square2Row,squareColumns,squareRows));
        }

        public IHudItem BuildHUDLinkDot(Screen screen)
        {
            const int linkDotColumn = 2, linkDotRow = 0, linkDotColumns = 16, linkDotRows = 3;
            return new HUDLinkDot(new HUDSprite(HUDIconsTexture, linkDotColumn, linkDotRow, linkDotColumns, linkDotRows), screen);
        }

    }
}
