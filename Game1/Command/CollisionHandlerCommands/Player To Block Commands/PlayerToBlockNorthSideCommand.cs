

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
            if (envo.GetType() == typeof(DoorSBombable) && ((DoorSBombable)envo).open)
                System.Console.WriteLine("Collision with Open Door. Allowing walk through.");
            else if (envo.GetType() == typeof(MovableBlock))
            {
                ((MovableBlock)envo).Move(new Vector2(0,1), 1.0f, 'S');
            }
            else if (envo.GetType() == typeof(DoorSLocked))
            {
                if (((DoorSLocked)envo).open)
                System.Console.WriteLine("Collision with Open Door. Allowing walk through.");
                else if (player.PlayerInventory.SubKey())
                    ((DoorSLocked)envo).Open();
            }
            else if (envo.GetType() == typeof(DoorSOpen))
            {
                System.Console.WriteLine("Collision with Open Door. Allowing walk through.");
            }
            else
            {
                Vector2 moveAmount = new Vector2(0, -collision.intersectionRec.Height);
                player.editPosition(moveAmount);
            }
        }
    }
}
