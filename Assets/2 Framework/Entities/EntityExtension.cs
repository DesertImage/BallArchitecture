using DesertImage;
using DesertImage.Entities;
using Entities;
using UnityEngine;

namespace Framework.Entities
{
    public abstract class EntityExtension : MonoBehaviour, IEntityExtension
    {
        [SerializeField] protected EntityMono monoEntity;

        protected IEntity Entity;

        public virtual void Link(IEntity entity)
        {
            Entity = entity;
        }

        protected virtual void OnValidate()
        {
            if (!monoEntity)
            {
                monoEntity = transform.GetComponent<EntityMono>();
            }
        }

        private void OnDestroy()
        {
            monoEntity = null;
        }
    }
}