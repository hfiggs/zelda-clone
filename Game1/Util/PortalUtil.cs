using Game1.Environment;
using Game1.Player;
using Game1.RoomLoading;
using Microsoft.Xna.Framework;
using System.Linq;

namespace Game1.Util
{
    public static class PortalUtil
    {
        private const int portalXOffset = 2, portalYOffset = 5;
        private static readonly Vector2 portalOffset = new Vector2(portalXOffset, portalYOffset);


        public static void HandlePortal(PortalBlock portal, IPlayer player, Room currentRoom)
        {
            portal.IsOccupied = true;

            var otherPortal = (PortalBlock)currentRoom.InteractEnviornment.Find(e => e is PortalBlock other && (portal.State == PortalBlockState.Blue ? (other.State == PortalBlockState.Orange) : (other.State == PortalBlockState.Blue)));

            if (otherPortal != null && portal.IsReady && otherPortal.IsReady && !otherPortal.IsOccupied)
            {
                portal.IsReady = false;

                otherPortal.IsOccupied = true;
                otherPortal.IsReady = false;

                var editVector = Vector2.Subtract(otherPortal.GetHitboxes().First().Location.ToVector2(), player.GetPlayerHitbox().Location.ToVector2());
                editVector = Vector2.Add(editVector, portalOffset);

                player.EditPosition(editVector);
            }
        }
    }
}
