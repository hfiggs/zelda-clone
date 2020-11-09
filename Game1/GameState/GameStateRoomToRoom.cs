using Game1.Controller;
using Game1.ResolutionManager;
using Game1.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Game1.GameState
{
    class GameStateRoomToRoom : IGameState
    {
        private readonly Game1 game;
        private readonly List<IController> controllerList;

        private const float transitionSpeed = 0.1f; // pixels per ms

        private const float hudOffset = -136f;

        private Vector2 roomDimensions = new Vector2(256, 176);

        private Vector2 oldRoomStartOffset = new Vector2(0, 40);
        private Vector2 oldRoomEndOffset;
        private Vector2 oldRoomOffset;

        private Vector2 newRoomStartOffset;
        private Vector2 newRoomEndOffset;
        private Vector2 newRoomOffset;

        private Vector2 directionVector;
        private readonly CompassDirection direction;

        public GameStateRoomToRoom(Game1 game, CompassDirection direction)
        {
            this.game = game;

            controllerList = new List<IController>
            {
                new KeyboardTransitionController(game),
                new GamepadTransitionController(game, PlayerIndex.One)
            };

            directionVector = CompassDirectionUtil.GetDirectionVector(direction);
            this.direction = direction;

            oldRoomEndOffset = Vector2.Subtract(oldRoomStartOffset, Vector2.Multiply(directionVector, roomDimensions));
            oldRoomOffset = oldRoomStartOffset;

            newRoomStartOffset = Vector2.Add(oldRoomStartOffset, Vector2.Multiply(directionVector, roomDimensions));
            newRoomEndOffset = oldRoomStartOffset;
            newRoomOffset = newRoomStartOffset;
        }

        public void Update(GameTime gameTime)
        {
            foreach (IController controller in controllerList)
            {
                controller.Update();
            }

            Mouse.SetPosition(150, 150);

            var ms = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            oldRoomOffset = Vector2.Subtract(oldRoomOffset, Vector2.Multiply(directionVector, new Vector2(ms * transitionSpeed, ms * transitionSpeed)));
            newRoomOffset = Vector2.Subtract(newRoomOffset, Vector2.Multiply(directionVector, new Vector2(ms * transitionSpeed, ms * transitionSpeed)));

            var vectorDiff = Vector2.Subtract(oldRoomOffset, oldRoomEndOffset);

            switch(direction)
            {
                case CompassDirection.North:
                    if (vectorDiff.Y >= 0f)
                    {
                        game.Screen.CurrentRoomKey = GetNewRoomKey();
                        game.SetState(new GameStateRoom(game));
                    }
                    break;
                case CompassDirection.East:
                    if (vectorDiff.X <= 0f)
                    {
                        game.Screen.CurrentRoomKey = GetNewRoomKey();
                        game.SetState(new GameStateRoom(game));
                    }
                    break;
                case CompassDirection.South:
                    if (vectorDiff.Y <= 0f)
                    {
                        game.Screen.CurrentRoomKey = GetNewRoomKey();
                        game.SetState(new GameStateRoom(game));
                    }
                    break;
                case CompassDirection.West:
                    if (vectorDiff.X >= 0f)
                    {
                        game.Screen.CurrentRoomKey = GetNewRoomKey();
                        game.SetState(new GameStateRoom(game));
                    }
                    break;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, IResolutionManager resolutionManager)
        {
            game.GraphicsDevice.Clear(Color.Black);

            var drawMatrix = resolutionManager.GetResolutionMatrix();


            drawMatrix.Translation = new Vector3(oldRoomOffset.X * resolutionManager.GetResolutionScale(), oldRoomOffset.Y * resolutionManager.GetResolutionScale(), 0);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, drawMatrix);

            game.Screen.Draw(spriteBatch);

            spriteBatch.End();


            var newRoom = game.Screen.RoomsDict[GetNewRoomKey()];

            drawMatrix.Translation = new Vector3(newRoomOffset.X * resolutionManager.GetResolutionScale(), newRoomOffset.Y * resolutionManager.GetResolutionScale(), 0);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, drawMatrix);

            newRoom.Draw(spriteBatch);

            spriteBatch.End();


            drawMatrix.Translation = new Vector3(0, hudOffset * resolutionManager.GetResolutionScale(), 0);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, drawMatrix);

            game.HUD.Draw(spriteBatch, new Vector2(0, 0), Color.White);

            spriteBatch.End();


            drawMatrix.Translation = new Vector3(0, 0, 0);
        }

        private (char, int) GetNewRoomKey()
        {
            var oldRoomKey = game.Screen.CurrentRoomKey;

            var newRoomKey = (oldRoomKey.Item1, oldRoomKey.Item2);

            switch(direction)
            {
                case CompassDirection.North:
                    newRoomKey.Item1--;
                    break;
                case CompassDirection.East:
                    newRoomKey.Item2++;
                    break;
                case CompassDirection.South:
                    newRoomKey.Item1++;
                    break;
                case CompassDirection.West:
                    newRoomKey.Item2--;
                    break;
            }

            return newRoomKey;
        }
    }
}
