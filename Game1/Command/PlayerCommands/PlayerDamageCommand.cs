/* Author: Hunter Figgs */

using Microsoft.Xna.Framework;

namespace Game1.Command
{
    class PlayerDamageCommand : ICommand
    {
        private Game1 game;

        public PlayerDamageCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.Screen.Player.ReceiveDamage();
        }
    }
}
