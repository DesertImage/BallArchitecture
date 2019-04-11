using DesertImage;
using DesertImage.Enums;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Framework.Extensions
{
    public static class FactorySpawnExtenstion
    {
        private static FactorySpawn _factorySpawn;

        private static void Init()
        {
            if (_factorySpawn != null) return;

            _factorySpawn = Core.Instance.get<FactorySpawn>();

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;

            _factorySpawn = null;
        }

        #region SPAWN

        public static GameObject Spawn(this object sender, ObjectsId id, Vector3 position, Quaternion rotation,
            Transform parent = null)
        {
            Init();

            return _factorySpawn.spawn(id, position, rotation, parent);
        }

        public static GameObject Spawn(this object sender, ObjectsId id)
        {
            Init();

            return _factorySpawn.spawn(id, Vector3.zero, Quaternion.identity, null);
        }

        
        public static T Spawn<T>(this object sender, ObjectsId id)
        {
            Init();

            return _factorySpawn.spawn<T>(id);
        }

        public static T Spawn<T>(this object sender, ObjectsId id, Vector3 position)
        {
            Init();

            return _factorySpawn.spawn<T>(id, position);
        }

        public static T Spawn<T>(this object sender, ObjectsId id, Vector3 position, Quaternion rotation)
        {
            Init();

            return _factorySpawn.spawn<T>(id, position, rotation);
        }

        public static T Spawn<T>(this object sender, ObjectsId id, Transform parent)
        {
            Init();

            return _factorySpawn.spawn<T>(id, parent);
        }

        #endregion
    }
}