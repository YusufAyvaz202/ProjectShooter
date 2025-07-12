using Bullets;
using Interfaces;
using Managers;
using Misc;
using Object_Pooling;
using UnityEngine;

namespace Abstracts
{
    public abstract class BaseGun : MonoBehaviour, IAttackType
    {
        [Header("Gun Properties")]
        [SerializeField] private Transform bulletSpawnTransform;
        [SerializeField] private Bullet bulletPrefab;
        private ObjectPool<Bullet> _objectPool;
        protected GunType gunType;

        public virtual void Attack()
        {
            var bullet = _objectPool.Get();
            bullet.transform.position = bulletSpawnTransform.position;
            bullet.transform.rotation = bulletSpawnTransform.rotation;
        }
        
        private void OnDeSpawn(Bullet obj)
        {
            _objectPool.ReturnToPool(obj);
        }

        public void ThrowGun()
        {
            // implement throwing a car.
        }

        #region Initlalize & CleanUp

        private void OnEnable()
        {
            _objectPool = new ObjectPool<Bullet>(bulletPrefab, 10);
            EventManager.OnDeSpawn += OnDeSpawn;
        }

        private void OnDisable()
        {
            EventManager.OnDeSpawn -= OnDeSpawn;
        }

        #endregion
    }
}