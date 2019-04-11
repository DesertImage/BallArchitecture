using BallArchitectureApp.Components;
using BallArchitectureApp.Events;
using DesertImage;
using DesertImage.Managers;

namespace BallArchitectureApp.Managers
{
    public class SpheresCountGameOverSystem : ManagerBase, IAwake, IListen<DieEvent>
    {
        private DataSpheres _dataSpheres;
        
        public void onAwake()
        {
            this.Listen<DieEvent>();

            _dataSpheres = Core.Instance.get<DataSpheres>();
        }

        public void handleCallback(DieEvent arguments)
        {
            if(_dataSpheres.Spheres.Count > 0) return;
            
            this.Send(new GameOverEvent());
        }
    }
}