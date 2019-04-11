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

        [SerializeField] private ScriptableObject[] Modules;
        
        protected Core Core;

        private void Awake()
        {
            Init();
        }

        private void Start()
        {
            OnStart();
        }

        protected virtual void Init()
        {
            InitCore();
            
            Core.initData();
            
            InitModules();
            
            Core.initSystem();
        }

        private void InitModules()
        {
            foreach (var module in Modules)
            {
                Core.Instance.add(module);
            }
        }

        protected abstract void InitCore();

        protected virtual void OnStart()
        {
            Core.onStart();
        }
    }
}