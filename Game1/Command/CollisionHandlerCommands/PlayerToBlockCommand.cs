﻿/* Author: Hunter Figgs */

using Game1.Collision_Handling;
using Game1.Environment;
using Game1.Player;
using Game1.Util;
using Microsoft.Xna.Framework;

namespace Game1.Command.CollisionHandlerCommands
{
    class PlayerToBlockCommand : ICollisionCommand
    {
        private readonly Game1 game;

        private readonly Vector2 negativeVector = new Vector2(-1, -1);
        private const float moveBlockTime = 1.0f;

        public PlayerToBlockCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute(Collision collision)
        {
            IEnvironment enviro = (IEnvironment)collision.collidee;
            IPlayer player = (IPlayer)collision.collider;
            char side = collision.side;

            // side is side of block (collidee)

            if (enviro.GetType() == typeof(MovableBlock) && !((MovableBlock)enviro).hasMoved)
            {
                ((MovableBlock)enviro).Move(Vector2.Multiply(CompassDirectionUtil.GetDirectionVector(side), negativeVector), moveBlockTime);
            }
            else if (enviro.GetType() == typeof(Stairs))
            {
                AudioManager.PlayMutex("stairs");
                // TODO: GameStateRoomToBasement
            }
            else
            {
                RoomUtil.OpenLockedDoor(game.Screen, enviro, player);

                RoomUtil.EnterDoor(game, enviro, player);

                Vector2 moveAmount = Vector2.Multiply(new Vector2(collision.intersectionRec.Width, collision.intersectionRec.Height), CompassDirectionUtil.GetDirectionVector(side));
                player.EditPosition(moveAmount);
            }

            if (player.GetType() == typeof(DamagedPlayer) && ((DamagedPlayer)player).stillSlide)
            {
                ((DamagedPlayer)player).stopKnockback(new Vector2(collision.intersectionRec.Width, collision.intersectionRec.Height));
            }

        }
    }
}
