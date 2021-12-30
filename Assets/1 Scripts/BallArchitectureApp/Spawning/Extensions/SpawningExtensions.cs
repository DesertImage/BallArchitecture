using DesertImage.Extensions;
using UnityEngine;

namespace BallArchitectureApp.Spawning
{
    public static class SpawningExtensions
    {
        public static T SpawnAs<T>(this ObjectsId id)
        {
            return FactorySpawnExtenstion.Spawn<T>(null, (ushort) id);
        }
        
        public static T SpawnAs<T>(this ObjectsId id, Transform parent)
        {
            return FactorySpawnExtenstion.Spawn<T>(null, (ushort) id, parent);
        }
        
        public static T SpawnAs<T>(this ObjectsId id, Vector2 position)
        {
            return FactorySpawnExtenstion.Spawn<T>(null, (ushort) id, position);
        }
        
        public static T SpawnAs<T>(this ObjectsId id, Vector2 position, Transform parent)
        {
            return FactorySpawnExtenstion.Spawn<T>(null, (ushort) id, position, parent);
        }
        
        public static T SpawnAs<T>(this ObjectsId id, Vector3 position)
        {
            return FactorySpawnExtenstion.Spawn<T>(null, (ushort) id, position);
        }
        
        public static T SpawnAs<T>(this ObjectsId id, Vector3 position, Quaternion rotation) where T : class
        {
            return FactorySpawnExtenstion.Spawn<T>(null, (ushort) id, position, rotation);
        }
        
        public static T SpawnAs<T>(this ushort id)
        {
            return FactorySpawnExtenstion.Spawn<T>(null, id);
        }
        
        public static T SpawnAs<T>(this ushort id, Vector3 position)
        {
            return FactorySpawnExtenstion.Spawn<T>(null, id, position);
        }
        
        public static T SpawnAs<T>(this ushort id, Transform parent)
        {
            return FactorySpawnExtenstion.Spawn<T>(null, id, parent);
        }
    }
}