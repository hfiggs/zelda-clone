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
        private const float distanceFromTarget = 30f;
        private const float distanceFromPlayer = 24f;
        private Stack<int> Directions;
        public AIPlayerController(IPlayer player, IPlayer AIPlayer, Screen screen)
        {
            controlledPlayer = AIPlayer;
            nonControlledPlayer = player;
            this.screen = screen;
            Directions = new Stack<int>();
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
            bool containsTrap = false;
            foreach(IEnemy E in screen.CurrentRoom.EnemyList)
            {
                if(E.GetType() == typeof(SpikeTrap) || E.GetType() == typeof(OldMan))
                {
                    containsTrap = true;
                }
            }
            if (screen.CurrentRoom.EnemyList.Count + screen.CurrentRoom.DecoratedEnemyList.Count == 0 || containsTrap)
            {

                Rectangle nonControlledPlayerHB = nonControlledPlayer.GetPlayerHitbox();
                xDiff = nonControlledPlayerHB.X - controlledPlayerHB.X;
                yDiff = nonControlledPlayerHB.Y - controlledPlayerHB.Y;

                if (Math.Abs(xDiff) + Math.Abs(yDiff) >= distanceFromPlayer)
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
                    if(!enemy.Equals(target))
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
                    controlledPlayer.Attack();
                }
            }
        }

        private void DecideDirection(Rectangle controlledPlayerHB, Rectangle nonControlledPlayerHB, List<Rectangle> obstacles)
        {
            int direction = 0;
            if (Directions.Count == 0)
                Directions = AStarPathfinding.Program.findNextDecision(controlledPlayerHB, nonControlledPlayerHB, obstacles);
            if (Directions.Count != 0)
                direction = Directions.Pop();
            else
                controlledPlayer.Attack();
            if (direction == -5)
            {

            }
            else if (direction == -2)
            {
                controlledPlayer.MoveLeft();
                if (this.direction != 'E')
                    controlledPlayer.MoveLeft();
                this.direction = 'E';
            }
            else if (direction == 2)
            {

                controlledPlayer.MoveRight();
                if (this.direction != 'W')
                    controlledPlayer.MoveRight();
                this.direction = 'W';
            }
            else if (direction == 1)
            {
                controlledPlayer.MoveUp();
                if (this.direction != 'N')
                    controlledPlayer.MoveUp();
                this.direction = 'N';
            }
            else if (direction == 5)
            {
                controlledPlayer.MoveDown();
                if (this.direction != 'S')
                    controlledPlayer.MoveDown();
                this.direction = 'S';
            }
            
        }
    }
}

