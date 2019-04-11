using System.Collections.Generic;
using DesertImage.Managers;
using BallArchitectureApp.Behaviours.Interfaces;
using UnityEngine;

namespace DesertImage.Subjects
{
    public class Subject : ISubject
    {
        #region PROPERTIS

        public Dictionary<int, object>.KeyCollection Components
        {
            get { return _components.Keys; }
        }

        #endregion
        
        #region PRIVATE

        private readonly ManagerEvents _managerEvents = new ManagerEvents();

        private ManagerUpdate _managerUpdate = new ManagerUpdate();
        
        private readonly Dictionary<int, object> _components = new Dictionary<int, object>();
            
        private readonly List<IBehaviour> _behaviours = new List<IBehaviour>();
        
        private readonly List<IStart> _starts = new List<IStart>();

        #endregion

        #region MONO BEHVAIOUR METHODS

        private void Awake()
        {
        }

        #endregion

        #region PUBLIC METHODS

        public void add<T>(T component)
        {
            Add(component);
        }

        public void add<T>() where T : new()
        {
            Add<T>();
        }

        public void remove<T>()
        {
            Remove<T>();
        }

        public void listen<T>(IListen listener)
        {
            Listen<T>(listener);
        }

        public void unlisten<T>(IListen listener)
        {
            Unlisten<T>(listener);
        }

        public void send<T>(T arguments)
        {
            Send<T>(arguments);
        }

        public T get<T>()
        {
            return Get<T>();
        }

        public void onCreate()
        {
            Init();
        }

        public void onStart()
        {
            foreach (var start in _starts)
            {
                start.onStart();
            }
        }
        
        public void returnToPool()
        {
            Destroy();
        }
        
        #endregion

        #region INIT

        private void Init()
        {
           Core.Instance.get<ManagerUpdate>().add(this);

            InitStuff();
        }

        protected virtual void InitStuff()
        {
        }

        #endregion

        #region ADD / REMOVE

        private void Add<T>(T component)
        {
            AddComponent(component);
        }

        private void Add<T>() where T : new()
        {
            var newComponent = new T();

            AddComponent(newComponent);
        }

        private void AddComponent<T>(T component)
        {
            if (!(component is IComponent) && !(component is IBehaviour))
            {
                Debug.LogError("CANT ADD COMPONENT THIS IS NOT IDATA AND NOT BEHAVIOUR");

                return;
            }

            var hash = typeof(T).GetHashCode();

            if (_components.ContainsKey(hash))
            {
                return;
            }

            var start = component as IStart;
            if (start != null)
                _starts.Add(start);
            
            var beh = component as IBehaviour;
            if (beh != null)
                AddBehaviour(beh);

            _components.Add(hash, component);
        }
        
        private void AddBehaviour(IBehaviour behaviour)
        {
            if (_behaviours.Contains(behaviour))
            {
                Debug.LogError(this + " already contains behaviour " + behaviour);
                
                return;
            }
            
            _behaviours.Add(behaviour);

            var beh = behaviour;
            if (beh != null)
                beh.link(this);

            _managerUpdate.add(beh);
        }
        
        private void Remove<T>()
        {
            var hash = typeof(T).GetHashCode();
            
            if (!_components.ContainsKey(hash)) return;
            
            _components.Remove(hash);
        }

        #endregion

        #region EVENTS MANAGING

        private void Listen<T>(IListen listener)
        {
            _managerEvents.add<T>(listener);
        }

        private void Unlisten<T>(IListen listener)
        {
            _managerEvents.remove<T>(listener);
        }

        private void Send<T>(T arguments)
        {
            _managerEvents.send<T>(arguments);
        }

        #endregion

        #region GET

        private T Get<T>()
        {
            object obj;

            _components.TryGetValue(typeof(T).GetHashCode(), out obj);

            return (T) obj;
        }

        #endregion

        #region DESTROY

        private void Destroy()
        {
            Core.Instance.get<ManagerUpdate>().remove(this);

            _managerEvents.clear();
            _managerUpdate.clear();
            
            _components.Clear();
            _behaviours.Clear();
        }

        #endregion
    }
}