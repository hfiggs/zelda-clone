﻿/* Author: Hunter Figgs */

using Game1.Audio;
using Game1.Command;
using Game1.HUD;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Game1.Controller
{
    internal class SelectItemRightCommand : ICommand
    {
        private Game1 game;
        private Stopwatch stopWatch;
        Point SettingPoint = new Point(173, 14);
        private const int cooldown = 250; // ms
        const int centerPointModifier = 20;

        const int xDiffTRL = 203, yDiffTRL = 4, widthAndHeightTRL = 20;
        private Rectangle topRightLimit = new Rectangle(xDiffTRL, yDiffTRL, widthAndHeightTRL, widthAndHeightTRL);
        const int xDiffBRL = 203, yDiffBRL = 23, widthAndHeightBRL = 20;
        private Rectangle bottomRightLimit = new Rectangle(xDiffBRL, yDiffBRL, widthAndHeightBRL, widthAndHeightBRL);
        const int xDiffTLL = 143, yDiffTLL = 4, widthAndHeightTLL = 20;
        private Rectangle topLeftLimit = new Rectangle(xDiffTLL, yDiffTLL, widthAndHeightTLL, widthAndHeightTLL);
        const int xDiffBLL = 143, yDiffBLL = 23, widthAndHeightBLL = 20;
        private Rectangle bottomLeftLimit = new Rectangle(xDiffBLL, yDiffBLL, widthAndHeightBLL, widthAndHeightBLL);

        public SelectItemRightCommand(Game1 game)
        {
            this.game = game;
            stopWatch = new Stopwatch();
            stopWatch.Start();
        }

        public void Execute()
        {
            if (stopWatch.ElapsedMilliseconds >= cooldown)
            {
                AudioManager.PlayFireForget("rupeePickUp");
                if (game.HUD.displayHUD1) {
                    ExecutePlayer1();
                } else {
                    ExecutePlayer2();
                }
                stopWatch.Restart();
            }
        }

        private void ExecutePlayer1()
        {
            if (game.HUD.displayItemTop != null) {
                bool swappedSuccess = false;
                Point centerPoint = game.HUD.displayItemTop.selectionRectangle.Center;
                centerPoint = DetermineNextCenterPoint(centerPoint);

                while (!swappedSuccess) {
                    foreach (IHudItem item in game.HUD.Items[0]) {
                        if (item.selectionRectangle.Contains(centerPoint)) {
                            game.HUD.displayItemTop = item.copyOf();
                            swappedSuccess = true;
                        }
                    }

                    if (!swappedSuccess) {
                        centerPoint = DetermineNextCenterPoint(centerPoint);
                    }
                }
            } else {
                foreach (IHudItem item in game.HUD.Items[0]) {
                    if (item.selectionRectangle.Contains(SettingPoint)) {
                        game.HUD.displayItemTop = item.copyOf();
                    }
                }
            }
        }

        private void ExecutePlayer2()
        {
            if (game.HUD.displayItemTop2 != null) {
                bool swappedSuccess = false;
                Point centerPoint = game.HUD.displayItemTop2.selectionRectangle.Center;
                centerPoint = DetermineNextCenterPoint(centerPoint);

                while (!swappedSuccess) {
                    foreach (IHudItem item in game.HUD.Items[1]) {
                        if (item.selectionRectangle.Contains(centerPoint)) {
                            game.HUD.displayItemTop2 = item.copyOf();
                            swappedSuccess = true;
                        }
                    }

                    if (!swappedSuccess) {
                        centerPoint = DetermineNextCenterPoint(centerPoint);
                    }
                }
            } else {
                foreach (IHudItem item in game.HUD.Items[1]) {
                    if (item.selectionRectangle.Contains(SettingPoint)) {
                        game.HUD.displayItemTop2 = item.copyOf();
                    }
                }
            }
        }

        private Point DetermineNextCenterPoint(Point centerPoint)
        {
            Point newCenterPoint = centerPoint;
            const int boundryX = 150, boundryY = 4, boundryWidthAndHeight = 200;
            Rectangle boundry = new Rectangle(boundryX, boundryY, boundryWidthAndHeight, boundryWidthAndHeight);

            if (topRightLimit.Contains(centerPoint))
            {
                newCenterPoint = bottomLeftLimit.Center;
            } else if (bottomRightLimit.Contains(centerPoint)) {
                newCenterPoint = topLeftLimit.Center;
            } else if (boundry.Contains(centerPoint)) {
                newCenterPoint.X = centerPoint.X + centerPointModifier;
            } else {
                newCenterPoint = topLeftLimit.Center;
            }

            return newCenterPoint;
        }
    }
}
