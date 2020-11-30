using Game1.Player.PlayerInventory;
using Game1.Player;
using Game1.RoomLoading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Game1.HUD
{
    public class HUDInterface
    {
        public List<IHudItem>[] Items;
        public IHudItem displayItemTop, displayItemTop2;
        IHudItem displayItemBottom, displayItemBottom2;
        IHudItem linkDot, selectionSquare1, selectionSquare2;
        IPlayerInventory[] inv = new IPlayerInventory[2]; // Create with size of two (currently max of 2 players)
        Screen screen;
        readonly Vector2 BBottomBox = new Vector2(120, 140);
        readonly Vector2 BUpperBox = new Vector2(50,-9);
        readonly Vector2 BBottomBox2 = new Vector2(158, 140);
        List<Tuple<char, int>> roomsEntered;
        public bool displayHUD1 { get; set; }
        public bool displayHUD2 { get; set; }
        public bool twoPlayers { get; private set; }

        public HUDInterface(List<IPlayer> players, Screen screen)
        {
            roomsEntered = new List<Tuple<char, int>>();
            this.screen = screen;
            linkDot = HUDItemFactory.Instance.BuildHUDLinkDot(screen);

            int playerNumber = 0;
            foreach (IPlayer player in players)
            {
                inv[playerNumber] = player.PlayerInventory;
                playerNumber++;
            }

            if (players.Count == 1) {
                twoPlayers = false;
            } else { 
                twoPlayers = true;
            }

            if (twoPlayers)
            {
                selectionSquare1 = HUDItemFactory.Instance.BuildHUDSelectionSquare();
                selectionSquare2 = HUDItemFactory.Instance.BuildHUDSelectionSquare();
                Items = new List<IHudItem>[inv.Length];
                
                Items[0] = HUDItemFactory.Instance.AddTwoPlayerHUDList(inv[1]);
                Items[1] = HUDItemFactory.Instance.AddTwoPlayerHUDList(inv[1]);
            } else {
                selectionSquare1 = HUDItemFactory.Instance.BuildHUDSelectionSquare();
                Items = new List<IHudItem>[1];

                Items[0] = HUDItemFactory.Instance.AddSinglePlayerHUDList();
            }

            for (int i = 0; i < players.Count; i++) { 
                Items[i].AddRange(HUDItemFactory.Instance.BuildHUDList(inv[i]));
            }

            displayHUD1 = true;
        }

        public void Update(GameTime gameTime)
        {
            Point position = Mouse.GetState().Position;
            System.Console.WriteLine(position);

            if (displayHUD1 || !twoPlayers) {
                foreach (IHudItem Item in Items[0])
                {
                    Item.Update(gameTime);

                    Rectangle selectionRectangle; // Y multiplier for bottom row items messes with the selection rectangle and mouse
                    if (Item.GetType() == typeof(HUDBoomerang) || Item.GetType() == typeof(HUDBomb) || Item.GetType() == typeof(HUDBlueCandle) || Item.GetType() == typeof(HUDBow))
                    {
                        const int xRectangleModifier = 4, yRectangleModifier = 44, widthAndHeightModifier = 3;
                        selectionRectangle = new Rectangle(Item.selectionRectangle.X * xRectangleModifier, Item.selectionRectangle.Y * yRectangleModifier, Item.selectionRectangle.Width * widthAndHeightModifier, Item.selectionRectangle.Height * widthAndHeightModifier);
                    }
                    else
                    {
                        const int xRectangleModifier = 4, yRectangleModifier = 11, widthAndHeightModifier = 3;
                        selectionRectangle = new Rectangle(Item.selectionRectangle.X * xRectangleModifier, Item.selectionRectangle.Y * yRectangleModifier, Item.selectionRectangle.Width * widthAndHeightModifier, Item.selectionRectangle.Height * widthAndHeightModifier);
                    }

                    if (selectionRectangle.Contains(position))
                    {
                        displayItemTop = Item.copyOf();
                    }
                }

                selectionSquare1.Update(gameTime);
            } else if (displayHUD2) {
                foreach (IHudItem Item in Items[1])
                {
                    Item.Update(gameTime);

                    Rectangle selectionRectangle; // Y multiplier for bottom row items messes with the selection rectangle and mouse
                    if (Item.GetType() == typeof(HUDBoomerang) || Item.GetType() == typeof(HUDBomb) || Item.GetType() == typeof(HUDBlueCandle) || Item.GetType() == typeof(HUDBow))
                    {
                        const int xRectangleModifier = 4, yRectangleModifier = 44, widthAndHeightModifier = 3;
                        selectionRectangle = new Rectangle(Item.selectionRectangle.X * xRectangleModifier, Item.selectionRectangle.Y * yRectangleModifier, Item.selectionRectangle.Width * widthAndHeightModifier, Item.selectionRectangle.Height * widthAndHeightModifier);
                    }
                    else
                    {
                        const int xRectangleModifier = 4, yRectangleModifier = 11, widthAndHeightModifier = 3;
                        selectionRectangle = new Rectangle(Item.selectionRectangle.X * xRectangleModifier, Item.selectionRectangle.Y * yRectangleModifier, Item.selectionRectangle.Width * widthAndHeightModifier, Item.selectionRectangle.Height * widthAndHeightModifier);
                    }

                    if (selectionRectangle.Contains(position))
                    {
                        displayItemTop2 = Item.copyOf();
                    }
                }

                selectionSquare2.Update(gameTime);
            } 

            Tuple<char, int> currentRoom = screen.CurrentRoomKey.ToTuple();
            if(!roomsEntered.Contains(currentRoom))
            {
                const int multiplier = 8, positionXModifier = 130, positionYModifier1 = 65, positionYModifier2 = 50;
                Vector2 roomPosition = new Vector2((currentRoom.Item2 * multiplier) + positionXModifier, multiplier * (currentRoom.Item1 - positionYModifier1) + positionYModifier2);
                for (int i = 0; i < Items.Length; i++)
                {
                    Items[i].Add(HUDItemFactory.Instance.BuildRoom(screen.CurrentRoom, roomPosition));
                }
                roomsEntered.Add(currentRoom);
            }

            const int xMultiplier = 8, yMultiplier = 4, xModifier = 7, yModifier1 = 65;
            const float yModifier2 = 145f;
            Vector2 DotPosition = new Vector2(currentRoom.Item2 * xMultiplier + xModifier, (currentRoom.Item1 - yModifier1) * yMultiplier + yModifier2);

            linkDot.location = DotPosition;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 movement ,Color color)
        {
            // Draw appropriate top
            if (displayHUD1 || !twoPlayers) {
                foreach (IHudItem Item in Items[0])
                {
                    Item.Draw(spriteBatch, movement, color);
                }
            } else if (displayHUD2) {
                foreach (IHudItem Item in Items[1])
                {
                    Item.Draw(spriteBatch, movement, color);
                }
            }

            if (displayItemTop != null)
            {
                displayItemBottom = displayItemTop.copyOf();
                displayItemBottom.location = BBottomBox;
                displayItemTop.location = BUpperBox;
                inv[0].EquippedItem = displayItemTop.myItem;

                displayItemBottom.Draw(spriteBatch, movement, color);
                if (displayHUD1) {
                    displayItemTop.Draw(spriteBatch, movement, color);
                    selectionSquare1.location = new Vector2(displayItemTop.selectionRectangle.X, displayItemTop.selectionRectangle.Y);
                }
            }

            if (displayItemTop2 != null)
            {
                displayItemBottom2 = displayItemTop2.copyOf();
                displayItemBottom2.location = BBottomBox2;
                displayItemTop2.location = BUpperBox;
                inv[1].EquippedItem = displayItemTop2.myItem;

                displayItemBottom2.Draw(spriteBatch, movement, color);
                if (displayHUD2) {
                    displayItemTop2.Draw(spriteBatch, movement, color);
                    selectionSquare2.location = new Vector2(displayItemTop2.selectionRectangle.X, displayItemTop2.selectionRectangle.Y);
                }
            }

            if (displayHUD1 || !twoPlayers) {
                selectionSquare1.Draw(spriteBatch, movement, color);
            } else if (displayHUD2) {
                selectionSquare2.Draw(spriteBatch, movement, color);
            }

            linkDot.Draw(spriteBatch, movement, color);
        }
    }
}
