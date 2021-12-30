using System;
using DesertImage;
using DesertImage.Extensions;

namespace Components
{
    [Serializable]
    public class DataComponent<T> : IDataComponent, IDisposable, IPoolable where T : DataComponent<T>
    {
        public virtual int Id { get; }
        
        public void Dispose()
        {
            ReturnToPool();
        }

        public virtual void OnCreate()
        {
        }

        public virtual void ReturnToPool()
        {
            ComponentsTool.ReturnToPool(this as T);
        }
    }
}