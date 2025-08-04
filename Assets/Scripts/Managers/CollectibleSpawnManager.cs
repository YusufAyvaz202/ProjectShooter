using System.Collections;
using System.Collections.Generic;
using Abstracts;
using Misc;
using Object_Pooling;
using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;
namespace Managers
{
    public class CollectibleSpawnManager : MonoBehaviour
    {
        [Header("Singleton")]
        public static CollectibleSpawnManager Instance;
        
        [Header("Gun Pool Settings")]
        [SerializeField] private List<CollectiblePoolSO> poolDatas;
        private Dictionary<CollectibleType, ObjectPool<BaseCollectible>> _collectiblePools;

        [Header("Spawn Settings")]
        private readonly float spawnInterval = 3f;
        private readonly bool isSpawning = true;
        private readonly float _spawnRadius = 50f;
        private Coroutine _spawnCoroutine;

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

        private void Start()
        {
            _collectiblePools = new Dictionary<CollectibleType, ObjectPool<BaseCollectible>>();
            CreatePools();
            _spawnCoroutine = StartCoroutine(nameof(SpawnCollectibleContinuously));
        }

        private void SpawnCollectible()
        {
            CollectibleType randomCollectibleType = GetRandomCollectibleType();
            _collectiblePools.TryGetValue(randomCollectibleType, out var pool);

            if (pool == null) return;
            BaseCollectible baseCollectible = pool.Get();

            Vector3 spawnPosition = Random.insideUnitSphere * _spawnRadius;
            spawnPosition.y = transform.position.y;

            baseCollectible.transform.position = spawnPosition;
        }

        public void DespawnCollectible(BaseCollectible baseCollectible, CollectibleType collectibleType)
        {
            if (_collectiblePools.TryGetValue(collectibleType, out var pool))
            {
                pool.ReturnToPool(baseCollectible);
            }
            else
            {
                Debug.LogError($"No pool found for collectible type {collectibleType}");
            }
        }

        private IEnumerator SpawnCollectibleContinuously()
        {
            while (isSpawning)
            {
                SpawnCollectible();
                yield return new WaitForSeconds(spawnInterval);
            }
        }
        private CollectibleType GetRandomCollectibleType()
        {
            int randomIndex = Random.Range(0, poolDatas.Count);
            return poolDatas[randomIndex].collectibleType;
        }

        #region Initialize & Cleanup

        private void CreatePools()
        {
            foreach (var poolData in poolDatas)
            {
                var objectPool = new ObjectPool<BaseCollectible>(poolData.baseCollectiblePrefab, poolData.initialSize);
                _collectiblePools.Add(poolData.collectibleType, objectPool);
            }
        }

        private void OnDisable()
        {
            StopCoroutine(_spawnCoroutine);
        }

        #endregion
    }
}