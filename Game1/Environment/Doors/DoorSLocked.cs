using System;
using Game1.Sprite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Environment
{
    class DoorSLocked : IEnvironment
    {
        private ISprite sprite;
        public DoorSLocked(SpriteSheet spriteSheet)
        {
            sprite = EnvironmentSpriteFactory.instance.createDoorSLocked();
        }

        public void BehaviorUpdate()
        {
            throw new NotImplementedException("For later collision mechanics");
        }
    }
}
