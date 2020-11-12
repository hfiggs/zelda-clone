/* Author: Hunter Figgs.3 */

using Game1.Environment;
using Game1.GameState;
using Game1.Player;
using Game1.RoomLoading;
using System.Linq;

namespace Game1.Util
{
    public static class RoomUtil
    {
        public static (char, int) GetAdjacentRoomKey((char, int) currentRoomKey, CompassDirection adjacentDirection)
        {
            var adjacentRoomKey = (currentRoomKey.Item1, currentRoomKey.Item2);

            switch (adjacentDirection)
            {
                case CompassDirection.North:
                    adjacentRoomKey.Item1--;
                    break;
                case CompassDirection.East:
                    adjacentRoomKey.Item2++;
                    break;
                case CompassDirection.South:
                    adjacentRoomKey.Item1++;
                    break;
                case CompassDirection.West:
                    adjacentRoomKey.Item2--;
                    break;
            }

            return adjacentRoomKey;
        }

        public static void EnterDoor(Game1 game, IEnvironment envo)
        {
            if (envo is LoadZone lZ)
            {
                switch (lZ.GetTransitionDirection())
                {
                    case CompassDirection.North:
                        game.SetState(new GameStateRoomToRoomNorth(game));
                        break;
                    case CompassDirection.East:
                        game.SetState(new GameStateRoomToRoomEast(game));
                        break;
                    case CompassDirection.South:
                        game.SetState(new GameStateRoomToRoomSouth(game));
                        break;
                    case CompassDirection.West:
                        game.SetState(new GameStateRoomToRoomWest(game));
                        break;
                }
            }
        }

        public static void EnterExitDungeon(Game1 game, IEnvironment envo)
        {
            if (envo is EnterDungeonLoadZone)
            {
                game.SetState(new GameStateSpawnToDungeon(game));
            }
            else if (envo is ExitDungeonLoadZone)
            {
                game.SetState(new GameStateDungeonToSpawn(game));
            }
        }

        public static void EnterExitBasement(Game1 game, IEnvironment envo)
        {
            if (envo is EnterBasementLoadZone)
            {
                game.SetState(new GameStateRoomToBasement(game));
            }
            else if (envo is ExitBasementLoadZone)
            {
                game.SetState(new GameStateBasementToRoom(game));
            }
        }

        #region Locked Door Utils

        public static void OpenLockedDoor(Screen screen, IEnvironment envo, IPlayer player)
        {
            switch (envo)
            {
                case DoorNLocked _:
                    if (((DoorNLocked)envo).open == 0 && player.PlayerInventory.SubKey())
                    {
                        ((DoorNLocked)envo).Open(false);
                        OpenAdjacentLockedDoor(screen, CompassDirection.North);
                    }
                    break;
                case DoorELocked _:
                    if (((DoorELocked)envo).open == 0 && player.PlayerInventory.SubKey())
                    {
                        ((DoorELocked)envo).Open(false);
                        OpenAdjacentLockedDoor(screen, CompassDirection.East);
                    }
                    break;
                case DoorSLocked _:
                    if (((DoorSLocked)envo).open == 0 && player.PlayerInventory.SubKey())
                    {
                        ((DoorSLocked)envo).Open(false);
                        OpenAdjacentLockedDoor(screen, CompassDirection.South);
                    }
                    break;
                case DoorWLocked _:
                    if (((DoorWLocked)envo).open == 0 && player.PlayerInventory.SubKey())
                    {
                        ((DoorWLocked)envo).Open(false);
                        OpenAdjacentLockedDoor(screen, CompassDirection.West);
                    }
                    break;
            }
        }

        private static void OpenAdjacentLockedDoor(Screen screen, CompassDirection adjacentDirection)
        {
            var enviroList = screen.RoomsDict[GetAdjacentRoomKey(screen.CurrentRoomKey, adjacentDirection)].InteractEnviornment;

            switch (adjacentDirection)
            {
                case CompassDirection.North:
                    ((DoorSLocked)enviroList.FirstOrDefault(e => e.GetType() == typeof(DoorSLocked)))?.Open(true);
                    break;
                case CompassDirection.East:
                    ((DoorWLocked)enviroList.FirstOrDefault(e => e.GetType() == typeof(DoorWLocked)))?.Open(true);
                    break;
                case CompassDirection.South:
                    ((DoorNLocked)enviroList.FirstOrDefault(e => e.GetType() == typeof(DoorNLocked)))?.Open(true);
                    break;
                case CompassDirection.West:
                    ((DoorELocked)enviroList.FirstOrDefault(e => e.GetType() == typeof(DoorELocked)))?.Open(true);
                    break;
            }
        }

        #endregion

        #region Bombable Door Utils

        public static void OpenBombableDoor(Screen screen, IEnvironment envo)
        {
            switch (envo)
            {
                case DoorNBombable dN:
                    if (!dN.open)
                    {
                        dN.OpenDoor(true);
                        OpenAdjacentBombableDoor(screen, CompassDirection.North);
                    }
                    break;
                case DoorEBombable dE:
                    if (!dE.open)
                    {
                        dE.OpenDoor(true);
                        OpenAdjacentBombableDoor(screen, CompassDirection.East);
                    }
                    break;
                case DoorSBombable dS:
                    if (!dS.open)
                    {
                        dS.OpenDoor(true);
                        OpenAdjacentBombableDoor(screen, CompassDirection.South);
                    }
                    break;
                case DoorWBombable dW:
                    if (!dW.open)
                    {
                        dW.OpenDoor(true);
                        OpenAdjacentBombableDoor(screen, CompassDirection.West);
                    }
                    break;
            }
        }

        private static void OpenAdjacentBombableDoor(Screen screen, CompassDirection adjacentDirection)
        {
            var enviroList = screen.RoomsDict[GetAdjacentRoomKey(screen.CurrentRoomKey, adjacentDirection)].InteractEnviornment;

            switch (adjacentDirection)
            {
                case CompassDirection.North:
                    ((DoorSBombable)enviroList.FirstOrDefault(e => e.GetType() == typeof(DoorSBombable)))?.OpenDoor(false);
                    break;
                case CompassDirection.East:
                    ((DoorWBombable)enviroList.FirstOrDefault(e => e.GetType() == typeof(DoorWBombable)))?.OpenDoor(false);
                    break;
                case CompassDirection.South:
                    ((DoorNBombable)enviroList.FirstOrDefault(e => e.GetType() == typeof(DoorNBombable)))?.OpenDoor(false);
                    break;
                case CompassDirection.West:
                    ((DoorEBombable)enviroList.FirstOrDefault(e => e.GetType() == typeof(DoorEBombable)))?.OpenDoor(false);
                    break;
            }
        }

        #endregion
    }
}
