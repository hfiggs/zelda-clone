using Game1.Enemy;
using Game1.Environment;
using Game1.Projectile;
using Game1.RoomLoading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Player
{
    public class AIPlayerController
    {
        private IPlayer controlledPlayer;
        private IPlayer nonControlledPlayer;
        private Screen screen;
        private IEnemy target;
        private char direction;
        private const float distanceFromTarget = 24f;

        public AIPlayerController(IPlayer player, IPlayer AIPlayer, Screen screen)
        {
            controlledPlayer = AIPlayer;
            nonControlledPlayer = player;
            this.screen = screen;
        }

        public void Update(GameTime time)
        {
            Rectangle controlledPlayerHB = controlledPlayer.GetPlayerHitbox();

            float xDiff;
            float yDiff;
            List<Rectangle> obstacles = new List<Rectangle>();
            foreach (IEnvironment env in screen.CurrentRoom.InteractEnviornment)
            {
                obstacles.AddRange(env.GetHitboxes());
            }

            if (screen.CurrentRoom.EnemyList.Count + screen.CurrentRoom.DecoratedEnemyList.Count == 0)
            {
                Rectangle nonControlledPlayerHB = nonControlledPlayer.GetPlayerHitbox();
                xDiff = nonControlledPlayerHB.X - controlledPlayerHB.X;
                yDiff = nonControlledPlayerHB.Y - controlledPlayerHB.Y;

                if (Math.Abs(xDiff) + Math.Abs(yDiff) >= distanceFromTarget)
                {
                    DecideDirection(controlledPlayerHB, nonControlledPlayerHB, obstacles);
                }


            }
            else if(target == null || target.ShouldRemove())
            {
                target = screen.CurrentRoom.EnemyList[0];
            }else
            {
                Rectangle enemyHB = target.GetHitboxes()[0];
                xDiff = controlledPlayerHB.X - enemyHB.X;
                yDiff = controlledPlayerHB.Y - enemyHB.Y;
                foreach(IEnemy enemy in screen.CurrentRoom.EnemyList)
                {
                    if(!enemy.Equals(target))
                    obstacles.AddRange(enemy.GetHitboxes());
                }
                foreach(IEnemy enemy in screen.CurrentRoom.DecoratedEnemyList)
                {
                    obstacles.AddRange(enemy.GetHitboxes());
                }
                foreach(IProjectile proj in screen.CurrentRoom.ProjectileList)
                {
                    obstacles.Add(proj.GetHitbox());
                }

                if (Math.Abs(xDiff) + Math.Abs(yDiff) >= distanceFromTarget)
                {
                    DecideDirection(controlledPlayerHB, enemyHB, obstacles);
                }
                else
                {
                    if (Math.Abs(yDiff) > Math.Abs(xDiff))
                    {
                        if (yDiff < 0 && direction != 'S')
                        {
                            controlledPlayer.MoveDown();
                            controlledPlayer.Attack();
                        }
                        else if (yDiff > 0 && direction != 'N')
                        {
                            controlledPlayer.MoveUp();
                            controlledPlayer.Attack();  
                        }
                        else
                            controlledPlayer.Attack();
                    }
                    else
                    controlledPlayer.Attack();
                }
            }
        }

        private void DecideDirection(Rectangle controlledPlayerHB, Rectangle nonControlledPlayerHB, List<Rectangle> obstacles)
        {

                int direction = AStarPathfinding.Program.findNextDecision(controlledPlayerHB, nonControlledPlayerHB, obstacles);
                if (direction == -5)
                {
                }
                else if (direction == -1)
                {
                    controlledPlayer.MoveLeft();
                    this.direction = 'E';
                }
                else if (direction == 1)
                {
                    controlledPlayer.MoveRight();
                    this.direction = 'W';
                }
                else if (direction == 2)
                {
                    controlledPlayer.MoveUp();
                    this.direction = 'N';
                }
                else if (direction == 4)
                {
                    controlledPlayer.MoveDown();
                    this.direction = 'S';
                }
        }
    }
}

