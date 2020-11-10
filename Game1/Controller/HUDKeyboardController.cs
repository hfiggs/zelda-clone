﻿/* Author: Hunter Figgs */

using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Game1.Command;

namespace Game1.Controller
{
    class HUDKeyboardController : IController
    {
        private Dictionary<Keys, ICommand> commands;

        public HUDKeyboardController(Game1 game)
        {
            commands = new Dictionary<Keys, ICommand>
            {
                { Keys.Q, new QuitCommand(game) },

                {Keys.Escape, new UnpauseCommand(game) }
            };
        }

        public void Update()
        {
            var keys = Keyboard.GetState().GetPressedKeys();

            foreach (Keys k in keys)
            {
                if (commands.ContainsKey(k))
                    commands[k].Execute();
            }
        }
    }
}
