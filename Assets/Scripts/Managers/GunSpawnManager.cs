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
    public class GunSpawnManager : MonoBehaviour
    {
        [Header("Gun Pool Settings")]
        [SerializeField] private List<GunPoolSO> poolDatas;
        private Dictionary<GunType, ObjectPool<BaseGun>> _gunPools;

        [Header("Spawn Settings")]
        private readonly float spawnInterval = 60f;
        private readonly bool isSpawning = true;
        private Coroutine _spawnCoroutine;

        private void Start()
        {
            _gunPools = new Dictionary<GunType, ObjectPool<BaseGun>>();
            CreatePools();
            _spawnCoroutine = StartCoroutine(nameof(SpawnGunsContinuously));
        }

        private void SpawnGun()
        {
            GunType randomGunType = GetRandomGunType();
            _gunPools.TryGetValue(randomGunType, out var pool);

            if (pool == null) return;
            BaseGun baseGun = pool.Get();

            Vector3 spawnPosition = Random.insideUnitSphere * 50f;
            spawnPosition.y = transform.position.y;

            baseGun.transform.position = spawnPosition;
            baseGun.ThrowedGun(null);
        }

        // TODO: Call this method when appropriate, but if plater picks up the gun, it should not be despawned
        private void DespawnGun(BaseGun gun, GunType gunType)
        {
            if (_gunPools.TryGetValue(gunType, out var pool))
            {
                pool.ReturnToPool(gun);
            }
            else
            {
                Debug.LogError($"No pool found for gun type {gunType}");
            }
        }

        private IEnumerator SpawnGunsContinuously()
        {
            while (isSpawning)
            {
                SpawnGun();
                yield return new WaitForSeconds(spawnInterval);
            }
        }
        private GunType GetRandomGunType()
        {
            int randomIndex = Random.Range(0, poolDatas.Count);
            return poolDatas[randomIndex].gunType;
        }

        #region Initialize & Cleanup

        private void CreatePools()
        {
            foreach (var poolData in poolDatas)
            {
                var objectPool = new ObjectPool<BaseGun>(poolData.baseGunPrefab, poolData.initialSize);
                _gunPools.Add(poolData.gunType, objectPool);
            }
        }

        private void OnDisable()
        {
            StopCoroutine(_spawnCoroutine);
        }

        #endregion
    }
}