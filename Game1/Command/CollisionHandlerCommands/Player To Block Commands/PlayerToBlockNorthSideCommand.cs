

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
            if (envo.GetType() == typeof(DoorNOpen) || envo.GetType() == typeof(DoorNHole))
                throw new System.NotImplementedException();
            else if (envo.GetType() == typeof(MovableBlock))
            {
                ((MovableBlock)envo).Move(new Vector2(0,1), 1.0f, 'S');
            }
            else if(envo.GetType() == typeof(DoorSLocked))
            {
                if(player.)
                ((DoorSLocked)envo).open();
            }
            else
            {
                Vector2 moveAmount = new Vector2(0, -collision.intersectionRec.Height);
                player.editPosition(moveAmount);
            }
        }
    }
}
