using System.Collections.Generic;
using Misc;
using UnityEngine;
namespace Object_Pooling
{
    public class Pools : MonoBehaviour
    {
        [Header("Singleton")]
        public static Pools Instance;
        
        [Header("Object Pools")]
        private readonly Dictionary<PoolType, object> _pools = new Dictionary<PoolType, object>();

        public void CreatePool<T>(PoolType poolType, T prefab, int initialSize) where T : MonoBehaviour
        {
            if (!_pools.ContainsKey(poolType))
            {
                var pool = new ObjectPool<T>(prefab, initialSize);
                _pools[poolType] = pool;
            }
            else
            {
                Debug.LogWarning($"Pool of type {poolType} already exists.");
            }
        }

        public ObjectPool<T> GetPool<T>(PoolType poolType) where T : MonoBehaviour
        {
            if (_pools.TryGetValue(poolType, out var pool))
            {
                return pool as ObjectPool<T>;
            }

            Debug.LogError($"Pool of type {poolType} does not exist.");
            return null;
        }

        #region Initialize & Cleanup

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #endregion
    }

}