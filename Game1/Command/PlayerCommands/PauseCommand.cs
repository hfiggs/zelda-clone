using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Command.PlayerCommands
{
    class PauseCommand : ICommand
    {
        Game1 game;
        private Stopwatch stopWatch;
        private const int cooldown = 250; // ms

        public PauseCommand(Game1 game)
        {
            this.game = game;
            stopWatch = new Stopwatch();
            stopWatch.Start();

        }

        public void Execute()
        {
            if(stopWatch.ElapsedMilliseconds >= cooldown)
            {
                if (game.Screen.HudPosition.Y != 0)
                {
                    game.Screen.HudPosition.Y = 0;
                    game.IsMouseVisible = true;
                    game.Screen.lockMouse = false;
                }
                else
                {
                    game.IsMouseVisible = false;
                    game.Screen.lockMouse = true;
                    game.Screen.HudPosition.Y = -136;
                }

                stopWatch.Restart();
            }
        }
    }
}
