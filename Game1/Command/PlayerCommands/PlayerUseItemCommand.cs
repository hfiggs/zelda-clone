using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Command
{
    class PlayerUseItemCommand : ICommand
    {
        private Game1 game;
        private int item;

        public PlayerUseItemCommand(Game1 game, int item)
        {
            this.game = game;
            this.item = item;
        }

        public void Execute()
        {
            game.Screen.Player.UseItem(item);
        }
    }
}
