﻿/* Author: Hunter Figgs */

using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Game1.Command;

namespace Game1.Controller
{
    class KeyboardQuitController : IController
    {
        private readonly Dictionary<Keys, ICommand> commands;

        public KeyboardQuitController(Game1 game)
        {
            commands = new Dictionary<Keys, ICommand>
            {
                { Keys.Q, new QuitCommand(game) },
                {Keys.F1, new MuteUnmuteCommand(game) },
                {Keys.F2, new VolumeDownCommand(game) },
                {Keys.F3, new VolumeUpCommand(game) },
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

