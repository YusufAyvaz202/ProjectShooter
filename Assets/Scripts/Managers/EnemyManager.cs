using System.Collections;
using System.Collections.Generic;
using Abstracts;
using Misc;
using Object_Pooling;
using Player;
using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;
namespace Managers
{
    public class EnemyManager : MonoBehaviour
    {
        [Header("Enemy Pool Settings")]
        [SerializeField] private List<EnemyPoolSO> enemyPoolDatas = new List<EnemyPoolSO>();
        private Dictionary<EnemyType, ObjectPool<BaseEnemy>> enemyPools;

        [Header("Spawn Settings")]
        [SerializeField] private float spawnRadius = 50f;
        private readonly float spawnInterval = 1f;
        private readonly bool isSpawning = true;
        private Transform _targetTransform;

        private void SpawnEnemy(EnemyType type)
        {
            Vector2 spawnPosition = Random.insideUnitCircle * spawnRadius;
            if (!enemyPools.TryGetValue(type, out var pool))
            {
                Debug.LogError($"No pool found for enemy type {type}");
            }

            if (pool != null)
            {
                var enemy = pool.Get();
                enemy.transform.position = new Vector3(spawnPosition.x, 0, spawnPosition.y);
                enemy.SetTargetTransform(_targetTransform);
            }
        }

        private IEnumerator SpawnEnemiesContinuously()
        {
            while (isSpawning)
            {
                EnemyType randomType = GetRandomEnemyType();
                SpawnEnemy(randomType);
                yield return new WaitForSeconds(spawnInterval);
            }
        }
        private EnemyType GetRandomEnemyType()
        {
            int randomIndex = Random.Range(0, enemyPoolDatas.Count);
            return enemyPoolDatas[randomIndex].enemyType;
        }

        private void DespawnEnemy(BaseEnemy enemy)
        {
            if (enemyPools.TryGetValue(enemy.enemyType, out var pool))
            {
                pool.ReturnToPool(enemy);
            }
            else
            {
                Debug.LogError($"No pool found for enemy type {enemy.enemyType}");
            }
        }

        #region Initalize & Cleanup

        private void OnEnable()
        {
            EventManager.OnEnemyDie += DespawnEnemy;
            
            CreateEnemyPools();
            _targetTransform = FindAnyObjectByType<PlayerMovementController>()?.GetComponent<Transform>();
            StartCoroutine(SpawnEnemiesContinuously());
        }
        private void CreateEnemyPools()
        {
            enemyPools = new Dictionary<EnemyType, ObjectPool<BaseEnemy>>();
            foreach (var enemyPoolSo in enemyPoolDatas)
            {
                ObjectPool<BaseEnemy> objectPool = new ObjectPool<BaseEnemy>(enemyPoolSo.baseEnemyPrefab, enemyPoolSo.initialSize);
                enemyPools.Add(enemyPoolSo.enemyType, objectPool);
            }
        }

        private void OnDisable()
        {
            EventManager.OnEnemyDie -= DespawnEnemy;
            StopAllCoroutines();
        }

        #endregion

    }
}