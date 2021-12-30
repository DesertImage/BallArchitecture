using System.Collections;
using System.Collections.Generic;

namespace DesertImage.Pools
{
    public abstract class Pool<T> : IPool<T> where T : IPoolable
    {
        protected readonly Stack<T> Stack = new Stack<T>();

        public void Register(int count)
        {
            for (var i = 0; i < count; i++)
            {
                ReturnInstance(CreateInstance());
            }
        }

        public IEnumerator AsyncRegister(int count)
        {
            for (var i = 0; i < count; i++)
            {
                ReturnInstance(CreateInstance());

                yield return null;
            }
        }

        public virtual T GetInstance()
        {
            var instance = Stack.Count > 0 ? Stack.Pop() : CreateInstance();

            instance.OnCreate();

            return instance;
        }

        public virtual void ReturnInstance(T instance)
        {
            //TODO: rework
            if (Stack.Contains(instance))
            {
#if DEBUG
                //UnityEngine.Debug.LogWarning($"[Pool] {this} already containt {instance}");
#endif
                return;
            }

            Stack.Push(instance);
        }

        protected abstract T CreateInstance();
    }
}