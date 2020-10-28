

using Game1.Collision_Handling;
using Game1.Environment;
using Game1.Player;
using Microsoft.Xna.Framework;
using System;

namespace Game1.Command.CollisionHandlerCommands
{
    class PlayerToBlockEastSideCommand : ICollisionCommand
    {
        public PlayerToBlockEastSideCommand()
        {

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
                    player.editPosition(moveAmount);
                }
            }
            else if (envo.GetType() == typeof(DoorWOpen))
            {
                /* Collision with Open Door. Allowing walk through */;
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
                    player.editPosition(moveAmount);
                }
            } else if (envo.GetType() == typeof(Stairs)) {
                // Do nothing until player can walk down stairs
            } else {

                if (player.GetType() == typeof(DamagedPlayer) && ((DamagedPlayer)player).stillSlide)
                {
                    ((DamagedPlayer)player).stopKnockback(new Vector2(collision.intersectionRec.Width,collision.intersectionRec.Height));
                }
                else
                {
                    Vector2 moveAmount = new Vector2(collision.intersectionRec.Width, 0);
                    player.editPosition(moveAmount);
                }
            }
        }
    }
}
