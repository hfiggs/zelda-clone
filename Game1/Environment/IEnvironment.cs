using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Environment
{
    interface IEnvironment
    {
        void Draw(SpriteBatch spritebatch, Vector2 position);

        void Update();
    }
}
