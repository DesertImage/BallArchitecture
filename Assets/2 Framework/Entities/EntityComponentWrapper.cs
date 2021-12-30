using Entities;
using Framework.Components;
using UnityEngine;

namespace Components
{
    [RequireComponent(typeof(EntityMono))]
    public class EntityComponentWrapper : ComponentWrapper
    {
        [SerializeField] private EntityMono entityMono;

        private void OnEnable()
        {
            // if (entityMono.AutoInitialize) return;
            //
            // if (!entityMono)
            // {
            //     entityMono = gameObject.AddComponent<EntityMono>();
            // }
            //
            // entityMono.OnCreate();
        }

        public override void Link(IComponentHolder componentHolder)
        {
        }

        protected virtual void OnValidate()
        {
            if (!entityMono)
            {
                entityMono = GetComponent<EntityMono>();
            }
        }

        protected virtual void OnDestroy()
        {
            entityMono = null;
        }
    }
}