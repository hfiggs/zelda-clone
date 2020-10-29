/* Author: Hunter Figgs */

using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Game1.Command;
using Microsoft.Xna.Framework;
using System;

namespace Game1.Controller
{
    class GamepadController : IController
    {
        private Dictionary<Buttons, ICommand> commands;
        private Stack<Buttons> currentMovementButtons = new Stack<Buttons>();
        private Buttons currentMove = new Buttons();

        private PlayerIndex playerIndex;

        private readonly HashSet<Buttons> movementButtons;

        public GamepadController(Game1 game, PlayerIndex playerIndex)
        {
            commands = new Dictionary<Buttons, ICommand>
            {
                { Buttons.Back, new QuitCommand(game) },

                { Buttons.DPadUp, new PlayerUpCommand(game) },
                { Buttons.DPadLeft, new PlayerLeftCommand(game) },
                { Buttons.DPadDown, new PlayerDownCommand(game) },
                { Buttons.DPadRight, new PlayerRightCommand(game) },
                { Buttons.LeftThumbstickUp, new PlayerUpCommand(game) },
                { Buttons.LeftThumbstickLeft, new PlayerLeftCommand(game) },
                { Buttons.LeftThumbstickDown, new PlayerDownCommand(game) },
                { Buttons.LeftThumbstickRight, new PlayerRightCommand(game) },

                { Buttons.A, new PlayerAttackCommand(game) },

                { Buttons.B, new PlayerUseItemCommand(game, 1) },
                { Buttons.X, new PlayerUseItemCommand(game, 2) },
                { Buttons.Y, new PlayerUseItemCommand(game, 3) },

                { Buttons.RightShoulder, new RoomNextCommand(game) },
                { Buttons.LeftShoulder, new RoomPrevCommand(game) }
            };

            this.playerIndex = playerIndex;

            movementButtons = new HashSet<Buttons>()
            {
                Buttons.DPadUp,
                Buttons.DPadLeft,
                Buttons.DPadDown,
                Buttons.DPadRight,
                Buttons.LeftThumbstickUp,
                Buttons.LeftThumbstickLeft,
                Buttons.LeftThumbstickDown,
                Buttons.LeftThumbstickRight
            };
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
                if (movementButtons.Contains(b))
                {
                    currentMovementButtons.Push(b);
                }
                else
                {
                    if (commands.ContainsKey(b))
                        commands[b].Execute();
                }
            }

            if (currentMovementButtons.Count == 1)
            {
                currentMove = currentMovementButtons.Pop();
                commands[currentMove].Execute();
            }
            else
            {
                while (currentMovementButtons.Count > 0)
                {
                    Buttons buttonCheck = currentMovementButtons.Pop();
                    if (currentMove != buttonCheck)
                    {
                        commands[buttonCheck].Execute();
                        break;
                    }
                }
            }

            currentMovementButtons.Clear();
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

