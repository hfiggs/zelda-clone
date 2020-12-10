using Game1.Sprite;
using Game1.Util;
using Microsoft.Xna.Framework;

namespace Game1.Environment.EnvironmentUtil
{
    public static class DoorUtil
    {
        private static readonly int openHitboxShort = 8, openHitboxLong = 32, openHitboxDiff = 24;
        private static readonly int doorFloorShort = 8, doorFloorLong = 17;

        public static void SetLockedDoorSprites(out ISprite spriteBelow, out ISprite spriteAbove, CompassDirection direction)
        {
            switch (direction)
            {
                case CompassDirection.North:
                    spriteBelow = EnvironmentSpriteFactory.instance.createDoorNLockedBelow();
                    spriteAbove = EnvironmentSpriteFactory.instance.createDoorNLockedAbove();
                    break;
                case CompassDirection.East:
                    spriteBelow = EnvironmentSpriteFactory.instance.createDoorELockedBelow();
                    spriteAbove = EnvironmentSpriteFactory.instance.createDoorELockedAbove();
                    break;
                case CompassDirection.South:
                    spriteBelow = EnvironmentSpriteFactory.instance.createDoorSLockedBelow();
                    spriteAbove = EnvironmentSpriteFactory.instance.createDoorSLockedAbove();
                    break;
                case CompassDirection.West:
                default:
                    spriteBelow = EnvironmentSpriteFactory.instance.createDoorWLockedBelow();
                    spriteAbove = EnvironmentSpriteFactory.instance.createDoorWLockedAbove();
                    break;
            }
        }

        public static void SetClosedDoorSprites(out ISprite spriteBelow, out ISprite spriteAbove, CompassDirection direction)
        {
            switch (direction)
            {
                case CompassDirection.North:
                    spriteBelow = EnvironmentSpriteFactory.instance.createDoorNClosedBelow();
                    spriteAbove = EnvironmentSpriteFactory.instance.createDoorNClosedAbove();
                    break;
                case CompassDirection.East:
                    spriteBelow = EnvironmentSpriteFactory.instance.createDoorEClosedBelow();
                    spriteAbove = EnvironmentSpriteFactory.instance.createDoorEClosedAbove();
                    break;
                case CompassDirection.South:
                    spriteBelow = EnvironmentSpriteFactory.instance.createDoorSClosedBelow();
                    spriteAbove = EnvironmentSpriteFactory.instance.createDoorSClosedAbove();
                    break;
                case CompassDirection.West:
                default:
                    spriteBelow = EnvironmentSpriteFactory.instance.createDoorWClosedBelow();
                    spriteAbove = EnvironmentSpriteFactory.instance.createDoorWClosedAbove();
                    break;
            }
        }

        public static void SetOpenDoorSprites(out ISprite spriteBelow, out ISprite spriteAbove, CompassDirection direction)
        {
            switch (direction)
            {
                case CompassDirection.North:
                    spriteBelow = EnvironmentSpriteFactory.instance.createDoorNOpenBelow();
                    spriteAbove = EnvironmentSpriteFactory.instance.createDoorNOpenAbove();
                    break;
                case CompassDirection.East:
                    spriteBelow = EnvironmentSpriteFactory.instance.createDoorEOpenBelow();
                    spriteAbove = EnvironmentSpriteFactory.instance.createDoorEOpenAbove();
                    break;
                case CompassDirection.South:
                    spriteBelow = EnvironmentSpriteFactory.instance.createDoorSOpenBelow();
                    spriteAbove = EnvironmentSpriteFactory.instance.createDoorSOpenAbove();
                    break;
                case CompassDirection.West:
                default:
                    spriteBelow = EnvironmentSpriteFactory.instance.createDoorWOpenBelow();
                    spriteAbove = EnvironmentSpriteFactory.instance.createDoorWOpenAbove();
                    break;
            }
        }

        public static void SetHoleDoorSprites(out ISprite spriteBelow, out ISprite spriteAbove, CompassDirection direction)
        {
            switch (direction)
            {
                case CompassDirection.North:
                    spriteBelow = EnvironmentSpriteFactory.instance.createDoorNHoleBelow();
                    spriteAbove = EnvironmentSpriteFactory.instance.createDoorNHoleAbove();
                    break;
                case CompassDirection.East:
                    spriteBelow = EnvironmentSpriteFactory.instance.createDoorEHoleBelow();
                    spriteAbove = EnvironmentSpriteFactory.instance.createDoorEHoleAbove();
                    break;
                case CompassDirection.South:
                    spriteBelow = EnvironmentSpriteFactory.instance.createDoorSHoleBelow();
                    spriteAbove = EnvironmentSpriteFactory.instance.createDoorSHoleAbove();
                    break;
                case CompassDirection.West:
                default:
                    spriteBelow = EnvironmentSpriteFactory.instance.createDoorWHoleBelow();
                    spriteAbove = EnvironmentSpriteFactory.instance.createDoorWHoleAbove();
                    break;
            }
        }

        public static ISprite GetBlankDoorSprite(CompassDirection direction)
        {
            switch (direction)
            {
                case CompassDirection.North:
                    return EnvironmentSpriteFactory.instance.createDoorNBlankBelow();
                case CompassDirection.East:
                    return EnvironmentSpriteFactory.instance.createDoorEBlankBelow();
                case CompassDirection.South:
                    return EnvironmentSpriteFactory.instance.createDoorSBlankBelow();
                case CompassDirection.West:
                default:
                    return EnvironmentSpriteFactory.instance.createDoorWBlankBelow();
            }
        }

        public static ISprite GetDoorFloorSprite(CompassDirection direction)
        {
            switch (direction)
            {
                case CompassDirection.North:
                    return EnvironmentSpriteFactory.instance.CreateDoorNFloor();
                case CompassDirection.East:
                    return EnvironmentSpriteFactory.instance.CreateDoorEFloor();
                case CompassDirection.South:
                    return EnvironmentSpriteFactory.instance.CreateDoorSFloor();
                case CompassDirection.West:
                default:
                    return EnvironmentSpriteFactory.instance.CreateDoorWFloor();
            }
        }

        public static void SetOpenDoorHitboxes(out Rectangle hitbox1, out Rectangle hitbox2, CompassDirection direction, Vector2 position)
        {
            switch (direction)
            {
                case CompassDirection.North:
                case CompassDirection.South:
                    hitbox1 = new Rectangle(0, 0, openHitboxShort, openHitboxLong);
                    hitbox2 = new Rectangle(openHitboxDiff, 0, openHitboxShort, openHitboxLong);
                    break;
                case CompassDirection.East:
                case CompassDirection.West:
                default:
                    hitbox1 = new Rectangle(0, 0, openHitboxLong, openHitboxShort);
                    hitbox2 = new Rectangle(0, openHitboxDiff, openHitboxLong, openHitboxShort);
                    break;
            }

            hitbox1.Location += position.ToPoint();
            hitbox2.Location += position.ToPoint();
        }

        public static Vector2 GetDoorFloorOffset(CompassDirection direction)
        {
            switch (direction)
            {
                case CompassDirection.North:
                    return new Vector2(doorFloorShort, doorFloorLong);
                case CompassDirection.East:
                    return new Vector2(0, doorFloorShort);
                case CompassDirection.South:
                    return new Vector2(doorFloorShort, 0);
                case CompassDirection.West:
                default:
                    return new Vector2(doorFloorLong, doorFloorShort);
            }
        }
    }
}
