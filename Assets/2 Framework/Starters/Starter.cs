using System.Collections;
using DesertImage.Entities;
using DesertImage.Managers;
using DesertImage.Pools;
using Framework.Components;
using Framework.Managers;
using Managers;
using UniRx;
using UnityEngine;

namespace DesertImage.Starters
{
    public abstract class Starter : MonoBehaviour
    {
        public static Starter Instance
        {
            get
            {
                if (!_instance)
                    _instance = FindObjectOfType<Starter>();

                return _instance;
            }
        }

        private static Starter _instance;

        [SerializeField] private ScriptableObject[] _modules;

        protected Core Core;

        private void Awake()
        {
            Core = new Core();

            Observable.FromMicroCoroutine(InitData)
                .SelectMany(InitManagers)
                .SelectMany(InitModules)
                .SelectMany(InitSystems)
                .Subscribe(unit => Start());
        }

        private void Start()
        {
            Core?.OnStart();
        }

        protected virtual IEnumerator InitData()
        {
            var wrappers = GetComponents<ComponentWrapper>();

            foreach (var componentWrapper in wrappers)
            {
                componentWrapper.Link(Core);
            }

            yield break;
        }

        protected virtual IEnumerator InitModules()
        {
            if (_modules == null) yield break;

            foreach (var module in _modules)
            {
                if (!module) continue;

                if (module is IAsyncAwake)
                {
                    var process = Core.AddAsync(module);

                    while (process.MoveNext())
                    {
                        yield return null;
                    }
                }
                else
                {
                    Core.Add(module);
                }
            }
        }

        protected virtual IEnumerator InitManagers()
        {
            Core.Add(new ManagerUpdate());

            yield return null;

            Core.Add(new ManagerEvents());

            yield return null;

            Core.Add(new EntitiesManager());

            yield return null;

            Core.Add(new ManagerTimers());

            yield return null;

            Core.Add(new TimersUpdater());

            yield return null;

            Core.Add(new PoolComponents());

            yield return null;

            var poolsObj = GameObject.Find("Pools");
            if (!poolsObj)
            {
                poolsObj = new GameObject("Pools");
            }

            Core.Add(new PoolGameObject(poolsObj.transform));

            Core.Add(new FactorySpawn());
            Core.Add(new FactorySound());
            Core.Add(new FactoryFx());
        }

        protected virtual IEnumerator InitSystems()
        {
            Core.IsInitialized = true;

            yield break;
        }

        private void OnDestroy()
        {
            Core?.Dispose();
        }
    }
}