using Game1.Enemy;
using Game1.Environment;
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
        private const float distanceFromTarget = 24f;

        public AIPlayerController(IPlayer player, IPlayer AIPlayer, Screen screen)
        {
            controlledPlayer = AIPlayer;
            nonControlledPlayer = player;
            this.screen = screen;
        }

        public void Update(GameTime time)
        {
            Rectangle controlledPlayerVector = controlledPlayer.GetPlayerHitbox();
            Rectangle nonControlledPlayerVector = nonControlledPlayer.GetPlayerHitbox();
            float xDiff;
            float yDiff;

            if (screen.CurrentRoom.EnemyList.Count + screen.CurrentRoom.DecoratedEnemyList.Count == 0)
            {
                 xDiff = nonControlledPlayerVector.X - controlledPlayerVector.X;
                 yDiff = nonControlledPlayerVector.Y - controlledPlayerVector.Y;

                if(Math.Abs(xDiff) + Math.Abs(yDiff) >= distanceFromTarget)
                {
                    List<Rectangle> obstacles = new List<Rectangle>();
                    foreach(IEnvironment env in screen.CurrentRoom.InteractEnviornment)
                    {
                        obstacles.AddRange(env.GetHitboxes());
                    }
                    int direction = AStarPathfinding.Program.findNextDecision(controlledPlayerVector, nonControlledPlayerVector, obstacles);
                    if(direction == -5)
                    {
                    }else if(direction == -1)
                    {
                        controlledPlayer.MoveLeft();
                    }else if(direction == 1)
                    {
                        controlledPlayer.MoveRight();
                    }else if(direction == 2)
                    {
                        controlledPlayer.MoveUp();
                    }else if(direction == 4)
                    {
                        controlledPlayer.MoveDown();
                    }
                }
            }
            else if(target == null)
            {
                target = screen.CurrentRoom.EnemyList[0];
            }else
            {
                xDiff = nonControlledPlayerVector.X - target.GetPosition().X;
                yDiff = nonControlledPlayerVector.Y - target.GetPosition().Y;
            }
        }
    }
}

