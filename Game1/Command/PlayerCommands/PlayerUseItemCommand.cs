using Microsoft.Xna.Framework;

namespace Game1.Command
{
    class PlayerUseItemCommand : ICommand
    {
        private Game1 game;
        private PlayerIndex playerIndex;

        public PlayerUseItemCommand(Game1 game, PlayerIndex playerIndex = PlayerIndex.One)
        {
            this.game = game;
            this.playerIndex = playerIndex;
        }

        public void Execute()
        {
            game.Screen.Players[(int)playerIndex].UseItem();
        }
    }
}
