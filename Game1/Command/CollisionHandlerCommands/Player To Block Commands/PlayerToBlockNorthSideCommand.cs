

using Game1.Collision_Handling;
using Game1.Environment;
using Game1.Player;
using Microsoft.Xna.Framework;
using SharpDX.MediaFoundation;

namespace Game1.Command.CollisionHandlerCommands
{
    class PlayerToBlockNorthSideCommand : ICollisionCommand
    {
        public PlayerToBlockNorthSideCommand()
        {

        }

        public void Execute(Collision collision)
        {
            IEnvironment envo = (IEnvironment)collision.collidee;
            IPlayer player = (IPlayer)collision.collider;
            if (envo.GetType() == typeof(MovableBlock) && !((MovableBlock)envo).hasMoved)
            {
                ((MovableBlock)envo).Move(new Vector2(0, 1), 1.0f);
            }
            else if (envo.GetType() == typeof(DoorSLocked))
            {
                if (((DoorSLocked)envo).open == 2)
                {
                    /* Collision with Open Door. Allowing walk through.*/
                }
                else if (((DoorSLocked)envo).open == 0 && player.PlayerInventory.SubKey())
                    ((DoorSLocked)envo).Open();
                else
                {
                    Vector2 moveAmount = new Vector2(0, -collision.intersectionRec.Height);
                    player.editPosition(moveAmount);
                }
            }
            else if (envo.GetType() == typeof(DoorSOpen))
            {
                /* Collision with Open Door. Allowing walk through. */;
            }
            else if (envo.GetType() == typeof(DoorSBombable))
            {
                if(((DoorSBombable)envo).open)
                {
                    /* Collision with Open Door. Allowing walk through. */;
                }
                else
                {
                    Vector2 moveAmount = new Vector2(0, -collision.intersectionRec.Height);
                    player.editPosition(moveAmount);
                }
            } else if (envo.GetType() == typeof(Stairs)) {
                // Do nothing until player can walk down stairs
            } else {

                if (player.GetType() == typeof(DamagedPlayer) && ((DamagedPlayer)player).stillSlide)
                {
                    ((DamagedPlayer)player).stopKnockback(new Vector2(collision.intersectionRec.Width, collision.intersectionRec.Height));
                }
                else
                {
                    Vector2 moveAmount = new Vector2(0, -collision.intersectionRec.Height);
                    player.editPosition(moveAmount);
                }
            }
        }
    }
}
