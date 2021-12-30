using BallArchitectureApp;
using BallArchitectureApp.Spawning;
using DesertImage;
using DesertImage.Components;
using DesertImage.Entities;
using DesertImage.Extensions;
using DesertImage.Managers;
using UnityEngine;

namespace BallArchitectureApp
{
    public class SpheresSpawnSystem : SystemBase, IListen<SpheresSpawnEvent>
    {
        public override void Activate()
        {
            base.Activate();

            this.ListenGlobalEvent<SpheresSpawnEvent>();
        }

        public override void Deactivate()
        {
            base.Deactivate();

            this.UnlistenGlobalEvent<SpheresSpawnEvent>();
        }

        private void SpawnSpheres(int count)
        {
            for (var i = 0; i < count; i++)
            {
                var entity = ObjectsId.Sphere.SpawnAs<IEntity>();

                var dataTransform = entity.Get<DataTransform>();

                dataTransform.Value.position = new Vector3((i - count / 2) * 11f, 5f, 5f);

                entity.SendEvent(new ColorSetEvent { Value = Random.ColorHSV(i * 0.2f, i * 0.2f, 1, 1, 1, 1) });

                this.SendGlobalEvent(new SphereSpawnedEvent { Value = entity });
            }
        }

        public void handleCallback(SpheresSpawnEvent arguments)
        {
            SpawnSpheres(arguments.Count);
        }
    }
}