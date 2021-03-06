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
            IEnvironment enviro = (IEnvironment)collision.Collidee;
            IPlayer player = (IPlayer)collision.Collider;
            var side = collision.Side;

            // side is side of block (collidee)

            if (enviro is PortalBlock portal && (portal.State == PortalBlockState.Blue || portal.State == PortalBlockState.Orange))
            {
                PortalUtil.HandlePlayerPortal(portal, player, game.Screen.CurrentRoom);
            }
            else if (enviro is MovableBlock mB && !(mB.hasMoved) && mB.Pushable)
            {
                mB.Move(Vector2.Multiply(CompassDirectionUtil.GetDirectionVector(side), negativeVector), moveBlockTime);
            }
            else if (enviro is Stairs)
            {
                // Do nothing
            }
            else
            {
                RoomUtil.OpenLockedDoor(game.Screen, enviro, player);

                RoomUtil.EnterDoor(game, enviro, player.playerID);

                RoomUtil.EnterExitDungeon(game, enviro);

                RoomUtil.EnterExitBasement(game, enviro);

                Vector2 moveAmount = Vector2.Multiply(new Vector2(collision.IntersectionRec.Width, collision.IntersectionRec.Height), CompassDirectionUtil.GetDirectionVector(side));
                player.EditPosition(moveAmount);
            }

            if (player is DamagedPlayer dP && dP.stillSlide)
            {
                dP.StopKnockback(new Vector2(collision.IntersectionRec.Width, collision.IntersectionRec.Height));
            }

        }
    }
}

