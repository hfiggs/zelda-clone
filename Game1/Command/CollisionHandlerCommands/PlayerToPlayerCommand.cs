using Game1.Collision_Handling;
using Game1.Player;
using Game1.Util;
using Microsoft.Xna.Framework;

namespace Game1.Command.CollisionHandlerCommands
{
    class PlayerToPlayerCommand : ICollisionCommand
    {
        public PlayerToPlayerCommand() { }

        public void Execute(Collision collision)
        {
            IPlayer player1 = (IPlayer)collision.collider;
            char side = collision.side;

            // side is side of player2 (collidee)
            Vector2 moveAmount = Vector2.Multiply(new Vector2(collision.intersectionRec.Width, collision.intersectionRec.Height), CompassDirectionUtil.GetDirectionVector(side));
            player1.EditPosition(moveAmount);

            if (player1 is DamagedPlayer dP && dP.stillSlide)
            {
                dP.StopKnockback(new Vector2(collision.intersectionRec.Width, collision.intersectionRec.Height));
            }

        }
    }
}
