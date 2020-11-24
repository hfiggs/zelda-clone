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
    class PuzzleMoveBlock : IPuzzle
    {
        private Boolean complete;
        public PuzzleMoveBlock()
        {
            complete = false;
        }

        public void Update(GameTime gameTime, Room room)
        {
            if (!complete) {
                var block = room.InteractEnviornment.Where(o => o.GetType() == typeof(MovableBlock)).Cast<MovableBlock>();

                if (room.EnemyList.Count > 0)
                {
                    block.ElementAt(0).Pushable = false;
                }
                else if(room.EnemyList.Count == 0 && !block.ElementAt(0).hasMoved)
                {
                    block.ElementAt(0).Pushable = true;
                }
                else if (room.EnemyList.Count == 0 && block.ElementAt(0).hasMoved)
                {
                    foreach (IEnvironment e in room.InteractEnviornment)
                    {
                        var type = e.GetType();
                        if (type == typeof(DoorEClosed))
                        {
                            DoorEClosed temp = (DoorEClosed)e;
                            temp.open = 1;
                        }
                        else if (type == typeof(DoorWClosed))
                        {
                            DoorWClosed temp = (DoorWClosed)e;
                            temp.open = 1;
                        }
                        else if (type == typeof(DoorSClosed))
                        {
                            DoorSClosed temp = (DoorSClosed)e;
                            temp.open = 1;
                        }
                        else if(type == typeof(DoorNClosed))
                        {
                            DoorNClosed temp = (DoorNClosed)e;
                            temp.open = 1;
                        }
                    }
                    complete = true;
                    const string revealAudio = "reveal";
                    AudioManager.PlayFireForget(revealAudio);
                }

            }
            
        }
    }
}
