using DesertImage;
using DesertImage.Extensions;
using DesertImage.Managers;
using UnityEngine;

namespace BallArchitectureApp
{
    public class SpawnRandomCountSpheresSystem : SystemBase, IAwake
    {
        public void OnAwake()
        {
            this.SendGlobalEvent(new SpheresSpawnEvent { Count = Random.Range(2, 5) });
        }
    }
}