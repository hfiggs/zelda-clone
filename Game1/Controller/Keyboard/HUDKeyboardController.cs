/* Author: Hunter Figgs */

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
                {Keys.Escape, new ExitHUDStateCommand(game) },

                { Keys.A, new SelectItemLeftCommand(game) },
                { Keys.D, new SelectItemRightCommand(game) },

                { Keys.Left, new SelectItemLeftCommand(game) },
                { Keys.Right, new SelectItemRightCommand(game) },

                { Keys.F1, new MuteUnmuteCommand(game) },
                { Keys.F2, new VolumeDownCommand(game) },
                { Keys.F3, new VolumeUpCommand(game) },
                { Keys.F4, new ToggleFullscreenCommand(game) }
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

