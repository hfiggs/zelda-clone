using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Enemy
{
    interface IEnemy
    {
        void Attack();

        void ReceiveDamage();

        void Update(GameTime gameTime, Rectangle drawingLimits5);

        void Draw();
    }
}
