/* Author: Hunter Figgs */

using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Game1.Command;

namespace Game1.Controller
{
    class KeyboardController : IController
    {
        private Dictionary<Keys, ICommand> commands;
        private Stack<Keys> movement = new Stack<Keys>();
        private Keys currentMove = new Keys();

        public KeyboardController(Game1 game)
        {
            commands = new Dictionary<Keys, ICommand>
            {
                { Keys.Q, new QuitCommand(game) },
                //{ Keys.R, new ResetCommand(game) },

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

                { Keys.E, new PlayerDamageCommand(game) },

                { Keys.D1, new PlayerUseItemCommand(game, 1) },
                { Keys.NumPad1, new PlayerUseItemCommand(game, 1) },
                { Keys.D2, new PlayerUseItemCommand(game, 2) },
                { Keys.NumPad2, new PlayerUseItemCommand(game, 2) },
                { Keys.D3, new PlayerUseItemCommand(game, 3) },
                { Keys.NumPad3, new PlayerUseItemCommand(game, 3) },

                //{ Keys.Y, new EnvironmentNextCommand(game) },
                //{ Keys.T, new EnvironmentPrevCommand(game) },

                //{ Keys.U, new ItemPrevCommand(game) },
                //{ Keys.I, new ItemNextCommand(game) },

                //{ Keys.O, new EnemyPrevCommand(game) },
                //{ Keys.P, new EnemyNextCommand(game) }
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

