using System.Collections.Generic;
using Interfaces;
using UnityEngine;
namespace Object_Pooling
{
    public class ObjectPool<T>  where T : Component
    {
        private T prefab;
        private Transform parent;
        private Queue<T> objects =  new Queue<T>();


        public ObjectPool(T prefab, int initialSize, Transform parent = null)
        {
            this.prefab = prefab;
            this.parent = parent;

            for (int i = 0; i < initialSize; i++)
            {
                T obj =  Object.Instantiate(prefab, parent);
                obj.gameObject.SetActive(false);
                objects.Enqueue(obj);
            }
        }

        public T Get()
        {
            T obj;
            obj = objects.Count == 0 ? Object.Instantiate(prefab, parent) : objects.Dequeue();
            
            obj.gameObject.SetActive(true);
            if (obj is IPoolabe poolable)
            {
                poolable.Spawn();
            }

            return obj;
        }

        public void ReturnToPool(T obj)
        {
            if (obj is  IPoolabe poolable)
            {
                poolable.Despawn();
            }
            
            obj.gameObject.SetActive(false);
            objects.Enqueue(obj);
        }
    }
}