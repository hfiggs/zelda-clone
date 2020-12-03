using Game1.Environment;
using Game1.Player;
using Game1.Projectile;
using Game1.RoomLoading;
using Microsoft.Xna.Framework;
using System.Linq;

namespace Game1.Util
{
    public static class PortalUtil
    {
        private const int portalXOffset = 2, portalYOffset = 5;
        private static readonly Vector2 portalOffset = new Vector2(portalXOffset, portalYOffset);


        public static void HandlePlayerPortal(PortalBlock portalBlock, IPlayer player, Room currentRoom)
        {
            portalBlock.IsOccupied = true;

            var otherPortal = (PortalBlock)currentRoom.InteractEnviornment.Find(e => e is PortalBlock other && (portalBlock.State == PortalBlockState.Blue ? (other.State == PortalBlockState.Orange) : (other.State == PortalBlockState.Blue)));

            if (otherPortal != null && portalBlock.IsReady && otherPortal.IsReady && !otherPortal.IsOccupied)
            {
                portalBlock.IsReady = false;

                otherPortal.IsOccupied = true;
                otherPortal.IsReady = false;

                var editVector = Vector2.Subtract(otherPortal.GetHitboxes().First().Location.ToVector2(), player.GetPlayerHitbox().Location.ToVector2());
                editVector = Vector2.Add(editVector, portalOffset);

                player.EditPosition(editVector);
            }
        }

        public static void HandleProjectilePortal(PortalBlock portalBlock, IProjectile proj, Screen screen)
        {

            switch(proj)
            {
                case PortalProjectile portalProj:
                    switch (portalProj.PortalColor)
                    {
                        case PortalColor.Blue:
                            screen.PortalManager.BluePortal = portalBlock;
                            break;
                        case PortalColor.Orange:
                            screen.PortalManager.OrangePortal = portalBlock;
                            break;
                    }
                    portalProj.BeginDespawn();
                    break;

                case Arrow _:
                case Boomerang _:
                    if (portalBlock.State != PortalBlockState.Normal)
                    {
                        HandleProjectileTeleport(portalBlock, proj, screen.CurrentRoom);
                    }
                    break;
            }
        }

        private static void HandleProjectileTeleport(PortalBlock portalBlock, IProjectile proj, Room room)
        {
            portalBlock.IsOccupied = true;

            var otherPortal = (PortalBlock)room.InteractEnviornment.Find(e => e is PortalBlock other && (portalBlock.State == PortalBlockState.Blue ? (other.State == PortalBlockState.Orange) : (other.State == PortalBlockState.Blue)));

            if (otherPortal != null && portalBlock.IsReady && otherPortal.IsReady && !otherPortal.IsOccupied)
            {
                portalBlock.IsReady = false;

                otherPortal.IsOccupied = true;
                otherPortal.IsReady = false;

                var editVector = Vector2.Subtract(otherPortal.GetHitboxes().First().Location.ToVector2(), proj.GetHitbox().Location.ToVector2());
                editVector = Vector2.Add(editVector, portalOffset);

                proj.EditPosition(editVector);
            }
        }
    }
}
