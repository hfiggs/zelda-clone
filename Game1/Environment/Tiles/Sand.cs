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
    class Sand : IEnvironment
    {
        private ISprite sprite;
        public Sand(SpriteSheet spriteSheet)
        {
            sprite = EnvironmentSpriteFactory.instance.createSand();
        }

        public void BehaviorUpdate()
        {
            throw new NotImplementedException("For later collision mechanics");
        }
    }
}
