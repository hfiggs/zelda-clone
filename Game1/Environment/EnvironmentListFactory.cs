using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Game1.Environment
{
    class EnvironmentListFactory
    {

        public static LinkedList<IEnvironment> GetEnvironmentList()
        {

            Vector2 position = new Vector2(100, 100);

            LinkedList<IEnvironment> list = new LinkedList<IEnvironment>();

            list.AddLast(new RoomBorder(position));

            list.AddLast(new DoorEBlank(position));
            list.AddLast(new DoorEClosed(position));
            list.AddLast(new DoorEHole(position));
            list.AddLast(new DoorEOpen(position));
            list.AddLast(new DoorELocked(position));

            list.AddLast(new DoorNBlank(position));
            list.AddLast(new DoorNClosed(position));
            list.AddLast(new DoorNHole(position));
            list.AddLast(new DoorNOpen(position));
            list.AddLast(new DoorNLocked(position));

            list.AddLast(new DoorSBlank(position));
            list.AddLast(new DoorSClosed(position));
            list.AddLast(new DoorSHole(position));
            list.AddLast(new DoorSOpen(position));
            list.AddLast(new DoorSLocked(position));

            list.AddLast(new DoorWBlank(position));
            list.AddLast(new DoorWClosed(position));
            list.AddLast(new DoorWHole(position));
            list.AddLast(new DoorWOpen(position));
            list.AddLast(new DoorWLocked(position));

            list.AddLast(new Black(position));
            list.AddLast(new Block(position));
            list.AddLast(new Bricks(position));
            list.AddLast(new Floor(position));
            list.AddLast(new Ladder(position));
            list.AddLast(new Sand(position));
            list.AddLast(new Stairs(position));
            list.AddLast(new StatueDragon(position));
            list.AddLast(new StatueFish(position));
            list.AddLast(new Water(position));

            return list;
        }
    }
}
