using Game1.Audio;
using Game1.Environment;
using Game1.Item;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.RoomLoading.Puzzle
{
    class PuzzleOpenDoor : IPuzzle
    {
        private Boolean complete;
        public PuzzleOpenDoor()
        {
            complete = false;
        }

        public void Check(GameTime gameTime, Room room)
        {
            if (!complete) {
                if (room.EnemyList.Count == 0)
                {
                    foreach (IEnvironment door in room.InteractEnviornment)
                    {
                        var type = door.GetType();
                        if (type == typeof(DoorEClosed))
                        {
                            DoorEClosed temp = (DoorEClosed)door;
                            temp.open = 1;
                        }
                        else if (type == typeof(DoorWClosed))
                        {
                            DoorWClosed temp = (DoorWClosed)door;
                            temp.open = 1;
                        }
                        else if (type == typeof(DoorSClosed))
                        {
                            DoorSClosed temp = (DoorSClosed)door;
                            temp.open = 1;
                        }
                        else if(type == typeof(DoorNClosed))
                        {
                            DoorNClosed temp = (DoorNClosed)door;
                            temp.open = 1;
                        }
                    }
                    complete = true;
                    AudioManager.PlayFireForget("doorLock");
                }

            }
            
        }
    }
}
