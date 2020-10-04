using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Game1.Command;
using System;

namespace Game1.Controller
{
    class MouseController : IController
    {
        private enum Input
        {
            Quit = 0,
            Q1 = 3,
            Q2 = 4,
            Q3 = 1,
            Q4 = 2,
            Nothing = 5
        }

        private Dictionary<Input, ICommand> commands;
        private Game1 game;

        public MouseController(Game1 game)
        {
            commands = new Dictionary<Input, ICommand>
            {
                { Input.Quit, new QuitCommand(game) }
            };

            this.game = game;
        }

        public void Update()
        {
            var mState = Mouse.GetState();

            Input q = getMouseQuad(mState);

            commands[q].Execute();
        }

        private Input getMouseQuad(MouseState mState)
        {
            Input q = (Input)5;

            if (mState.RightButton == ButtonState.Pressed)
            {
                q = Input.Quit;
            }
            else if (mState.LeftButton == ButtonState.Pressed)
            {
                var xPos = mState.X - (float)game.Graphics.PreferredBackBufferWidth / 2;
                var yPos = mState.Y - (float)game.Graphics.PreferredBackBufferHeight / 2;

                // This function maps cartesian coordinates of the mouse to 4 integer values, one for each quadrant
                // Q1 = 3, Q2 = 4, Q3 = 1, Q4 = 2
                q = (Input)((xPos / (2 * Math.Abs(xPos))) - (yPos / Math.Abs(yPos)) + (5f / 2));
            }

            /*if (mState.RightButton == ButtonState.Pressed)
            {
                q = Input.Quit;
            }
            else if (mState.LeftButton == ButtonState.Pressed)
            {
                if (mState.X <= (float)game.Graphics.PreferredBackBufferWidth / 2)
                {
                    if (mState.Y <= (float)game.Graphics.PreferredBackBufferHeight / 2)
                    {
                        q = Input.Q1;
                    }
                    else
                    {
                        q = Input.Q3;
                    }
                }
                else
                {
                    if (mState.Y <= (float)game.Graphics.PreferredBackBufferHeight / 2)
                    {
                        q = Input.Q2;
                    }
                    else
                    {
                        q = Input.Q4;
                    }
                }
            }*/

            return q;
        }
    }
}
