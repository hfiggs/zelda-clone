/* Author: Hunter Figgs.3 */

using Game1.Audio;
using Game1.GameState;
using Game1.RoomLoading;
using System;
using System.Diagnostics;

namespace Game1.Command
{
    class StartGameCommand : ICommand
    {
        Game1 game;
        private Stopwatch stopWatch;
        private const int cooldown = 250; // ms

        public StartGameCommand(Game1 game)
        {
            this.game = game;
            stopWatch = new Stopwatch();
            stopWatch.Start();
        }

        public void Execute()
        {
            if (stopWatch.ElapsedMilliseconds >= cooldown)
            {
                if (game.State.GetType() == typeof(GameStateStart))
                {
                    game.SetMode((game.State as GameStateStart).GetOption());
                    game.SetState(new GameStateStartDifficulty(game));
                }
                else
                {
                    game.Screen.LoadAllRooms((game.State as GameStateStartDifficulty).GetOption());
                    game.SetState(new GameStateStartToSpawn(game));
                }
                
               

                stopWatch.Restart();
            }
        }
    }
}
