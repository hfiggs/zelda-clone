/* Author: Hunter Figgs */

using Microsoft.Xna.Framework;

namespace Game1.Command
{
    class PlayerUpCommand : ICommand
    {
        private Game1 game;
        private PlayerIndex playerIndex;

        public PlayerUpCommand(Game1 game, PlayerIndex playerIndex = PlayerIndex.One)
        {
            this.game = game;
            this.playerIndex = playerIndex;
        }
        public void Execute()
        {
            game.Screen.Players[(int)playerIndex].MoveUp();
        }
    }
}
