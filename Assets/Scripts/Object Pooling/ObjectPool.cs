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
            T obj = objects.Count == 0 ? Object.Instantiate(prefab, parent) : objects.Dequeue();
            obj.gameObject.SetActive(true);

            if (obj is IPoolabe poolable)
            {
                poolable.Spawn();
            }

            return obj;
        }

        // If object is done return that object to the pool.
        public void ReturnToPool(T obj)
        {
            if (obj is IPoolabe poolable)
            {
                poolable.Despawn();
            }

            obj.gameObject.SetActive(false);
            objects.Enqueue(obj);
        }
    }
}