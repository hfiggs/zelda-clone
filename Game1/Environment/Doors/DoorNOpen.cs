﻿using System;
using Game1.Sprite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Environment
{
    class DoorNOpen : IEnvironment
    {
        private ISprite sprite;
        public DoorNOpen(SpriteSheet spriteSheet)
        {
            sprite = EnvironmentSpriteFactory.instance.createDoorNOpen();
        }

        public void BehaviorUpdate()
        {
            throw new NotImplementedException("For later collision mechanics");
        }
    }
}
