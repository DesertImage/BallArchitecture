using System;
using UnityEngine;

namespace DesertImage.Entities
{
    public class MonoBehaviourPoolable : MonoBehaviour, IPoolable
    {
        public virtual void OnCreate()
        {
        }

        public virtual void ReturnToPool()
        {
            try
            {
                Core.Instance?.Get<FactorySpawn>()?.ReturnInstance(gameObject);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}