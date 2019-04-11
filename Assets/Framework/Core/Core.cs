using System.Collections.Generic;
using DesertImage.Managers;
using DesertImage.Subjects;

namespace DesertImage
{
    public class Core : IAwake, IStart
    {
        #region PROPERTIES

        public Core()
        {
            Instance = this;
        }

        public static Core Instance { get; private set; }

        #endregion

        #region PRIVATE

        private readonly Dictionary<int, object> _data = new Dictionary<int, object>();

        private List<IStart> _starts = new List<IStart>();

        #endregion

        #region PUBLIC METHODS

        public void onAwake()
        {
            Init();
        }

        public void onStart()
        {
            InitStarts();
        }

        public void initData()
        {
            InitData();
        }

        public void initSystem()
        {
            InitSystems();
        }

        public void add<T>(T instance)
        {
            Add(instance);
        }

        public void remove<T>()
        {
            Remove<T>();
        }

        public T get<T>()
        {
            return Get<T>();
        }

        #endregion

        #region INIT

        private void Init()
        {
            InitData();
            InitSystems();
        }

        protected virtual void InitData()
        {
        }

        protected virtual void InitSystems()
        {
            Add(new ManagerUpdate());
            Add(new ManagerEvents());
            Add(new ManagerSubjects());
            Add(new ManagerTimers());
        }

        private void InitStarts()
        {
            foreach (var start in _starts)
            {
                start.onStart();
            }
        }

        #endregion

        #region ADD

        protected void Add<T>(T newObj)
        {
            var hash = newObj.GetType().GetHashCode();

            if (_data.ContainsKey(hash)) return;

            _data.Add(hash, newObj);

            var awake = newObj as IAwake;
            if (awake != null)
            {
                awake.onAwake();
            }

            var start = newObj as IStart;
            if (start != null)
            {
                _starts.Add(start);
            }

            var tick = newObj as ITick;
            if (tick != null)
            {
                Get<ManagerUpdate>().add(tick);
            }
        }

        #endregion

        #region REMOVE

        private void Remove<T>()
        {
            var hash = typeof(T).GetHashCode();

            if (!_data.ContainsKey(hash)) return;

            _data.Remove(hash);
        }

        #endregion

        #region GET

        protected T Get<T>()
        {
            object obj = default(T);

            Instance._data.TryGetValue(typeof(T).GetHashCode(), out obj);

            return (T) obj;
        }

        #endregion
    }
}