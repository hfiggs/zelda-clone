

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
            if (envo.GetType() == typeof(DoorEOpen) || envo.GetType() == typeof(DoorEHole))
                throw new System.NotImplementedException();
            else if (envo.GetType() == typeof(MovableBlock))
            {
                ((MovableBlock)envo).Move(new Vector2(-1, 0), 1.0f, 'W');
            }
            else if (envo.GetType() == typeof(DoorSLocked))
            {
                if (player.)
                    ((DoorWLocked)envo).open();
            }
            else
            {
                Vector2 moveAmount = new Vector2(collision.intersectionRec.Width, 0);
                player.editPosition(moveAmount);
            }
        }
    }
}
