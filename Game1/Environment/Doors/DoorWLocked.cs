using System;
using Game1.Sprite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Environment
{
    class DoorWLocked : IEnvironment
    {
        private ISprite sprite;
        public DoorWLocked(SpriteSheet spriteSheet)
        {
            sprite = EnvironmentSpriteFactory.instance.createDoorWLocked();
        }

        public void BehaviorUpdate()
        {
            throw new NotImplementedException("For later collision mechanics");
        }
    }
}
