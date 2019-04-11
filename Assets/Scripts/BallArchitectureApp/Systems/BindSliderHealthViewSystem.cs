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
    public class BindSliderHealthViewSystem : ManagerBase, IAwake, IListen<SphereSpawnedEvent>
    {
        private Transform _rightPanelTransform;
        
        public void onAwake()
        {
            this.Listen<SphereSpawnedEvent>();

            var dataCanvas = Core.Instance.get<DataCanvas>();

            _rightPanelTransform = dataCanvas.Value.transform.GetChild(1);
        }

        public void handleCallback(SphereSpawnedEvent arguments)
        {
            var bar = this.Spawn<SliderHealthView>(ObjectsId.SliderHealthView, _rightPanelTransform);
            
            bar.bind(arguments.Value);
        }
    }
}