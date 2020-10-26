

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
                ((MovableBlock)envo).Move(new Vector2(0, -1), .8f, 'N');
            }
            else if (envo.GetType() == typeof(DoorNLocked))
            {
                if (((DoorNLocked)envo).open)
                System.Console.WriteLine("Collision with Open Door. Allowing walk through.");
                else if (player.PlayerInventory.SubKey())
                    ((DoorNLocked)envo).Open();
                else
                {
                    Vector2 moveAmount = new Vector2(0, collision.intersectionRec.Height);
                    player.editPosition(moveAmount);
                }
            }
            else if (envo.GetType() == typeof(DoorNOpen))
            {
                System.Console.WriteLine("Collision with Open Door. Allowing walk through.");
            }
            else if (envo.GetType() == typeof(DoorNBombable))
            {
                if(((DoorNBombable)envo).open)
                {

                }
                else
                {
                    Vector2 moveAmount = new Vector2(0, collision.intersectionRec.Height);
                    player.editPosition(moveAmount);
                }
            }
            else
            {
                Vector2 moveAmount = new Vector2(0, collision.intersectionRec.Height);
                player.editPosition(moveAmount);
            }
        }
    }
}
