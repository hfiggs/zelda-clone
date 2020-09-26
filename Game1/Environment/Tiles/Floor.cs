﻿using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Environment
{
    class Floor : IEnvironment
    {
        private ISprite sprite;
        public Floor(SpriteSheet spriteSheet)
        {
            sprite = EnvironmentSpriteFactory.instance.createFloor();
        }

        public void BehaviorUpdate()
        {
            throw new NotImplementedException("For later collision mechanics");
        }
    }
}
