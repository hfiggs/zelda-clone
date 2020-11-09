/* Author: Hunter Figgs */

using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Game1.Command;
using Microsoft.Xna.Framework;
using System;

namespace Game1.Controller
{
    class HUDGamepadController : IController
    {
        private readonly Dictionary<Buttons, ICommand> commands;

        private readonly PlayerIndex playerIndex;

        public HUDGamepadController(Game1 game, PlayerIndex playerIndex)
        {
            commands = new Dictionary<Buttons, ICommand>
            {
                { Buttons.Back, new QuitCommand(game) },
            };

            this.playerIndex = playerIndex;
        }

        public void Update()
        {
            if(!GamePad.GetState(playerIndex).IsConnected)
            {
                return;
            }

            var buttons = GetPressedButtons(GamePad.GetState(playerIndex));

            foreach(Buttons b in buttons)
            {
                if (commands.ContainsKey(b))
                    commands[b].Execute();
            }
        }

        private HashSet<Buttons> GetPressedButtons(GamePadState gamePadState)
        {
            var buttons = new HashSet<Buttons>();

            foreach(Buttons b in Enum.GetValues(typeof(Buttons)))
            {
                if (gamePadState.IsButtonDown(b))
                {
                    buttons.Add(b);
                }
            }

            return buttons;
        }
    }
}

