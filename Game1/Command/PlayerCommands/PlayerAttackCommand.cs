using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Command
{
    class PlayerAttackCommand : ICommand
    {
        private Game1 game;
        private PlayerIndex playerIndex;

        public PlayerAttackCommand(Game1 game, PlayerIndex playerIndex = PlayerIndex.One)
        {
            this.game = game;
            this.playerIndex = playerIndex;
        }

        public void Execute()
        {
            game.Screen.Players[(int)playerIndex].Attack();
        }
    }
}
