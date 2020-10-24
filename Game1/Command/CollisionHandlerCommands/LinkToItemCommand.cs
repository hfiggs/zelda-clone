using Game1.Collision_Handling;
using Game1.Item;
using Game1.Player;
using System;

namespace Game1.Command.CollisionHandlerCommands
{
    class LinkToItemCommand : ICollisionCommand
    {
        private const int fairyHalfHearts = 6;
        private const int heartHalfHearts = 2;

        private const int yellowRupee = 1;
        private const int blueRupee = 5;

        public LinkToItemCommand()
        {

        }

        public void Execute(Collision collision)
        {
            IPlayer player = (IPlayer)collision.collider;
            IItem item = (IItem)collision.collidee;

            switch(item)
            {
                case Bomb _:
                    player.PlayerInventory.AddBomb();
                    break;
                case Bow _:
                    player.PlayerInventory.HasBow = true;
                    break;
                case Clock _:
                    throw new NotImplementedException();
                case Compass _:
                    player.PlayerInventory.HasCompass = true;
                    break;
                case Fairy _:
                    player.PlayerInventory.AddHealth(fairyHalfHearts);
                    break;
                case Heart _:
                    player.PlayerInventory.AddHealth(heartHalfHearts);
                    break;
                case HeartContainer _:
                    player.PlayerInventory.AddMaxHeart();
                    break;
                case ItemBoomerang _:
                    player.PlayerInventory.HasBoomerang = true;
                    break;
                case Key _:
                    player.PlayerInventory.AddKey();
                    break;
                case Map _:
                    player.PlayerInventory.HasMap = true;
                    break;
                case RupeeYellow _:
                    player.PlayerInventory.AddRupees(yellowRupee);
                    break;
                case RupeeBlue _:
                    player.PlayerInventory.AddRupees(blueRupee);
                    break;
                case Triforce _:
                    player.PlayerInventory.AddTriforce();
                    break;
            }

            item.ShouldDelete = true;
        }
    }
}
