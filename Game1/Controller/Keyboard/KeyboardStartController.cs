/* Author: Hunter Figgs */

using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Game1.Command;

namespace Game1.Controller
{
    class KeyboardStartController : IController
    {
        private readonly Dictionary<Keys, ICommand> commands;

        public KeyboardStartController(Game1 game)
        {
            commands = new Dictionary<Keys, ICommand>
            {
                { Keys.Q, new QuitCommand(game) },
                { Keys.Enter, new StartGameCommand(game) },
                {Keys.F1, new MuteUnmuteCommand(game) },
                {Keys.F2, new VolumeDownCommand(game) },
                {Keys.F3, new VolumeUpCommand(game) },
                {Keys.F4, new ToggleFullscreenCommand(game) },
                {Keys.LeftShift, new TitleSelectCommand(game) },
                {Keys.RightShift, new TitleSelectCommand(game) }
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

            var LShiftUp = Keyboard.GetState().IsKeyUp(Keys.LeftShift);
            var RShiftUp = Keyboard.GetState().IsKeyUp(Keys.RightShift);
            ICommand NotifiedCommand1;
            ICommand NotifiedCommand2;
            if (LShiftUp && RShiftUp)
            {
                commands.TryGetValue(Keys.LeftShift, out NotifiedCommand1);
                commands.TryGetValue(Keys.RightShift, out NotifiedCommand2);
                (NotifiedCommand1 as TitleSelectCommand).KeyUp();
                (NotifiedCommand2 as TitleSelectCommand).KeyUp();
            }
        }
    }
}

