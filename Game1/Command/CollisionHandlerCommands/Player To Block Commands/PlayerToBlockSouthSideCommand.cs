

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
            if (envo.GetType() == typeof(DoorSOpen) || envo.GetType() == typeof(DoorSHole))
                envo.BehaviorUpdate();
            else
            {
                IPlayer player = (IPlayer)collision.collider;
                Vector2 moveAmount = new Vector2(0, collision.intersectionRec.Height);
                player.editPosition(moveAmount);
            }
        }
    }
}
