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
    class PuzzleMoveBlockNS : IPuzzle
    {
        private bool complete;        
        public PuzzleMoveBlockNS()
        {
            complete = false;
        }

        public void Update(GameTime gameTime, Room room)
        {
            if (!complete) {
                var block = room.InteractEnviornment.Where(o => o.GetType() == typeof(MovableBlock)).Cast<MovableBlock>().ElementAt(0);

                if(block.hasMoved && (block.hasMovedDir == Util.CompassDirection.North || block.hasMovedDir == Util.CompassDirection.South))
                {
                    complete = true;
                    AudioManager.PlayFireForget("reveal");
                }
            }
            
        }
    }
}
