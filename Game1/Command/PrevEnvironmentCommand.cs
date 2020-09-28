using Game1.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Command
{
    class PrevEnvironmentCommand : ICommand
    {
        private Game1 game;

        public PrevEnvironmentCommand(Game1 game)
        {
            this.game = game;
        }
        public void Execute()
        {
            EnvironmentSprite gameSprite = (EnvironmentSprite)game.environmentSprite;
            switch (gameSprite.GetSpriteID())
            {
                case 0:
                    game.environmentSprite = EnvironmentSpriteFactory.instance.createFire();
                    break;
                case 1:
                    game.environmentSprite = EnvironmentSpriteFactory.instance.createRoom();
                    break;
                case 2:
                    game.environmentSprite = EnvironmentSpriteFactory.instance.createFloor();
                    break;
                case 3:
                    game.environmentSprite = EnvironmentSpriteFactory.instance.createBlock();
                    break;
                case 4:
                    game.environmentSprite = EnvironmentSpriteFactory.instance.createStatueFish();
                    break;
                case 5:
                    game.environmentSprite = EnvironmentSpriteFactory.instance.createStatueDragon();
                    break;
                case 6:
                    game.environmentSprite = EnvironmentSpriteFactory.instance.createBlack();
                    break;
                case 7:
                    game.environmentSprite = EnvironmentSpriteFactory.instance.createSand();
                    break;
                case 8:
                    game.environmentSprite = EnvironmentSpriteFactory.instance.createWater();
                    break;
                case 9:
                    game.environmentSprite = EnvironmentSpriteFactory.instance.createStairs();
                    break;
                case 10:
                    game.environmentSprite = EnvironmentSpriteFactory.instance.createBricks();
                    break;
                case 11:
                    game.environmentSprite = EnvironmentSpriteFactory.instance.createLadder();
                    break;
                case 12:
                    game.environmentSprite = EnvironmentSpriteFactory.instance.createDoorNBlank();
                    break;
                case 13:
                    game.environmentSprite = EnvironmentSpriteFactory.instance.createDoorNOpen();
                    break;
                case 14:
                    game.environmentSprite = EnvironmentSpriteFactory.instance.createDoorNLocked();
                    break;
                case 15:
                    game.environmentSprite = EnvironmentSpriteFactory.instance.createDoorNClosed();
                    break;
                case 16:
                    game.environmentSprite = EnvironmentSpriteFactory.instance.createDoorNHole();
                    break;
                case 17:
                    game.environmentSprite = EnvironmentSpriteFactory.instance.createDoorWBlank();
                    break;
                case 18:
                    game.environmentSprite = EnvironmentSpriteFactory.instance.createDoorWOpen();
                    break;
                case 19:
                    game.environmentSprite = EnvironmentSpriteFactory.instance.createDoorWLocked();
                    break;
                case 20:
                    game.environmentSprite = EnvironmentSpriteFactory.instance.createDoorWClosed();
                    break;
                case 21:
                    game.environmentSprite = EnvironmentSpriteFactory.instance.createDoorWHole();
                    break;
                case 22:
                    game.environmentSprite = EnvironmentSpriteFactory.instance.createDoorEBlank();
                    break;
                case 23:
                    game.environmentSprite = EnvironmentSpriteFactory.instance.createDoorEOpen();
                    break;
                case 24:
                    game.environmentSprite = EnvironmentSpriteFactory.instance.createDoorELocked();
                    break;
                case 25:
                    game.environmentSprite = EnvironmentSpriteFactory.instance.createDoorEClosed();
                    break;
                case 26:
                    game.environmentSprite = EnvironmentSpriteFactory.instance.createDoorEHole();
                    break;
                case 27:
                    game.environmentSprite = EnvironmentSpriteFactory.instance.createDoorSBlank();
                    break;
                case 28:
                    game.environmentSprite = EnvironmentSpriteFactory.instance.createDoorSOpen();
                    break;
                case 29:
                    game.environmentSprite = EnvironmentSpriteFactory.instance.createDoorSLocked();
                    break;
                case 30:
                    game.environmentSprite = EnvironmentSpriteFactory.instance.createDoorSClosed();
                    break;
                case 31:
                    game.environmentSprite = EnvironmentSpriteFactory.instance.createDoorSHole();
                    break;
                default:
                    Console.Error.WriteLine("Unknown SpriteID given to PrevEnvironmnetCommand->Execute()");
                    break;
            }
        }
    }
}
