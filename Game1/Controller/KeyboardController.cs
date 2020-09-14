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
                { Keys.D1, new SpriteNotAnimatedNotMovingCommand(game) },
                { Keys.D2, new SpriteAnimatedNotMovingCommand(game) },
                { Keys.D3, new SpriteNotAnimatedMovingCommand(game) },
                { Keys.D4, new SpriteAnimatedMovingCommand(game) }
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
