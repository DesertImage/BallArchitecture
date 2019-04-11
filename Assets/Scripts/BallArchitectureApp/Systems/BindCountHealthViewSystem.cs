using BallArchitectureApp.Components;
using BallArchitectureApp.Events;
using BallArchitectureApp.UI;
using DesertImage;
using DesertImage.Enums;
using DesertImage.Managers;
using Framework.Extensions;
using UnityEngine;

namespace BallArchitectureApp.Managers
{
    public class BindCountHealthViewSystem : ManagerBase, IAwake, IListen<SphereSpawnedEvent>
    {
        private Transform _leftPanelTransform;
        
        public void onAwake()
        {
            this.Listen<SphereSpawnedEvent>();

            var dataCanvas = Core.Instance.get<DataCanvas>();

            _leftPanelTransform = dataCanvas.Value.transform.GetChild(0);
        }

        public void handleCallback(SphereSpawnedEvent arguments)
        {
            var bar = this.Spawn<CountHealthView>(ObjectsId.CountHealthView, _leftPanelTransform);
            
            bar.bind(arguments.Value);
        }
    }
}