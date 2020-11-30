/* Author: Hunter Figgs.3 */

using Game1.Environment;
using Game1.GameState;
using Game1.Player;
using Game1.RoomLoading;
using System.Collections.Generic;
using System.Linq;

namespace Game1.Util
{
    public static class RoomUtil
    {
        private static List<CompassDirection> playerRequests = new List<CompassDirection>(2);
        private static List<LoadZone> loadZones = new List<LoadZone>(2);

        public static void constructRoomUtil(Screen screen)
        {
            playerRequests = new List<CompassDirection>(screen.Players.Count);
            loadZones = new List<LoadZone>(screen.Players.Count);
            for (int i = 0; i < screen.Players.Count; i++)
            {
                playerRequests.Add(CompassDirection.None);
                loadZones.Add(null);
            }
        }
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

        public static void EnterDoor(Game1 game, IEnvironment envo, int playerID)
        {
            if (envo is LoadZone lZ)
            {
                playerRequests[playerID - 1] = lZ.GetTransitionDirection();
                loadZones[playerID - 1] = lZ;
                bool playersAgree = true;
                CompassDirection lastDirection = playerRequests[0];
                foreach(CompassDirection dir in playerRequests)
                {
                    if(lastDirection != dir || dir == CompassDirection.None)
                    {
                        playersAgree = false;
                    }
                    lastDirection = dir;
                }

                if (playersAgree)
                {
                    switch (lZ.GetTransitionDirection())
                    {
                        case CompassDirection.North:
                            if(game.State.GetType() != typeof(GameStateRoomToRoomNorth))
                                game.SetState(new GameStateRoomToRoomNorth(game, playerID));
                            break;
                        case CompassDirection.East:
                            if (game.State.GetType() != typeof(GameStateRoomToRoomEast))
                                game.SetState(new GameStateRoomToRoomEast(game, playerID));
                            break;
                        case CompassDirection.South:
                            if (game.State.GetType() != typeof(GameStateRoomToRoomSouth))
                                game.SetState(new GameStateRoomToRoomSouth(game, playerID));
                            break;
                        case CompassDirection.West:
                            if (game.State.GetType() != typeof(GameStateRoomToRoomWest))
                                game.SetState(new GameStateRoomToRoomWest(game, playerID));
                            break;
                    }
                }
                else
                {
                    lZ.SetWaiting(playerID);
                }
             }
        }

        public static void ExitDoor(int playerID)
        {
            List<LoadZone> view = loadZones;
            if(loadZones[playerID - 1] != null)
            {
                loadZones[playerID - 1].SetNotWaiting(playerID);
                loadZones[playerID - 1] = null;
                playerRequests[playerID - 1] = CompassDirection.None;
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
            bool playerHasKey = player.PlayerInventory.KeyCount > 0;
            bool doorOpened = false;

            switch (envo)
            {
                case DoorNLocked _:
                    if (((DoorNLocked)envo).open == 0 && playerHasKey)
                    {
                        ((DoorNLocked)envo).Open(false);
                        OpenAdjacentLockedDoor(screen, CompassDirection.North);
                        doorOpened = true;
                    }
                    break;
                case DoorELocked _:
                    if (((DoorELocked)envo).open == 0 && playerHasKey)
                    {
                        ((DoorELocked)envo).Open(false);
                        OpenAdjacentLockedDoor(screen, CompassDirection.East);
                        doorOpened = true;
                    }
                    break;
                case DoorSLocked _:
                    if (((DoorSLocked)envo).open == 0 && playerHasKey)
                    {
                        ((DoorSLocked)envo).Open(false);
                        OpenAdjacentLockedDoor(screen, CompassDirection.South);
                        doorOpened = true;
                    }
                    break;
                case DoorWLocked _:
                    if (((DoorWLocked)envo).open == 0 && playerHasKey)
                    {
                        ((DoorWLocked)envo).Open(false);
                        OpenAdjacentLockedDoor(screen, CompassDirection.West);
                        doorOpened = true;
                    }
                    break;
            }

            if (doorOpened) {
                foreach (IPlayer Player in screen.Players)
                {
                    Player.PlayerInventory.SubKey();
                }
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

        public static bool IsAdjacentDoorClosed(Screen screen, CompassDirection adjacentDirection)
        {
            var enviroList = screen.RoomsDict[GetAdjacentRoomKey(screen.CurrentRoomKey, adjacentDirection)].InteractEnviornment;

            var isLocked = false;

            switch (adjacentDirection)
            {
                case CompassDirection.North:
                    isLocked = ((DoorSClosed)enviroList.FirstOrDefault(e => e.GetType() == typeof(DoorSClosed)))?.open == 0;
                    break;
                case CompassDirection.East:
                    isLocked = ((DoorWClosed)enviroList.FirstOrDefault(e => e.GetType() == typeof(DoorWClosed)))?.open == 0;
                    break;
                case CompassDirection.South:
                    isLocked = ((DoorNClosed)enviroList.FirstOrDefault(e => e.GetType() == typeof(DoorNClosed)))?.open == 0;
                    break;
                case CompassDirection.West:
                    isLocked = ((DoorEClosed)enviroList.FirstOrDefault(e => e.GetType() == typeof(DoorEClosed)))?.open == 0;
                    break;
            }

            return isLocked;
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
