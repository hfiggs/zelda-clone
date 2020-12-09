using Game1.Audio;
using Game1.Environment;
using Microsoft.Xna.Framework;
using System;
using System.Linq;

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
                        if (e is DoorClosed door)
                        {
                            door.Open(false);
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
