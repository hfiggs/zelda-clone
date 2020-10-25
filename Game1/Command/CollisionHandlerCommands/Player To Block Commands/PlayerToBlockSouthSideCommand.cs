

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
            if (envo.GetType() == typeof(DoorSOpen) || envo.GetType() == typeof(DoorSHole))
                throw new System.NotImplementedException();
            else if (envo.GetType() == typeof(MovableBlock))
            {
                ((MovableBlock)envo).Move(new Vector2(0, -1), 1.0f, 'N');
            }
            else if (envo.GetType() == typeof(DoorSLocked))
            {
                if (player.)
                    ((DoorNLocked)envo).open();
            }
            else
            {
                Vector2 moveAmount = new Vector2(0, collision.intersectionRec.Height);
                player.editPosition(moveAmount);
            }
        }
    }
}
