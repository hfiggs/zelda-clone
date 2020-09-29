using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Environment
{
    class Black : IEnvironment
    {
        private ISprite sprite;
        public Black(SpriteSheet spriteSheet)
        {
            sprite = EnvironmentSpriteFactory.instance.createBlack();
        }

        public void BehaviorUpdate()
        {
            throw new NotImplementedException("For later collision mechanics");
        }
    }
}
