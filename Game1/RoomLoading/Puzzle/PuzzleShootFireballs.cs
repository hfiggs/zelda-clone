using Game1.Item;
using Game1.Projectile;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.RoomLoading.Puzzle
{
    class PuzzleShootFireballs : IPuzzle
    {
        private Screen screen;
        private Boolean complete;

        private float timeUntilNextFrame;
        private double totalElapsedSeconds = 0;
        private const double fireballShoot = 1.2;

        public PuzzleShootFireballs(Screen screen)
        {
            complete = false;
            this.screen = screen;
        }

        public void Update(GameTime gameTime, Room room)
        {
            totalElapsedSeconds += gameTime.ElapsedGameTime.TotalSeconds;

            if (!complete)
            {
                if (room.EnemyList.Count > 0)
                {
                    if (totalElapsedSeconds >= fireballShoot)
                    {
                        totalElapsedSeconds -= fireballShoot;
                        room.SpawnProjectile(new Fireballs(new Vector2(32, 32), screen.Players[0].GetPlayerHitbox(), 'M'));
                        room.SpawnProjectile(new Fireballs(new Vector2(32, 112), screen.Players[0].GetPlayerHitbox(), 'M'));
                    }
                    timeUntilNextFrame -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                }
                else
                {
                    complete = true;
                }
            }
        }
       

    }

}
