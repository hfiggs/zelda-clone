

using Game1.Collision_Handling;
using Game1.Environment;
using Game1.Player;
using Microsoft.Xna.Framework;

namespace Game1.Command.CollisionHandlerCommands
{
    class PlayerToBlockSouthSideCommand : ICollisionCommand
    {
        public PlayerToBlockSouthSideCommand()
        {

        }

        public void Execute(Collision collision)
        {
            IEnvironment envo = (IEnvironment)collision.collidee;
            IPlayer player = (IPlayer)collision.collider;
            if (envo.GetType() == typeof(MovableBlock) && !((MovableBlock)envo).hasMoved)
            {
                ((MovableBlock)envo).Move(new Vector2(0, -1), 0.8f);
            }
            else if (envo.GetType() == typeof(DoorNLocked))
            {
                if (((DoorNLocked)envo).open == 2)
                {
                    /* Collision with Open Door. Allowing walk through.*/
                }
                else if (((DoorNLocked)envo).open == 0 && player.PlayerInventory.SubKey())
                    ((DoorNLocked)envo).Open();
                else
                {
                    Vector2 moveAmount = new Vector2(0, collision.intersectionRec.Height);
                    player.EditPosition(moveAmount);
                }
            }
            else if (envo.GetType() == typeof(DoorNOpen))
            {
                /* Collision with Open Door. Allowing walk through. */;
            }
            else if (envo.GetType() == typeof(DoorNBombable))
            {
                if (((DoorNBombable)envo).open)
                {

                }
                else
                {
                    Vector2 moveAmount = new Vector2(0, collision.intersectionRec.Height);
                    player.EditPosition(moveAmount);
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
                    Vector2 moveAmount = new Vector2(0, collision.intersectionRec.Height);
                    player.EditPosition(moveAmount);
                }
            }
        }
    }
}
