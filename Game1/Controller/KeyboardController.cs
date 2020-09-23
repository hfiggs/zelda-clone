/* Author: Hunter Figgs */

using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Game1.Command;

namespace Game1.Controller
{
    class KeyboardController : IController
    {
        private Dictionary<Keys, ICommand> commands;

        public KeyboardController(Game1 game)
        {
            commands = new Dictionary<Keys, ICommand>
            {
                { Keys.D0, new QuitCommand(game) },
                { Keys.W, new PlayerUpCommand(game) },
                { Keys.A, new PlayerLeftCommand(game) },
                { Keys.S, new PlayerDownCommand(game) },
                { Keys.D, new PlayerRightCommand(game) },
                { Keys.Up, new PlayerUpCommand(game) },
                { Keys.Left, new PlayerLeftCommand(game) },
                { Keys.Down, new PlayerDownCommand(game) },
                { Keys.Right, new PlayerRightCommand(game) },
                { Keys.Space, new PlayerAttackCommand(game) },
                { Keys.D1, new PlayerUseItemCommand(game, 1) },
                { Keys.NumPad1, new PlayerUseItemCommand(game, 1) },
                { Keys.D2, new PlayerUseItemCommand(game, 2) },
                { Keys.NumPad2, new PlayerUseItemCommand(game, 2) },
                { Keys.D3, new PlayerUseItemCommand(game, 3) },
                { Keys.NumPad3, new PlayerUseItemCommand(game, 3) },
                { Keys.D4, new PlayerUseItemCommand(game, 4) },
                { Keys.NumPad4, new PlayerUseItemCommand(game, 4) }
            };
        }

        public void Update()
        {
            var keys = Keyboard.GetState().GetPressedKeys();

            foreach(Keys k in keys)
            {
                if (commands.ContainsKey(k))
                {
                    commands[k].Execute();
                }
            }
        }
    }
}
