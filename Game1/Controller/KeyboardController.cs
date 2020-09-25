/* Author: Hunter Figgs */

using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Game1.Command;
using System.Runtime.InteropServices;
using System.CodeDom;
using System.Linq;

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

                switch (k)
                {
                    case Keys.W:
                        movement.Push(Keys.W);
                        break;
                    case Keys.A:
                        movement.Push(Keys.A);
                        break;
                    case Keys.S:
                        movement.Push(Keys.S);
                        break;
                    case Keys.D:
                        movement.Push(Keys.D);
                        break;
                    case Keys.Up:
                        movement.Push(Keys.Up);
                        break;
                    case Keys.Down:
                        movement.Push(Keys.Down);
                        break;
                    case Keys.Left:
                        movement.Push(Keys.Left);
                        break;
                    case Keys.Right:
                        movement.Push(Keys.Right);
                        break;
                    default:
                        if(commands.ContainsKey(k))
                            commands[k].Execute();
                        break;
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
