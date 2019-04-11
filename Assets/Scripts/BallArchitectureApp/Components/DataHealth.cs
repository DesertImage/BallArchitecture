using DesertImage;
using UniRx;

namespace BallArchitectureApp.Components
{
    public class DataHealth : IDataComponent
    {
        public ReactiveProperty<float> Health;

        public float StartHealth;
    }
}