

using Game1.Collision_Handling;
using Game1.Environment;
using Game1.Player;
using Microsoft.Xna.Framework;

namespace Game1.Command.CollisionHandlerCommands
{
    class PlayerToBlockWestSideCommand : ICollisionCommand
    {
        public PlayerToBlockWestSideCommand()
        {

        }

        public void Execute(Collision collision)
        {
            IEnvironment envo = (IEnvironment)collision.collidee;
            IPlayer player = (IPlayer)collision.collider;
            if (envo.GetType() == typeof(DoorEBombable) && ((DoorEBombable)envo).open)
                throw new System.NotImplementedException();
            else if (envo.GetType() == typeof(MovableBlock))
            {
                ((MovableBlock)envo).Move(new Vector2(1, 0), 1.0f, 'E');
            }
            else if (envo.GetType() == typeof(DoorELocked))
            {
                if (((DoorELocked)envo).open)
                    throw new System.NotImplementedException();
                else if (player.PlayerInventory.SubKey())
                    ((DoorELocked)envo).Open();
            }
            else if(envo.GetType() == typeof(DoorEOpen))
            {
                throw new System.NotImplementedException();
            }
            else
            {
                Vector2 moveAmount = new Vector2(-collision.intersectionRec.Width, 0);
                player.editPosition(moveAmount);
            }
        }
    }
}
