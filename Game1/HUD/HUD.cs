using Game1.Player.PlayerInventory;
using Game1.RoomLoading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Net;

namespace Game1.HUD
{
    public class HUDInterface
    {
        public List<IHudItem> Items;
        public IHudItem displayItemTop;
        IHudItem displayItemBottom;
        IHudItem selectionSquare;
        IHudItem linkDot;
        IPlayerInventory inv;
        Screen screen;
        readonly Vector2 BBottomBox = new Vector2(110, 140);
        readonly Vector2 BUpperBox = new Vector2(50,-9);
        List<Tuple<char, int>> roomsEntered;

        public HUDInterface(IPlayerInventory playerInventory, Screen screen)
        {
            roomsEntered = new List<Tuple<char, int>>();
            inv = playerInventory;
            selectionSquare = HUDItemFactory.Instance.BuildHUDSelectionSquare();
            this.screen = screen;
            Items = HUDItemFactory.Instance.buildHUDList(playerInventory);
            linkDot = HUDItemFactory.Instance.BuildHUDLinkDot(screen);
        }

        public void Update(GameTime gameTime)
        {
            foreach (IHudItem Item in Items)
            {
                Point position = Mouse.GetState().Position;
                //System.Console.WriteLine(position);
                Item.Update(gameTime);

                Rectangle selectionRectangle; // Y multiplier for bottom row items messes with the selection rectangle and mouse
                if (Item.GetType() == typeof(HUDBoomerang) || Item.GetType() == typeof(HUDBomb) || Item.GetType() == typeof(HUDBlueCandle) || Item.GetType() == typeof(HUDBow)) {
                    const int xRectangleModifier = 4, yRectangleModifier = 44, widthAndHeightModifier = 3;
                    selectionRectangle = new Rectangle(Item.selectionRectangle.X * xRectangleModifier, Item.selectionRectangle.Y * yRectangleModifier, Item.selectionRectangle.Width * widthAndHeightModifier, Item.selectionRectangle.Height * widthAndHeightModifier);
                } else {
                    const int xRectangleModifier = 4, yRectangleModifier = 11, widthAndHeightModifier = 3;
                    selectionRectangle = new Rectangle(Item.selectionRectangle.X * xRectangleModifier, Item.selectionRectangle.Y * yRectangleModifier, Item.selectionRectangle.Width * widthAndHeightModifier, Item.selectionRectangle.Height * widthAndHeightModifier);
                }

                if (selectionRectangle.Contains(position))
                {
                    displayItemTop = Item.copyOf();
                }
            }

            Tuple<char, int> currentRoom = screen.CurrentRoomKey.ToTuple();
            if(!roomsEntered.Contains(currentRoom))
            {
                const int multiplier = 8, positionXModifier = 130, positionYModifier1 = 65, positionYModifier2 = 50;
                Vector2 position = new Vector2((currentRoom.Item2 * multiplier) + positionXModifier, multiplier * (currentRoom.Item1 - positionYModifier1) + positionYModifier2);
                Items.Add(HUDItemFactory.Instance.BuildRoom(screen.CurrentRoom, position));
                roomsEntered.Add(currentRoom);
            }

            selectionSquare.Update(gameTime);

            const int xMultiplier = 8, yMultiplier = 4, xModifier = 25, yModifier1 = 65;
            const float yModifier2 = 145f;
            Vector2 DotPosition = new Vector2(currentRoom.Item2 * xMultiplier + xModifier, (currentRoom.Item1 - yModifier1) * yMultiplier + yModifier2);

            linkDot.location = DotPosition;

            
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 movement ,Color color)
        {
            foreach (IHudItem Item in Items)
            {
                Item.Draw(spriteBatch, movement, color);
            }
            if (displayItemTop != null)
            {
                displayItemBottom = displayItemTop.copyOf();
                displayItemBottom.location = BBottomBox;
                displayItemTop.location = BUpperBox;
                inv.EquippedItem = displayItemTop.myItem;

                displayItemBottom.Draw(spriteBatch, movement, color);
                displayItemTop.Draw(spriteBatch, movement, color);
                selectionSquare.location = new Vector2(displayItemTop.selectionRectangle.X, displayItemTop.selectionRectangle.Y);
            }

            linkDot.Draw(spriteBatch, movement, color);
            selectionSquare.Draw(spriteBatch, movement ,color);
        }
    }
}
