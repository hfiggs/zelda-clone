using Game1.Environment;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Game1.RoomLoading
{
    public class PortalManager
    {
        private readonly Screen screen;

        private PortalBlock _bluePortal;
        private PortalBlock _orangePortal;

        public PortalManager(Screen screen)
        {
            this.screen = screen;
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void ResetRoom()
        {
            ((List<PortalBlock>)screen.CurrentRoom.InteractEnviornment.Where(e => e is PortalBlock)).ForEach(e => e.State = PortalBlockState.Normal);
        }

        public PortalBlock BluePortal
        { 
            get => _bluePortal;
            set
            {
                foreach (IEnvironment env in screen.CurrentRoom.InteractEnviornment)
                {
                    if (env is PortalBlock block && block.State == PortalBlockState.Blue)
                    {
                        block.State = PortalBlockState.Normal;
                    }


                    _bluePortal = value;
                    _bluePortal.State = PortalBlockState.Blue;
                }
            }
        }

        public PortalBlock OrangePortal
        {
            get => _orangePortal;
            set
            {
                foreach (IEnvironment env in screen.CurrentRoom.InteractEnviornment)
                {
                    if (env is PortalBlock block && block.State == PortalBlockState.Orange)
                    {
                        block.State = PortalBlockState.Normal;
                    }
                }

                _orangePortal = value;
                _orangePortal.State = PortalBlockState.Orange;
            }
        }
    }
}
