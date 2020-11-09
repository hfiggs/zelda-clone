using Game1.GameState;
using System.Diagnostics;

namespace Game1.Command
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
            if (stopWatch.ElapsedMilliseconds >= cooldown)
            {
                game.SetState(new GameStateRoomToHUD(game));

                stopWatch.Restart();
            }
        }
    }
}
