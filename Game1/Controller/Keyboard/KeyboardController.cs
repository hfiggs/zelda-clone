/* Author: Hunter Figgs */

using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Game1.Command;
using Game1.Player.PlayerInventory;

namespace Game1.Controller
{
    class KeyboardController : IController
    {
        private readonly Dictionary<Keys, ICommand> commands;
        private readonly Stack<Keys> movement = new Stack<Keys>();
        private Keys currentMove = new Keys();

        public KeyboardController(Game1 game)
        {
            commands = new Dictionary<Keys, ICommand>
            {
                { Keys.Q, new QuitCommand(game) },
                { Keys.LeftShift, new PauseGameCommand(game) },
                { Keys.RightShift, new PauseGameCommand(game) },

                { Keys.W, new PlayerUpCommand(game) },
                { Keys.A, new PlayerLeftCommand(game) },
                { Keys.S, new PlayerDownCommand(game) },
                { Keys.D, new PlayerRightCommand(game) },
                { Keys.Up, new PlayerUpCommand(game) },
                { Keys.Left, new PlayerLeftCommand(game) },
                { Keys.Down, new PlayerDownCommand(game) },
                { Keys.Right, new PlayerRightCommand(game) },

                { Keys.Z, new PlayerAttackCommand(game) },
                { Keys.N, new PlayerAttackCommand(game) },

                { Keys.X, new PlayerUseItemCommand(game) },
                { Keys.M, new PlayerUseItemCommand(game) },

                { Keys.D1, new PlayerEquipItemCommand(game, ItemEnum.Bow) },
                { Keys.D2, new PlayerEquipItemCommand(game, ItemEnum.Boomerang) },
                { Keys.D3, new PlayerEquipItemCommand(game, ItemEnum.Bomb) },
                { Keys.D4, new PlayerEquipItemCommand(game, ItemEnum.BluePotion) },
                { Keys.D5, new PlayerEquipItemCommand(game, ItemEnum.BlueCandle) },

                { Keys.NumPad1, new PlayerEquipItemCommand(game, ItemEnum.Bow) },
                { Keys.NumPad2, new PlayerEquipItemCommand(game, ItemEnum.Boomerang) },
                { Keys.NumPad3, new PlayerEquipItemCommand(game, ItemEnum.Bomb) },
                { Keys.NumPad4, new PlayerEquipItemCommand(game, ItemEnum.BluePotion) },
                { Keys.NumPad5, new PlayerEquipItemCommand(game, ItemEnum.BlueCandle) },

                {Keys.Escape, new EnterHUDStateCommand(game) },
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

                if (k == Keys.W || k == Keys.A || k == Keys.S || k == Keys.D || k == Keys.Up || k == Keys.Down || k == Keys.Left || k == Keys.Right)
                {
                    movement.Push(k);
                }
                else
                {
                    if (commands.ContainsKey(k))
                        commands[k].Execute();
                }

            }

            if (movement.Count == 1)
            {
                Keys keyCheck = movement.Pop();
                currentMove = keyCheck;
                commands[keyCheck].Execute();
            }
            else
            {
                while (movement.Count > 0)
                {
                    Keys keyCheck = movement.Pop();
                    if (currentMove != keyCheck)
                    {
                        commands[keyCheck].Execute();
                        break;
                    }
                }
            }

            movement.Clear();
        }
    }
}

