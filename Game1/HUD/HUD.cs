﻿using Game1.Player.PlayerInventory;
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
        IPlayerInventory inv;
        Screen screen;
        Vector2 BBottomBox = new Vector2(110, 140);
        Vector2 BUpperBox = new Vector2(50,-9);
        List<Tuple<char, int>> roomsEntered;

        public HUDInterface(IPlayerInventory playerInventory, Screen screen)
        {
            roomsEntered = new List<Tuple<char, int>>();
            inv = playerInventory;
            selectionSquare = HUDItemFactory.Instance.BuildHUDSelectionSquare();
            this.screen = screen;
            Items = HUDItemFactory.Instance.buildHUDList(playerInventory);
        }

        public void Update(GameTime gameTime)
        {
            foreach (IHudItem Item in Items)
            {
                Point position = Mouse.GetState().Position;
                System.Console.WriteLine(position);
                Item.Update(gameTime);
                Rectangle selectionRectangle = new Rectangle(Item.selectionRectangle.X * 4, Item.selectionRectangle.Y * 48, Item.selectionRectangle.Width * 3, Item.selectionRectangle.Height * 3);
                if (selectionRectangle.Contains(position))
                {
                    displayItemTop = Item.copyOf();
                }
            }

            Tuple<char, int> currentRoom = screen.CurrentRoomKey.ToTuple();
            if(!roomsEntered.Contains(currentRoom))
            {
                Vector2 position = new Vector2((currentRoom.Item2 * 8) + 130,8*(currentRoom.Item1 - 65) + 50);
                Items.Add(HUDItemFactory.Instance.BuildRoom(screen.CurrentRoom, position));
                roomsEntered.Add(currentRoom);
            }

            selectionSquare.Update(gameTime);

            
            
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 movement ,Color color)
        {

            foreach (IHudItem Item in Items)
            {
                Item.Draw(spriteBatch, movement ,color);
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


            selectionSquare.Draw(spriteBatch, movement ,color);
        }
    }
}
