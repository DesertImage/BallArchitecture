using System;
using System.Collections;
using DesertImage;
using DesertImage.Components;
using DesertImage.Entities;
using DesertImage.Extensions;
using Framework.Components;

namespace Entities
{
    public class EntityMono : MonoBehaviourPoolable, IEntity
    {
        public event Action<IEntity, int> OnComponentAdded;
        public event Action<IEntity, int> OnComponentRemoved;
        public event Action<IEntity, int> OnComponentUpdated;
        public event Action OnDispose;

        public int Id => _localEntity?.Id ?? 0;

        public IEntity LocalEntity => _localEntity;

        public IComponent[] Components => _localEntity.Components;

        public bool AutoInitialize;

        private IComponentWrapper[] _componentWrappers;
        private IEntityExtension[] _entityExtensions;

        private IEntity _localEntity;

        private bool _isInitialized = false;

        private IEnumerator Start()
        {
            if (!AutoInitialize) yield break;

            while (!Core.Instance?.IsInitialized ?? false)
            {
                yield return null;
            }

            OnCreate();
            OnStart();
        }

        public virtual void OnStart()
        {
            _localEntity?.OnStart();
        }

        public T Add<T>(T component)
        {
            return _localEntity.Add(component);
        }

        public T Add<T>() where T : new()
        {
            return _localEntity.Add<T>();
        }

        public void Remove<T>()
        {
            _localEntity.Remove<T>();
        }

        public override void OnCreate()
        {
            base.OnCreate();

            if (_isInitialized)
            {
#if DEBUG
                UnityEngine.Debug.LogWarning($"[EntityMono] entity {name} (id: {Id}) is initialized already");
#endif
                return;
            }

            _localEntity = this.GetNewEntity();

            _localEntity.OnDispose += LocalEntityOnDispose;

            this.AddComponentFromPool<DataTransform>().Value = transform;

            // _localEntity.OnCreate();

            _componentWrappers ??= GetComponents<IComponentWrapper>();

            if (_componentWrappers.Length > 0)
            {
                foreach (var componentWrapper in _componentWrappers)
                {
                    componentWrapper.Link(_localEntity);
                }
            }

            _isInitialized = true;

            _entityExtensions ??= GetComponents<IEntityExtension>();
            if (_entityExtensions.Length > 0)
            {
                foreach (var entityExtension in _entityExtensions)
                {
                    entityExtension.Link(this);
                }
            }
            
            OnStart();
        }

        private void LocalEntityOnDispose()
        {
            base.ReturnToPool();

            _isInitialized = false;

            if (_localEntity == null) return;

            _localEntity.OnDispose -= LocalEntityOnDispose;
        }

        public override void ReturnToPool()
        {
            base.ReturnToPool();

            _isInitialized = false;

            if (_localEntity == null) return;

            _localEntity.OnDispose -= LocalEntityOnDispose;
            _localEntity.ReturnToPool();
        }

        public T Get<T>()
        {
            return _isInitialized ? _localEntity.Get<T>() : default;
        }

        public void ListenEvent<T>(IListen listener)
        {
#if DEBUG
            if (_localEntity == null)
            {
#if DEBUG
                UnityEngine.Debug.LogWarning("[EntityMono] entity is not initialized yet!");
#endif
            }
#endif
            _localEntity?.ListenEvent<T>(listener);
        }

        public void UnlistenEvent<T>(IListen listener)
        {
            _localEntity?.UnlistenEvent<T>(listener);
        }

        public void SendEvent<T>(T arguments)
        {
            _localEntity?.SendEvent(arguments);
        }

        public override string ToString()
        {
            return name;
        }
    }
}