using System;
using Game1.Sprite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Environment
{
    class DoorNLocked : IEnvironment
    {
        private ISprite sprite;
        public DoorNLocked(SpriteSheet spriteSheet)
        {
            sprite = EnvironmentSpriteFactory.instance.createDoorNLocked();
        }

        public void BehaviorUpdate()
        {
            throw new NotImplementedException("For later collision mechanics");
        }
    }
}
