/* Author: Hunter Figgs */

using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Game1.Command;
using Game1.Player.PlayerInventory;
using Microsoft.Xna.Framework;
using System;

namespace Game1.Controller
{
    class GamepadController : IController
    {
        private readonly Dictionary<Buttons, ICommand> commands;
        private readonly Stack<Buttons> currentMovementButtons = new Stack<Buttons>();
        private Buttons currentMove = new Buttons();

        private readonly PlayerIndex playerIndex;

        private readonly HashSet<Buttons> movementButtons;        

        public GamepadController(Game1 game, PlayerIndex playerIndex)
        {
            if (game.Mode == 1)
            {
                //MUTLIPLAYER CONTROLS (SHARED CONTROLLER)
                commands = new Dictionary<Buttons, ICommand>
                    {
                        { Buttons.Back, new QuitCommand(game) },
                        { Buttons.Start, new EnterHUDStateCommand(game) },

                        { Buttons.BigButton, new PauseGameCommand(game) },

                        { Buttons.RightThumbstickUp, new PlayerUpCommand(game, PlayerIndex.One) },
                        { Buttons.RightThumbstickLeft, new PlayerLeftCommand(game, PlayerIndex.One) },
                        { Buttons.RightThumbstickDown, new PlayerDownCommand(game, PlayerIndex.One) },
                        { Buttons.RightThumbstickRight, new PlayerRightCommand(game, PlayerIndex.One) },
                        { Buttons.LeftThumbstickUp, new PlayerUpCommand(game, PlayerIndex.Two) },
                        { Buttons.LeftThumbstickLeft, new PlayerLeftCommand(game, PlayerIndex.Two) },
                        { Buttons.LeftThumbstickDown, new PlayerDownCommand(game, PlayerIndex.Two) },
                        { Buttons.LeftThumbstickRight, new PlayerRightCommand(game, PlayerIndex.Two) },

                        { Buttons.RightTrigger, new PlayerAttackCommand(game, PlayerIndex.One) },
                        { Buttons.RightShoulder, new PlayerUseItemCommand(game, PlayerIndex.One) },
                        { Buttons.LeftTrigger, new PlayerAttackCommand(game, PlayerIndex.Two) },
                        { Buttons.LeftShoulder, new PlayerUseItemCommand(game, PlayerIndex.Two) },

                        { Buttons.Y, new PlayerEquipItemCommand(game, ItemEnum.Bow, PlayerIndex.One) },
                        { Buttons.B, new PlayerEquipItemCommand(game, ItemEnum.Boomerang, PlayerIndex.One) },
                        { Buttons.A, new PlayerEquipItemCommand(game, ItemEnum.Bomb, PlayerIndex.One) },
                        { Buttons.X, new PlayerEquipItemCommand(game, ItemEnum.BluePotion, PlayerIndex.One) },
                        { Buttons.DPadUp, new PlayerEquipItemCommand(game, ItemEnum.BluePotion, PlayerIndex.Two) },
                        { Buttons.DPadRight, new PlayerEquipItemCommand(game, ItemEnum.Boomerang, PlayerIndex.Two) },
                        { Buttons.DPadDown, new PlayerEquipItemCommand(game, ItemEnum.Bomb, PlayerIndex.Two) },
                        { Buttons.DPadLeft, new PlayerEquipItemCommand(game, ItemEnum.BluePotion, PlayerIndex.Two) },
                    };

                movementButtons = new HashSet<Buttons>()
                    {
                        Buttons.RightThumbstickUp,
                        Buttons.RightThumbstickLeft,
                        Buttons.RightThumbstickDown,
                        Buttons.RightThumbstickRight,
                        Buttons.LeftThumbstickUp,
                        Buttons.LeftThumbstickLeft,
                        Buttons.LeftThumbstickDown,
                        Buttons.LeftThumbstickRight
                    };
            }
            else
            {
                //SINGLEPLAYER/AI CONTROLS
                commands = new Dictionary<Buttons, ICommand>
                    {
                        { Buttons.Back, new QuitCommand(game) },
                        { Buttons.Start, new EnterHUDStateCommand(game) },

                        { Buttons.BigButton, new PauseGameCommand(game) },

                        { Buttons.DPadUp, new PlayerUpCommand(game) },
                        { Buttons.DPadLeft, new PlayerLeftCommand(game) },
                        { Buttons.DPadDown, new PlayerDownCommand(game) },
                        { Buttons.DPadRight, new PlayerRightCommand(game) },
                        { Buttons.LeftThumbstickUp, new PlayerUpCommand(game) },
                        { Buttons.LeftThumbstickLeft, new PlayerLeftCommand(game) },
                        { Buttons.LeftThumbstickDown, new PlayerDownCommand(game) },
                        { Buttons.LeftThumbstickRight, new PlayerRightCommand(game) },

                        { Buttons.A, new PlayerAttackCommand(game) },
                        { Buttons.B, new PlayerUseItemCommand(game) },

                        { Buttons.RightThumbstickLeft, new PlayerEquipItemCommand(game, ItemEnum.Bow) },
                        { Buttons.RightThumbstickUp, new PlayerEquipItemCommand(game, ItemEnum.Boomerang) },
                        { Buttons.RightThumbstickRight, new PlayerEquipItemCommand(game, ItemEnum.Bomb) },
                        { Buttons.RightThumbstickDown, new PlayerEquipItemCommand(game, ItemEnum.BluePotion) },
                    };

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

