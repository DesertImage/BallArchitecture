using BallArchitectureApp.Components;
using BallArchitectureApp.Events;
using DesertImage;
using DesertImage.Managers;

namespace BallArchitectureApp.Managers
{
    public class SpheresCollectionSystem : ManagerBase, IAwake, IListen<SphereSpawnedEvent>, IListen<DieEvent>
    {
        private DataSpheres _dataSpheres;

        public void onAwake()
        {
            this.Listen<SphereSpawnedEvent>();
            this.Listen<DieEvent>();

            _dataSpheres = Core.Instance.get<DataSpheres>();
        }

        public void handleCallback(SphereSpawnedEvent arguments)
        {
            _dataSpheres.Spheres.Add(arguments.Value);
        }

        public void handleCallback(DieEvent arguments)
        {
            if(!_dataSpheres.Spheres.Contains(arguments.Value)) return;

            _dataSpheres.Spheres.Remove(arguments.Value);
        }
    }
}