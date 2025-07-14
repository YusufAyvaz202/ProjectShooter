using System.Collections;
using System.Collections.Generic;
using Abstracts;
using Misc;
using Object_Pooling;
using ScriptableObjects;
using UnityEngine;
namespace Managers
{
    public class EnemyManager : MonoBehaviour
    {
        [Header("Enemy Pool Settings")]
        [SerializeField] private List<EnemyPoolSO> enemyPoolDatas = new List<EnemyPoolSO>();
        private Dictionary<EnemyType, ObjectPool<BaseEnemy>> enemyPools;

        [Header("Spawn Settings")]
        private readonly float spawnInterval = 1f;
        private readonly bool isSpawning = true;

        private void SpawnEnemy(EnemyType type)
        {
            Vector2 spawnPosition = Random.insideUnitCircle * 25f;
            if (!enemyPools.TryGetValue(type, out var pool))
            {
                Debug.LogError($"No pool found for enemy type {type}");
            }

            if (pool != null)
            {
                var enemy = pool.Get();
                enemy.transform.position = new Vector3(spawnPosition.x, 0, spawnPosition.y);
            }
        }

        private IEnumerator SpawnEnemiesContinuously()
        {
            while (isSpawning)
            {
                EnemyType randomType = EnemyType.Skeleton_Mage;
                SpawnEnemy(randomType);
                yield return new WaitForSeconds(spawnInterval);
            }
        }

        #region Initalize & Cleanup

        private void OnEnable()
        {
            CreateEnemyPools();
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

        #endregion

    }
}