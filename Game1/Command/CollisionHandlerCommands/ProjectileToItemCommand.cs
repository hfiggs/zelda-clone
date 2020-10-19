using Game1.Collision_Handling;
using Game1.Item;
using Game1.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Command.CollisionHandlerCommands
{
    class ProjectileToItemCommand : ICollisionCommand
    {
        public ProjectileToItemCommand()
        {

        }

        public void Execute(Collision collision)
        {
            IPlayer player = (IPlayer)collision.collider;
            IItem item = (IItem)collision.collidee;
        }
    }
}
