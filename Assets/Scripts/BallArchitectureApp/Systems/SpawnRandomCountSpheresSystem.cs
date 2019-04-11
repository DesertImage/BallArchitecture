using BallArchitectureApp.Events;
using DesertImage;
using DesertImage.Managers;
using UnityEngine;

namespace BallArchitectureApp.Managers
{
    public class SpawnRandomCountSpheresSystem : ManagerBase, IAwake
    {
        public void onAwake()
        {
            this.Send(new SpawnSpheresEvent {Count = Random.Range(2, 5)});
        }
    }
}