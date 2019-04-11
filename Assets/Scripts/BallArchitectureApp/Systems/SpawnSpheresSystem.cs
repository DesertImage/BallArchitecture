using BallArchitectureApp.Events;
using DesertImage;
using DesertImage.Enums;
using DesertImage.Managers;
using DesertImage.Subjects;
using Framework.Extensions;
using UnityEngine;

namespace BallArchitectureApp.Managers
{
    public class SpawnSpheresSystem : ManagerBase, IAwake, IListen<SpawnSpheresEvent>
    {
        public void onAwake()
        {
            this.Listen<SpawnSpheresEvent>();
        }

        private void SpawnSpheres(int count)
        {
            for (var i = 0; i < count; i++)
            {
                var ball = this.Spawn(ObjectsId.Sphere);

                ball.transform.position = new Vector3((i - count / 2) * 11f, 5f, 5f);

                var sphereSubject = ball.GetComponent<ISubject>();

                sphereSubject.send(new SetColorEvent {Value = Random.ColorHSV(i * 0.2f, i * 0.2f, 1, 1, 1, 1)});

                this.Send(new SphereSpawnedEvent {Value = sphereSubject});
            }
        }

        public void handleCallback(SpawnSpheresEvent arguments)
        {
            SpawnSpheres(arguments.Count);
        }
    }
}