using Game1.Collision_Handling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Command.CollisionHandlerCommands
{
    interface ICollisionCommand
    {
        void Execute(Collision collision);

    }
}
