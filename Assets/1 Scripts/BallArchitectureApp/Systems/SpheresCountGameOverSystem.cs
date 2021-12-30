using DesertImage;
using DesertImage.Extensions;
using DesertImage.Managers;

namespace BallArchitectureApp.Managers
{
    public class SpheresCountGameOverSystem : SystemBase, IAwake, IListen<DieEvent>
    {
        private DataSpheres _dataSpheres;

        public void OnAwake()
        {
            this.ListenGlobalEvent<DieEvent>();

            _dataSpheres = Core.Instance.Get<DataSpheres>();
        }

        public void handleCallback(DieEvent arguments)
        {
            if (_dataSpheres.Values.Count > 0) return;

            this.SendGlobalEvent(new GameOverEvent());
        }
    }
}