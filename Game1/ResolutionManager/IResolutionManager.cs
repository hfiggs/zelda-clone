/* Author: Hunter Figgs.3 */

using Microsoft.Xna.Framework;

namespace Game1.ResolutionManager
{
    public interface IResolutionManager
    {
        Matrix GetResolutionMatrix();

        Point GetVirtualResolution();

        float GetResolutionScale();

        void ToggleFullscreen();
    }
}
