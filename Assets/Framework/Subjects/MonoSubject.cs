using System.Collections.Generic;
using BallArchitectureApp.Behaviours.Interfaces;
using DesertImage.Managers;
using UnityEngine;

namespace DesertImage.Subjects
{
    public class MonoSubject : MonoBehaviour, ISubject, ITick, ITickFixed, ITickLate
    {
        #region PROPERTIES

        public Dictionary<int, object>.KeyCollection Components
        {
            get { return _components.Keys; }
        }

        #endregion

        #region PRIVATE

        private ManagerEvents _managerEvents = new ManagerEvents();

        private ManagerUpdate _managerUpdate = new ManagerUpdate();

        private readonly Dictionary<int, object> _components = new Dictionary<int, object>();
        private readonly List<IBehaviour> _behaviours = new List<IBehaviour>();

        private readonly List<IStart> _starts = new List<IStart>();

        #endregion

        #region MONO BEHVAIOUR METHODS

        private void Awake()
        {
            AwakeStuff();
        }

        private void Start()
        {
            this.AddToSubjects();
            
            StartStuff();
        }

        private void OnDestroy()
        {
            this.RemoveFromSubjects();
        }

        #endregion

        #region PUBLIC METHODS

        public void onStart()
        {
            StartStuff();

            foreach (var start in _starts)
            {
                start.onStart();
            }
        }

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
            onStart();
        }

        public void returnToPool()
        {
            Destroy();
        }

        public void tick()
        {
            _managerUpdate.tick();
        }

        public void fixedTick()
        {
            _managerUpdate.fixedTick();
        }

        public void lateTick()
        {
            _managerUpdate.lateTick();
        }

        #endregion

        #region INIT

        protected void Init()
        {
            Core.Instance.get<ManagerUpdate>().add(this);

            InitStuff();
        }

        protected virtual void AwakeStuff()
        {
        }

        protected virtual void StartStuff()
        {
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
                Debug.LogError("THIS IS NOT IDATA AND NOT BEHAVIOUR " + component);

                return;
            }

            var hash = typeof(T).GetHashCode();

            if (_components.ContainsKey(hash))
            {
                Debug.LogError(name + " already contains " + typeof(T));

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

            _managerUpdate.remove(_components[hash]);
            
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

            foreach (var behaviour in _behaviours)
            {
                behaviour.Dispose();
            }

            _behaviours.Clear();

            _components.Clear();
            _managerEvents.clear();

            _starts.Clear();

            _managerUpdate.clear();

            Core.Instance.get<FactorySpawn>().returnInstance(gameObject, name);
        }

        #endregion
    }
}