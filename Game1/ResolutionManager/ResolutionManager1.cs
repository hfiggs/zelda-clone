/* Author: Hunter Figgs.3 */

using Microsoft.Xna.Framework;
using ResolutionBuddy; // Nuget package found here: https://www.nuget.org/packages/ResolutionBuddy/2.0.4
using System;

namespace Game1.ResolutionManager
{
    public class ResolutionManager1 : IResolutionManager
    {
        private readonly Game1 game;
        private readonly GraphicsDeviceManager graphics;
        private readonly Point virtualResolution;
        private readonly Point scaledResolution;

        private IResolution resolution;

        private bool isFullscreen;

        public ResolutionManager1(Game1 game, GraphicsDeviceManager graphics, Point virtualResolution, Point scaledResolution)
        {
            this.game = game;
            this.graphics = graphics;
            this.virtualResolution = virtualResolution;
            this.scaledResolution = scaledResolution;

            isFullscreen = false;

            resolution = new ResolutionComponent(game, graphics, virtualResolution, scaledResolution, isFullscreen, true, false);
        }

        public Matrix GetResolutionMatrix()
        {
            return resolution.TransformationMatrix();
        }

        public Point GetVirtualResolution()
        {
            return virtualResolution;
        }

        public float GetResolutionScale()
        {
            var scaleY = game.GetWindowDimensions().Y / resolution.VirtualResolution.Y;

            var scaleX = game.GetWindowDimensions().X / resolution.VirtualResolution.X;

            return Math.Min(scaleX, scaleY);
        }

        public void ToggleFullscreen()
        {
            isFullscreen = !isFullscreen;

            game.Services.RemoveService(typeof(IResolution));
            game.Components.Remove((IGameComponent)resolution);

            resolution = new ResolutionComponent(game, graphics, virtualResolution, scaledResolution, isFullscreen, true, false);
        }
    }
}
