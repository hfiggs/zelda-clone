/* Author: Hunter Figgs */

using Microsoft.Xna.Framework;

namespace Game1.Command
{
    class PlayerLeftCommand : ICommand
    {
        private Game1 game;
        private PlayerIndex playerIndex;

        public PlayerLeftCommand(Game1 game, PlayerIndex playerIndex = PlayerIndex.One)
        {
            this.game = game;
            this.playerIndex = playerIndex;
        }
        public void Execute()
        {
            game.Screen.Players[(int)playerIndex].MoveLeft();
        }
    }
}
