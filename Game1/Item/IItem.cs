using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Item
{
    interface IItem
    {
        void Update();
        void Draw(SpriteBatch spriteBatch,  Vector2 position);
    }
}
