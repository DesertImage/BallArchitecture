using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Components;
using DesertImage.Extensions;
using Framework.Components;
using Framework.Managers;
using UniRx;
using UnityEngine;

namespace DesertImage
{
    public class Core : IComponentHolder, IStart, IDisposable
    {
        public static Core Instance { get; private set; }

        public bool IsInitialized;

        public bool IsStartInvoked { get; private set; }
        public bool IsLateStartInvoked { get; private set; }

        private readonly Dictionary<int, object> _data = new Dictionary<int, object>();

        private readonly List<IStart> _starts = new List<IStart>();
        private readonly List<IStart> _lateStart = new List<IStart>();

        public Core()
        {
            Instance = this;
        }

        public virtual void OnStart()
        {
            if (IsStartInvoked)
            {
                foreach (var start in _lateStart)
                {
                    start.OnStart();
                }

                IsLateStartInvoked = true;
            }
            else
            {
                foreach (var start in _starts)
                {
                    start.OnStart();
                }
            }

            IsStartInvoked = true;
        }

        #region ADD

        /// <summary>
        /// Add object to Core
        /// </summary>
        /// <param name="instance"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Add<T>(T instance)
        {
            var (isAdded, _) = AddProcess(instance);

            if (!isAdded) return instance;

            if (instance is IAsyncAwake asyncAwake)
            {
                Observable.FromMicroCoroutine(asyncAwake.OnAsyncAwake).Subscribe();
            }

            if (instance is IStart start)
            {
                _starts.Add(start);

                if (IsStartInvoked)
                {
                    if (IsLateStartInvoked)
                    {
                        start.OnStart();
                    }
                    else
                    {
                        _lateStart.Add(start);
                    }
                }
            }

            return instance;
        }

        /// <summary>
        /// Add object to Core
        /// </summary>
        /// <param name="instance"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerator AddAsync<T>(T instance)
        {
            var (isAdded, _) = AddProcess(instance);

            if (!isAdded) yield break;

            if (instance is IAsyncAwake asyncAwake)
            {
                var process = asyncAwake.OnAsyncAwake();

                while (process.MoveNext())
                {
                    yield return null;
                }
            }

            if (instance is IStart start)
            {
                _starts.Add(start);

                if (IsStartInvoked)
                {
                    if (IsLateStartInvoked)
                    {
                        start.OnStart();
                    }
                    else
                    {
                        _lateStart.Add(start);
                    }
                }
            }

            yield return null;
        }

        /// <summary>
        /// Add object to Core. Using pool
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Add<T>() where T : new()
        {
            return Add(new T());
        }

        private (bool, T) AddProcess<T>(T instance)
        {
            if (instance is IComponentWrapper componentWrapper)
            {
                componentWrapper.Link(this);

                return default;
            }

            var hash = instance.GetCachedHashCode();

            var isInitialComponent = instance is IInitialComponent;

            if (_data.ContainsKey(hash) && !isInitialComponent)
            {
#if DEBUG
                Debug.Log($"Core already contains {typeof(T)}");
#endif
                return default;
            }

            if (!isInitialComponent)
            {
                _data.Add(hash, instance);
            }

            if (instance is ISwitchable switchable) switchable.Activate();

            if (instance is IAwake awake) awake.OnAwake();

            if (instance is ITick tick)
            {
                Get<ManagerUpdate>().Add(tick);
            }

            if (instance is ITickLate tickLate)
            {
                Get<ManagerUpdate>().Add(tickLate);
            }

            if (instance is ITickFixed tickFixed)
            {
                Get<ManagerUpdate>().Add(tickFixed);
            }

            return (true, instance);
        }

        #endregion

        /// <summary>
        /// Remove object from Core
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Remove<T>()
        {
            var hash = HashCodeTypeTool.GetCachedHashCode<T>();

            if (!_data.ContainsKey(hash)) return;

            var disposable = _data[hash] as IDisposable;

            disposable?.Dispose();

            _data.Remove(hash);
        }

        /// <summary>
        /// Get object from Core
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>()
        {
            var hash = HashCodeTypeTool.GetCachedHashCode<T>();

            if (!Instance._data.TryGetValue(hash, out var obj))
            {
#if DEBUG
                UnityEngine.Debug.LogWarning($"[Core] there is no component {typeof(T)}");
#endif
            }

            return (T)obj;
        }

        /// <summary>
        /// Replace object with new instance
        /// </summary>
        /// <param name="instance"></param>
        /// <typeparam name="T"></typeparam>
        public void Replace<T>(T instance)
        {
            var hash = HashCodeTypeTool.GetCachedHashCode<T>();

            if (Instance._data.TryGetValue(hash, out var obj))
            {
                Instance._data[hash] = instance;
            }
        }

        public virtual void Dispose()
        {
            // Instance = null;

            foreach (var value in _data.Values.Reverse())
            {
                var disposable = value as IDisposable;

                disposable?.Dispose();
            }
        }
    }
}