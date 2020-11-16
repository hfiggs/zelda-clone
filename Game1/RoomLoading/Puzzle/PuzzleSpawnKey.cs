using Game1.Item;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.RoomLoading.Puzzle
{
    class PuzzleSpawnKey : IPuzzle
    {
        private Boolean complete;
        public PuzzleSpawnKey()
        {
            complete = false;
        }

        public void Check(GameTime gameTime, Room room)
        {
            if (!complete) {
                if (room.EnemyList.Count == 0)
                {
                    const int keySpawnX = 128, keySpawnY = 88;
                    room.SpawnItem(new Key(new Vector2(keySpawnX, keySpawnY)));
                    complete = true;
                }
            }
            
        }
    }
}
