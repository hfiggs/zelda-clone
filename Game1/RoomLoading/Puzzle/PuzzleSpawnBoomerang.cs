using Game1.Item;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.RoomLoading.Puzzle
{
    class PuzzleSpawnBoomerang : IPuzzle
    {
        private Boolean complete;
        public PuzzleSpawnBoomerang()
        {
            complete = false;
        }

        public void Check(GameTime gameTime, Room room)
        {
            if (!complete) {
                if (room.EnemyList.Count == 0)
                {
                    room.SpawnItem(new ItemBoomerang(new Vector2(128, 88)));
                    complete = true;
                }
            }
            
        }
    }
}
