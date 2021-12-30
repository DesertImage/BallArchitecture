using DesertImage.Entities;

namespace DesertImage.Behaviours
{
    public class Behaviour : IBehaviour
    {
        protected IEntity Entity;

        /// <summary>
        /// Link to subject to cache parentSubject and his components
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Link(IEntity entity)
        {
            Entity = entity;
        }

        public virtual void Activate()
        {
        }

        public virtual void Deactivate()
        {
        }

        public virtual void Dispose()
        {
            Deactivate();

            Entity = null;
        }
    }
}