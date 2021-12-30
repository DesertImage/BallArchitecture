using DesertImage.UI;
using UI;

namespace BallArchitectureApp.UI
{
    public struct ShowWindowEvent
    {
        public ushort Id;
        
        public IScreenSettings Settings;

        public bool WithoutAnimation;
    }
}