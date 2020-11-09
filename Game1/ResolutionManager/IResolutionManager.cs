using Microsoft.Xna.Framework;

namespace Game1.ResolutionManager
{
    public interface IResolutionManager
    {
        Matrix GetResolutionMatrix();

        int GetResolutionScale();
    }
}
