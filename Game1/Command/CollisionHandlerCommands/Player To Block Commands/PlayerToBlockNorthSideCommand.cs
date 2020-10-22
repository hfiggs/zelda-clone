

using Game1.Collision_Handling;
using Game1.Environment;
using Game1.Player;
using Microsoft.Xna.Framework;

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
            if (envo.GetType() == typeof(DoorNOpen) || envo.GetType() == typeof(DoorNHole))
                envo.BehaviorUpdate();
            else
            {
                IPlayer player = (IPlayer)collision.collider;
                Vector2 moveAmount = new Vector2(0, -collision.intersectionRec.Y);
                player.editPosition(moveAmount);
            }
        }
    }
}
