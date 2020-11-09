using Microsoft.Xna.Framework;
using ResolutionBuddy; // Nuget package found here: https://www.nuget.org/packages/ResolutionBuddy/2.0.4

namespace Game1.ResolutionManager
{
    public class ResolutionManager1 : IResolutionManager
    {
        private IResolution resolution;

        private Point baseResolution;
        private int scale;

        public ResolutionManager1(Game game, GraphicsDeviceManager graphics, Point baseResolution, int scale)
        {
            this.baseResolution = baseResolution;

            this.scale = scale;

            resolution = new ResolutionComponent(game, graphics, baseResolution, new Point(baseResolution.X * scale, baseResolution.Y * scale), false, true, false);
        }

        public Matrix GetResolutionMatrix()
        {
            return resolution.TransformationMatrix();
        }

        public int GetResolutionScale()
        {
            return scale;
        }
    }
}
