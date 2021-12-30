using DesertImage;
using DesertImage.Extensions;
using DesertImage.Managers;

namespace BallArchitectureApp
{
    public class SpheresManagingSystem : SystemBase, IAwake, IListen<SphereSpawnedEvent>, IListen<DieEvent>
    {
        private DataSpheres _dataSpheres;

        public void OnAwake()
        {
            this.ListenGlobalEvent<SphereSpawnedEvent>();
            this.ListenGlobalEvent<DieEvent>();

            _dataSpheres = Core.Instance.Get<DataSpheres>();
        }

        public void handleCallback(SphereSpawnedEvent arguments)
        {
            _dataSpheres.Values.Add(arguments.Value);
        }

        public void handleCallback(DieEvent arguments)
        {
            if (_dataSpheres.Values.Count == 0) return;

            _dataSpheres.Values.RemoveAt(0);
        }
    }
}