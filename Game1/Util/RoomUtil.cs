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

                if (playersAgree || game.Mode == 2)
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

            if(envo is DoorLocked door && door.open == 0 && playerHasKey)
            {
                door.Open(false);
                OpenAdjacentLockedDoor(screen, door.direction);

                foreach (IPlayer Player in screen.Players)
                {
                    Player.PlayerInventory.SubKey();
                }
            }
        }

        private static void OpenAdjacentLockedDoor(Screen screen, CompassDirection adjacentDirection)
        {
            var enviroList = screen.RoomsDict[GetAdjacentRoomKey(screen.CurrentRoomKey, adjacentDirection)].InteractEnviornment;

            ((DoorLocked)enviroList.FirstOrDefault(e => e is DoorLocked door && door.direction == CompassDirectionUtil.GetOppositeDirection(adjacentDirection)))?.Open(true);
        }

        public static bool IsAdjacentDoorClosed(Screen screen, CompassDirection adjacentDirection)
        {
            var enviroList = screen.RoomsDict[GetAdjacentRoomKey(screen.CurrentRoomKey, adjacentDirection)].InteractEnviornment;

            var isLocked = false;

            isLocked = ((DoorClosed)(enviroList.FirstOrDefault(e => e is DoorClosed door && door.direction == CompassDirectionUtil.GetOppositeDirection(adjacentDirection))))?.open == 0;

            return isLocked;
        }

        #endregion

        #region Bombable Door Utils

        public static void OpenBombableDoor(Screen screen, IEnvironment envo)
        {
            if (envo is DoorBombable door && !door.open)
            {
                door.Open(true);
                OpenAdjacentBombableDoor(screen, door.direction);
            }
        }

        private static void OpenAdjacentBombableDoor(Screen screen, CompassDirection adjacentDirection)
        {
            var enviroList = screen.RoomsDict[GetAdjacentRoomKey(screen.CurrentRoomKey, adjacentDirection)].InteractEnviornment;

            ((DoorBombable)enviroList.FirstOrDefault(e => e is DoorBombable door && door.direction == CompassDirectionUtil.GetOppositeDirection(adjacentDirection)))?.Open(false);
        }

        #endregion
    }
}
