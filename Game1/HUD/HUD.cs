using Game1.Item;
using Game1.Player.PlayerInventory;
using Game1.Projectile;
using Game1.RoomLoading;
using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.MediaFoundation;
using System.Collections.Generic;

namespace Game1.HUD
{
    class HUDInterface
    {
        List<IHudItem> Items = new List<IHudItem>();
        IHudItem displayItemTop;
        IHudItem displayItemBottom;
        IHudItem selectionSquare;

        IPlayerInventory playerInventory;
        Screen screen;
        Vector2 BBottomBox = new Vector2(110, 140);
        Vector2 BUpperBox = new Vector2(50,-9);

        public HUDInterface(IPlayerInventory playerInventory, Screen screen)
        {
            this.playerInventory = playerInventory;
            this.screen = screen;

            selectionSquare = HUDItemFactory.Instance.BuildHUDSelectionSquare();
            Items.Add(HUDItemFactory.Instance.BuildHUDBase());
            Items.Add(HUDItemFactory.Instance.BuildDungeonOneMap(playerInventory));
            Items.Add(HUDItemFactory.Instance.BuildHUDRupeeTextBox(playerInventory));
            Items.Add(HUDItemFactory.Instance.BuildHUDArrow(playerInventory));
            Items.Add(HUDItemFactory.Instance.BuildHUDKeyTextBox(playerInventory));
            Items.Add(HUDItemFactory.Instance.BuildHUDBombTextBox(playerInventory));
            Items.Add(HUDItemFactory.Instance.BuildHUDBow(playerInventory, new Vector2(177, -9)));
            Items.Add(HUDItemFactory.Instance.BuildHUDCompass(playerInventory));
            Items.Add(HUDItemFactory.Instance.BuildHUDMap(playerInventory));
            Items.Add(HUDItemFactory.Instance.BuildHUDBomb(playerInventory, new Vector2(150,-9)));
            Items.Add(HUDItemFactory.Instance.BuildHUDBoomerang(playerInventory, new Vector2(130, -9)));
            Items.Add(HUDItemFactory.Instance.BuildHUDHeartBar(playerInventory));
            Items.Add(HUDItemFactory.Instance.BuildHUDFlashingDot(playerInventory, new Vector2(65, 149)));
            Items.Add(HUDItemFactory.Instance.BuildHUDSword());
            
        }

        public void Update(GameTime gameTime)
        {
            foreach (IHudItem Item in Items)
            {
                Point position = Mouse.GetState().Position;
                Item.Update(gameTime);
                Rectangle selectionRectangle = new Rectangle(Item.selectionRectangle.X * 4, Item.selectionRectangle.Y * 8, Item.selectionRectangle.Width * 3, Item.selectionRectangle.Height * 3);
                if (selectionRectangle.Contains(position))
                {
                    selectionSquare.location = new Vector2(Item.selectionRectangle.X, Item.selectionRectangle.Y);
                    displayItemTop = Item.copyOf();
                    displayItemBottom = Item.copyOf();
                    displayItemBottom.location = BBottomBox;
                    displayItemTop.location = BUpperBox;
                    
                }
            }
            selectionSquare.Update(gameTime);

            //TODO: Check for room addition and perform room change -- requires dictionary version of rooms list 
            
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 movement ,Color color)
        {

            foreach (IHudItem Item in Items)
            {
                Item.Draw(spriteBatch, movement ,color);
            }
            if (displayItemTop != null)
            {
                displayItemBottom.Draw(spriteBatch, movement, color);
                displayItemTop.Draw(spriteBatch, movement, color);
            }
            selectionSquare.Draw(spriteBatch, movement ,color);
        }
    }
}
