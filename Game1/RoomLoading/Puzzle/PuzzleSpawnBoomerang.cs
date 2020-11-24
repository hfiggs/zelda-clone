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

        public void Update(GameTime gameTime, Room room)
        {
            if (!complete) {
                if (room.EnemyList.Count == 0)
                {
                    const int boomerangSpawnX = 128, boomerangSpawnY = 88;
                    room.SpawnItem(new ItemBoomerang(new Vector2(boomerangSpawnX, boomerangSpawnY)));
                    complete = true;
                }
            }
            
        }
    }
}
