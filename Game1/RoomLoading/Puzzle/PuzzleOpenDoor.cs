using Game1.Environment;
using Microsoft.Xna.Framework;
using System;

namespace Game1.RoomLoading.Puzzle
{
    class PuzzleOpenDoor : IPuzzle
    {
        private Boolean complete;
        public PuzzleOpenDoor()
        {
            complete = false;
        }

        public void Update(GameTime gameTime, Room room)
        {
            if (!complete) {
                if (room.EnemyList.Count == 0)
                {
                    foreach (IEnvironment e in room.InteractEnviornment)
                    {
                        if (e is DoorClosed door)
                        {
                            door.Open(false);
                        }
                    }

                    complete = true;
                }

            }
            
        }
    }
}
