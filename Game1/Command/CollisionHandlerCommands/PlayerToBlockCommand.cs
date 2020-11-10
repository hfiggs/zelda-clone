using Game1.Collision_Handling;
using Game1.Environment;
using Game1.GameState;
using Game1.Player;
using Game1.Util;
using Microsoft.Xna.Framework;

namespace Game1.Command.CollisionHandlerCommands
{
    class PlayerToBlockCommand : ICollisionCommand
    {
        private readonly Game1 game;

        private readonly Vector2 negativeVector = new Vector2(-1, -1);
        private const float moveBlockTime = 1.0f;

        public PlayerToBlockCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute(Collision collision)
        {
            IEnvironment envo = (IEnvironment)collision.collidee;
            IPlayer player = (IPlayer)collision.collider;
            char side = collision.side;

            // side is side of block (collidee)

            if (envo.GetType() == typeof(MovableBlock) && !((MovableBlock)envo).hasMoved)
            {
                ((MovableBlock)envo).Move(Vector2.Multiply(CompassDirectionUtil.GetDirectionVector(side), negativeVector), moveBlockTime);
            }
            else if (envo.GetType() == typeof(Stairs))
            {
                AudioManager.PlayMutex("stairs");
                // TODO: GameStateRoomToBasement
            }
            else
            {
                OpenDoors(envo, player);

                EnterDoors(envo, player);

                Vector2 moveAmount = Vector2.Multiply(new Vector2(collision.intersectionRec.Width, collision.intersectionRec.Height), CompassDirectionUtil.GetDirectionVector(side));
                player.EditPosition(moveAmount);
            }

            if (player.GetType() == typeof(DamagedPlayer) && ((DamagedPlayer)player).stillSlide)
            {
                ((DamagedPlayer)player).stopKnockback(new Vector2(collision.intersectionRec.Width, collision.intersectionRec.Height));
            }

        }

        private void OpenDoors(IEnvironment envo, IPlayer player)
        {
            switch (envo)
            {
                case DoorNLocked _:
                    if (((DoorNLocked)envo).open == 0 && player.PlayerInventory.SubKey())
                        ((DoorNLocked)envo).Open();
                    break;
                case DoorELocked _:
                    if (((DoorELocked)envo).open == 0 && player.PlayerInventory.SubKey())
                        ((DoorELocked)envo).Open();
                    break;
                case DoorSLocked _:
                    if (((DoorSLocked)envo).open == 0 && player.PlayerInventory.SubKey())
                        ((DoorSLocked)envo).Open();
                    break;
                case DoorWLocked _:
                    if (((DoorWLocked)envo).open == 0 && player.PlayerInventory.SubKey())
                        ((DoorWLocked)envo).Open();
                    break;
            }
        }

        private void EnterDoors(IEnvironment envo, IPlayer player)
        {
            if(envo.GetType() == typeof(LoadZone))
            {
                switch(((LoadZone)envo).GetTransitionDirection())
                {
                    case CompassDirection.North:
                        game.SetState(new GameStateRoomToRoomNorth(game));
                        break;
                    case CompassDirection.East:
                        game.SetState(new GameStateRoomToRoomEast(game));
                        break;
                    case CompassDirection.South:
                        //game.SetState(new GameStateRoomToRoomSouth(game));
                        break;
                    case CompassDirection.West:
                        game.SetState(new GameStateRoomToRoomWest(game));
                        break;
                }
            }
        }
    }
}
