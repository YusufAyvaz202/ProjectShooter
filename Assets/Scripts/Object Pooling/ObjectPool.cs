using System.Collections.Generic;
using Interfaces;
using UnityEngine;
namespace Object_Pooling
{
    public class ObjectPool<T> where T : Component
    {
        [Header("Object Pool Settings")]
        private readonly Queue<T> objects = new Queue<T>();

        private readonly Transform parent;
        private readonly T prefab;
        private int _capacityLimit = 30;

        // Initialize pool any type.
        public ObjectPool(T prefab, int initialSize, Transform parent = null)
        {
            this.prefab = prefab;
            this.parent = parent;

            for (int i = 0; i < initialSize; i++)
            {
                T obj = Object.Instantiate(prefab, parent);
                obj.gameObject.SetActive(false);
                objects.Enqueue(obj);
            }
        }

        // Get object from pool. if pool is empty create another Instantiate.
        public T Get()
        {
            T obj;
            
            if (objects.Count == 0)
            {
                if (_capacityLimit <= 0) return null;
                obj = Object.Instantiate(prefab, parent);
                _capacityLimit--;
            }
            else
            {
                obj = objects.Dequeue();
            }


            obj.gameObject.SetActive(true);

            if (obj is IPoolable poolable)
            {
                poolable.Spawn();
            }

            return obj;
        }

        // If object is done return that object to the pool.
        public void ReturnToPool(T obj)
        {
            if (obj is IPoolable poolable)
            {
                poolable.Despawn();
            }

            obj.gameObject.SetActive(false);
            objects.Enqueue(obj);
        }
    }
}