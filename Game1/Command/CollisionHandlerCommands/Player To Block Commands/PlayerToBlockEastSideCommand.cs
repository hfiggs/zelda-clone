﻿using Game1.Collision_Handling;
using Game1.Environment;
using Game1.GameState;
using Game1.Player;
using Game1.Util;
using Microsoft.Xna.Framework;

namespace Game1.Command.CollisionHandlerCommands
{
    class PlayerToBlockEastSideCommand : ICollisionCommand
    {
        private readonly Game1 game;

        public PlayerToBlockEastSideCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute(Collision collision)
        {
            IEnvironment envo = (IEnvironment)collision.collidee;
            IPlayer player = (IPlayer)collision.collider;
            if (envo.GetType() == typeof(MovableBlock) && !((MovableBlock)envo).hasMoved)
            {
                ((MovableBlock)envo).Move(new Vector2(-1, 0), 1.0f);
            }
            else if (envo.GetType() == typeof(DoorWLocked))
            {
                if (((DoorWLocked)envo).open == 2)
                {
                    /* Collision with Open Door. Allowing walk through.*/
                }
                else if (((DoorWLocked)envo).open == 0 && player.PlayerInventory.SubKey())
                    ((DoorWLocked)envo).Open();
                else
                {
                    Vector2 moveAmount = new Vector2(collision.intersectionRec.Width, 0);
                    player.EditPosition(moveAmount);
                }
            }
            else if (envo.GetType() == typeof(DoorWOpen))
            {
                game.SetState(new GameStateRoomToRoom(game, CompassDirection.West));
            }
            else if (envo.GetType() == typeof(DoorWBombable))
            {
                if(((DoorWBombable)envo).open)
                {
                    /* Collision with Open Door. Allowing walk through. */;
                }
                else
                {
                    Vector2 moveAmount = new Vector2(collision.intersectionRec.Width, 0);
                    player.EditPosition(moveAmount);
                }
            } else if (envo.GetType() == typeof(Stairs)) {
                // Do nothing until player can walk down stairs
                AudioManager.PlayMutex("stairs");
            } else {

                if (player.GetType() == typeof(DamagedPlayer) && ((DamagedPlayer)player).stillSlide)
                {
                    ((DamagedPlayer)player).stopKnockback(new Vector2(collision.intersectionRec.Width,collision.intersectionRec.Height));
                }
                else
                {
                    Vector2 moveAmount = new Vector2(collision.intersectionRec.Width, 0);
                    player.EditPosition(moveAmount);
                }
            }
        }
    }
}
