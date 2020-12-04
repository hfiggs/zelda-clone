/* Author: Hunter Figgs */

using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Game1.Command;
using Game1.Player.PlayerInventory;
using System;
using Microsoft.Xna.Framework;

namespace Game1.Controller
{
    class KeyboardController : IController
    {
        private readonly Dictionary<Keys, ICommand> commands;
        private readonly Stack<Keys> movement = new Stack<Keys>();
        private readonly Stack<Keys> movement2 = new Stack<Keys>();
        private Keys currentMove = new Keys();
        private Keys currentMove2 = new Keys();

        public KeyboardController(Game1 game)
        {
            if (game.Mode == 1) {
                //MULTIPLAYER CONTROLS
                commands = new Dictionary<Keys, ICommand>
                {
                    { Keys.Q, new QuitCommand(game) },
                    { Keys.LeftShift, new PauseGameCommand(game) },
                    { Keys.RightShift, new PauseGameCommand(game) },

                    { Keys.W, new PlayerUpCommand(game, PlayerIndex.One) },
                    { Keys.A, new PlayerLeftCommand(game, PlayerIndex.One) },
                    { Keys.S, new PlayerDownCommand(game, PlayerIndex.One) },
                    { Keys.D, new PlayerRightCommand(game, PlayerIndex.One) },
                    { Keys.Up, new PlayerUpCommand(game, PlayerIndex.Two) },
                    { Keys.Left, new PlayerLeftCommand(game, PlayerIndex.Two) },
                    { Keys.Down, new PlayerDownCommand(game, PlayerIndex.Two) },
                    { Keys.Right, new PlayerRightCommand(game, PlayerIndex.Two) },

                    { Keys.Z, new PlayerAttackCommand(game, PlayerIndex.One) },
                    { Keys.N, new PlayerAttackCommand(game, PlayerIndex.Two) },

                    { Keys.X, new PlayerUseItemCommand(game, PlayerIndex.One) },
                    { Keys.M, new PlayerUseItemCommand(game, PlayerIndex.Two) },

                    { Keys.D1, new PlayerEquipItemCommand(game, ItemEnum.Bow, PlayerIndex.One) },
                    { Keys.D2, new PlayerEquipItemCommand(game, ItemEnum.Boomerang, PlayerIndex.One) },
                    { Keys.D3, new PlayerEquipItemCommand(game, ItemEnum.Bomb, PlayerIndex.One) },
                    { Keys.D4, new PlayerEquipItemCommand(game, ItemEnum.BluePotion, PlayerIndex.One) },
                    { Keys.D5, new PlayerEquipItemCommand(game, ItemEnum.BlueCandle, PlayerIndex.One) },

                    { Keys.NumPad1, new PlayerEquipItemCommand(game, ItemEnum.Bow, PlayerIndex.Two) },
                    { Keys.NumPad2, new PlayerEquipItemCommand(game, ItemEnum.Boomerang, PlayerIndex.Two) },
                    { Keys.NumPad3, new PlayerEquipItemCommand(game, ItemEnum.Bomb, PlayerIndex.Two) },
                    { Keys.NumPad4, new PlayerEquipItemCommand(game, ItemEnum.BluePotion, PlayerIndex.Two) },
                    { Keys.NumPad5, new PlayerEquipItemCommand(game, ItemEnum.BlueCandle, PlayerIndex.Two) },

                    {Keys.Escape, new EnterHUDStateCommand(game) },
                    {Keys.F1, new MuteUnmuteCommand(game) },
                    {Keys.F2, new VolumeDownCommand(game) },
                    {Keys.F3, new VolumeUpCommand(game) },
                    {Keys.F4, new ToggleFullscreenCommand(game) }
                };
            } 
            else
            {   
                //SINGLEPLAYER/AI CONTROLS
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
                        {Keys.F4, new ToggleFullscreenCommand(game) }
                    };

            }
        }

        public void Update()
        {
            int movementsExecuted = 0;
            var keys = Keyboard.GetState().GetPressedKeys();

            foreach (Keys k in keys)
            {

                if (k == Keys.W || k == Keys.A || k == Keys.S || k == Keys.D)
                {
                    movement.Push(k);
                }
                else if (k == Keys.Up || k == Keys.Down || k == Keys.Left || k == Keys.Right)
                {
                    movement2.Push(k);
                }
                else
                {
                    if (commands.ContainsKey(k))
                        commands[k].Execute();
                }
            }

            //player 1 multikey movement
            if (movement.Count == 1)
            {
                Keys keyCheck = movement.Pop();
                currentMove = keyCheck;
                commands[keyCheck].Execute();
                movementsExecuted++;
            }
            else
            {
                while (movement.Count > 0)
                {
                    Keys keyCheck = movement.Pop();
                    if (currentMove != keyCheck)
                    {
                        commands[keyCheck].Execute();
                        movementsExecuted++;
                    }
                }
            }

            //player 2 multikey movement
            if (movement2.Count == 1)
            {
                Keys keyCheck = movement2.Pop();
                currentMove2 = keyCheck;
                commands[keyCheck].Execute();
                movementsExecuted++;
            } else
            {
                while (movement2.Count > 0)
                {
                    Keys keyCheck = movement2.Pop();
                    if (currentMove2 != keyCheck)
                    {
                        commands[keyCheck].Execute();
                        movementsExecuted++;
                    }
                }
            }

            movement.Clear();
            movement2.Clear();
        }
    }
}

