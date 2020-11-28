using Game1.Item;
using Microsoft.Xna.Framework;

namespace Game1.RoomLoading.Puzzle
{
    class PuzzleSpawnKey : IPuzzle
    {
        private bool complete;
        private readonly Vector2 spawnPosition;

        public PuzzleSpawnKey(Vector2 spawnPosition)
        {
            complete = false;
            this.spawnPosition = spawnPosition;
        }

        public void Update(GameTime gameTime, Room room)
        {
            if (!complete) {
                if (room.EnemyList.Count == 0)
                {
                    room.SpawnItem(new Key(spawnPosition));
                    complete = true;
                }
            }
            
        }
    }
}
