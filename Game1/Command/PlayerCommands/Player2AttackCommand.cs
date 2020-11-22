using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Command
{
    class Player2AttackCommand : ICommand
    {
        private Game1 game;

        public Player2AttackCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.Screen.Player2.Attack();
        }
    }
}
