using System;
using System.Collections.Generic;
using DesertImage.Behaviours;
using DesertImage.Extensions;
using DesertImage.Managers;
using Framework.Components;
using Framework.Managers;
using UnityEngine;

namespace DesertImage.Entities
{
    public class Entity : IEntity, ITick, ITickFixed, ITickLate
    {
        public event Action<IEntity, int> OnComponentAdded;
        public event Action<IEntity, int> OnComponentRemoved;
        public event Action<IEntity, int> OnComponentUpdated;
        public event Action OnDispose;

        public int Id { get; }

        public IComponent[] Components { get; private set; }

        private readonly ManagerEvents _eventsManager = new ManagerEvents();

        private readonly ManagerUpdate _updateManager = new ManagerUpdate();

        protected readonly Dictionary<int, object> components = new Dictionary<int, object>();

        private readonly List<IBehaviour> _behaviours = new List<IBehaviour>();

        private readonly List<IStart> _starts = new List<IStart>();

        public Entity(int id, int componentSlotsCount = 10)
        {
            Id = id;
            Components = new IComponent[componentSlotsCount];
        }

        public void OnStart()
        {
            foreach (var start in _starts)
            {
                start.OnStart();
            }
        }
        
        #region ADD

        public T Add<T>(T component)
        {
            if (component is IComponentWrapper componentWrapper)
            {
                componentWrapper.Link(this);
                
                return default;
            }
            
            if (!(component is IComponent) && !(component is IBehaviour))
            {
#if UNITY_EDITOR
                Debug.LogError("CANT ADD COMPONENT THIS IS NOT IDATA AND NOT BEHAVIOUR " + component);
#endif
                return default;
            }

            var hash = HashCodeTypeTool.GetCachedHashCode<T>();

            if (components.ContainsKey(hash))
            {
                return default;
            }

            if (component is IStart start) _starts.Add(start);

            if (component is IBehaviour beh) AddBehaviour(beh);

            if (component is ISwitchable switchable) switchable.Activate();

            if (component is IComponent comp)
            {
                Components[comp.Id] = comp;
                OnComponentAdded?.Invoke(this, comp.Id);
            }

            components.Add(hash, component);

            return component;
        }

        public T Add<T>() where T : new()
        {
            return Add(ComponentsTool.GetInstanceFromPool<T>());
        }

        private void AddBehaviour(IBehaviour behaviour)
        {
            if (_behaviours.Contains(behaviour))
            {
#if UNITY_EDITOR
                Debug.LogError(this + " already contains behaviour " + behaviour);
#endif
                return;
            }

            _behaviours.Add(behaviour);

            var beh = behaviour;

            beh?.Link(this);

            _updateManager.Add(beh);
        }
        
        #endregion

        #region REMOVE

        public void Remove<T>()
        {
            var hash = HashCodeTypeTool.GetCachedHashCode<T>();

            if (!components.TryGetValue(hash, out var component)) return;

            if (component is IComponent comp)
            {
                Components[comp.Id] = null;
                OnComponentRemoved?.Invoke(this, comp.Id);
            }

            if (component is ISwitchable switchable)
            {
                switchable.Deactivate();
            }
            
            components.Remove(hash);
        }

        #endregion

        #region GET

        public T Get<T>()
        {
            components.TryGetValue(HashCodeTypeTool.GetCachedHashCode<T>(), out var obj);

            return (T) obj;
        }

        #endregion

        #region POOLING STUFF

        public void OnCreate()
        {
            Core.Instance.Get<ManagerUpdate>().Add(this);

            InitStuff();
        }


        public void ReturnToPool()
        {
            Core.Instance.Get<ManagerUpdate>().Remove(this);

            _eventsManager.Clear();
            _updateManager.Clear();

            components.Clear();

            foreach (var behaviour in _behaviours)
            {
                behaviour.Dispose();
            }
            _behaviours.Clear();
            
            _starts.Clear();

            Core.Instance.Get<EntitiesManager>().ReturnSubject(this);
            
            OnDispose?.Invoke();
        }

        #endregion

        #region INIT

        protected virtual void InitStuff()
        {
        }

        #endregion

        #region EVENT MANAGING

        public void ListenEvent<T>(IListen listener)
        {
            _eventsManager.Add<T>(listener);
        }

        public void UnlistenEvent<T>(IListen listener)
        {
            _eventsManager.Remove<T>(listener);
        }

        public void SendEvent<T>(T arguments)
        {
            _eventsManager.Send<T>(arguments);
        }

        #endregion

        public void Tick()
        {
            _updateManager.Tick();
        }

        public void FixedTick()
        {
            _updateManager.FixedTick();
        }

        public void LateTick()
        {
            _updateManager.LateTick();
        }

        public override bool Equals(object obj)
        {
            if (obj is IEntity entity)
            {
                return Id == entity.Id;
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}