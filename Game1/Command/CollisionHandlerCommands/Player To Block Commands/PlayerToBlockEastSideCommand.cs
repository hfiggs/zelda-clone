

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
            if (envo.GetType() == typeof(DoorEOpen) || envo.GetType() == typeof(DoorEHole))
                envo.BehaviorUpdate();
            else
            {
                IPlayer player = (IPlayer)collision.collider;
                Vector2 moveAmount = new Vector2(collision.intersectionRec.X, 0);
                player.editPosition(moveAmount);
            }
        }
    }
}
