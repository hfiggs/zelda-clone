using Game1.Projectile;
using Game1.Sprite;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Player
{
    public class PortalGunManager
    {
        private static PortalGunManager instance = new PortalGunManager();

        public PortalColor Player1Color { get; private set; }
        public PortalColor Player2Color { get; private set; }

        public static PortalGunManager Instance
        {
            get
            {
                return instance;
            }
        }

        private PortalGunManager()
        {
            Player1Color = PortalColor.Blue;
            Player2Color = PortalColor.Blue;
        }

        public void Shoot(IPlayer player)
        {
            switch(player)
            {
                case Player1 _:
                    Player1Color = Player1Color == PortalColor.Blue ? PortalColor.Orange : PortalColor.Blue;
                    break;
                case Player2 _:
                    Player2Color = Player2Color == PortalColor.Blue ? PortalColor.Orange : PortalColor.Blue;
                    break;
                default:
                    break;
            }
        }
    }
}

