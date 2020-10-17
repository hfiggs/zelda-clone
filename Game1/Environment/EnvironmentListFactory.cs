using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Game1.Environment
{
    class EnvironmentListFactory
    {

        public static LinkedList<IEnvironment> GetEnvironmentList()
        {

            Vector2 position = new Vector2(0, 0);

            LinkedList<IEnvironment> list = new LinkedList<IEnvironment>();

            list.AddLast(new Block(position));
            list.AddLast(new Black(position));
            list.AddLast(new Bricks(position));
            list.AddLast(new Floor(position));
            list.AddLast(new Ladder(position));
            list.AddLast(new Sand(position));
            list.AddLast(new Stairs(position));
            list.AddLast(new StatueDragon(position));
            list.AddLast(new StatueFish(position));
            list.AddLast(new Water(position));

            list.AddLast(new Fire(position));

            list.AddLast(new DoorNFloor(position));
            list.AddLast(new DoorEFloor(position));
            list.AddLast(new DoorSFloor(position));
            list.AddLast(new DoorWFloor(position));

            return list;
        }
    }
}
