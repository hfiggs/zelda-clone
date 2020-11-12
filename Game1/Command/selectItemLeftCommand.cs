
/* Author: Hunter Figgs */

using Game1.Command;
using Game1.HUD;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Game1.Controller
{
    internal class selectItemLeftCommand : ICommand
    {
        private Game1 game;
        private Stopwatch stopWatch;
        private const int cooldown = 250; // ms
        Point SettingPoint = new Point(193, 14);
        public selectItemLeftCommand(Game1 game)
        {
            this.game = game;
            stopWatch = new Stopwatch();
            stopWatch.Start();
        }

        public void Execute()
        {
            if (stopWatch.ElapsedMilliseconds >= cooldown)
            {
                if (game.HUD.displayItemTop != null)
                {
                    bool swappedSuccess = false;
                    Point centerPoint = game.HUD.displayItemTop.selectionRectangle.Center;
                    centerPoint.X = centerPoint.X - 20;
                    foreach (IHudItem item in game.HUD.Items)
                    {
                        if (item.selectionRectangle.Contains(centerPoint))
                        {
                            game.HUD.displayItemTop = item.copyOf();
                            swappedSuccess = true;
                        }
                    }

                    if (!swappedSuccess)
                    {
                        centerPoint = game.HUD.displayItemTop.selectionRectangle.Center;
                        centerPoint.X = centerPoint.X + 40;
                        foreach(IHudItem item in game.HUD.Items)
                        {
                            if(item.selectionRectangle.Contains(centerPoint))
                            {
                                game.HUD.displayItemTop = item.copyOf();
                            }
                        }
                    }
                }
                else
                {
                    foreach(IHudItem item in game.HUD.Items)
                    {
                        if(item.selectionRectangle.Contains(SettingPoint))
                        {
                            game.HUD.displayItemTop = item.copyOf();
                        }
                    }
                }
                stopWatch.Restart();
            }
        }
    }
}