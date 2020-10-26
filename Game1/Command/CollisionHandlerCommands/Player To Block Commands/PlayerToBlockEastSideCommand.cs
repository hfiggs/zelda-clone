

using Game1.Collision_Handling;
using Game1.Environment;
using Game1.Player;
using Microsoft.Xna.Framework;

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
                ((MovableBlock)envo).Move(new Vector2(-1, 0), .8f, 'W');
            }
            else if (envo.GetType() == typeof(DoorWLocked))
            {
                if (((DoorWLocked)envo).open)
                    System.Console.WriteLine("Collision with Open Door. Allowing walk through.");
                else if (player.PlayerInventory.SubKey())
                    ((DoorWLocked)envo).Open();
                else
                {
                    Vector2 moveAmount = new Vector2(collision.intersectionRec.Width, 0);
                    player.editPosition(moveAmount);
                }
            }
            else if (envo.GetType() == typeof(DoorWOpen))
            {
                System.Console.WriteLine("Collision with Open Door. Allowing walk through.");
            }
            else if (envo.GetType() == typeof(DoorWBombable))
            {
                if(((DoorWBombable)envo).open)
                {
                    System.Console.WriteLine("Collision with Open Door. Allowing walk through.");
                }
                else
                {
                    Vector2 moveAmount = new Vector2(collision.intersectionRec.Width, 0);
                    player.editPosition(moveAmount);
                }
            }
            else
            {
                Vector2 moveAmount = new Vector2(collision.intersectionRec.Width, 0);
                player.editPosition(moveAmount);
            }
        }
    }
}
