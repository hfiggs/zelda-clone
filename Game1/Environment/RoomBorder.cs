using Game1.Sprite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Environment
{
    class RoomBorder : IEnvironment
    {
        private ISprite sprite;

        public RoomBorder()
        {
            sprite = EnvironmentSpriteFactory.instance.createRoom();
        }
        public void BehaviorUpdate()
        {
            throw new NotImplementedException("For collision mechanics later");
        }
    }
}
