using Game1.Sprite;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Enemy
{
    interface IEnemyState
    {
        void Attack();

        void Update(GameTime gametime, Rectangle drawingLimits);

        void editPosition(Vector2 amount);

        Vector2 GetPosition();

        Vector2 GetDirection();

        ISprite Sprite { get; }
    }
}
