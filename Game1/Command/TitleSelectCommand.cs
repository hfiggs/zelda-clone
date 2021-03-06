﻿/* Author: Hunter Figgs */

using Game1.Audio;
using Game1.GameState;
using System.Diagnostics;

namespace Game1.Command
{
    class TitleSelectCommand : ICommand
    {
        private Game1 game;
        private bool executed = false;

        public TitleSelectCommand(Game1 game)
        {
            this.game = game;
            executed = false;
        }
        public void Execute()
        {
            if(!executed)
            {
                if(game.State.GetType() == typeof(GameStateStart))
                {
                    (game.State as GameStateStart).MoveCursor();
                }
                else
                {
                    if(game.State as GameStateStartDifficulty != null)
                        (game.State as GameStateStartDifficulty).MoveCursor();
                }
                
                AudioManager.PlayFireForget("rupeeAddShort");
                AudioManager.PlayFireForget("shield");
                executed = true;
            }
        }

        public void KeyUp()
        {
            executed = false;
        }
    }
}
