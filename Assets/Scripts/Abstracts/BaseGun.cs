using Bullets;
using Interfaces;
using Managers;
using Misc;
using Object_Pooling;
using UnityEngine;

namespace Abstracts
{
    public abstract class BaseGun : MonoBehaviour, IAttacker
    {
        [Header("Gun Properties")]
        [SerializeField] private Transform bulletSpawnTransform;
        [SerializeField] private Bullet bulletPrefab;
        protected GunType gunType;

        public virtual void Attack()
        {
            var bullet = Pools.Instance.GetPool<Bullet>(PoolType.Bullet).Get();
            bullet.transform.position = bulletSpawnTransform.position;
            bullet.transform.rotation = bulletSpawnTransform.rotation;
        }
        
        private void OnDeSpawn(Bullet obj)
        {
            Pools.Instance.GetPool<Bullet>(PoolType.Bullet).ReturnToPool(obj);
        }

        public void ThrowGun()
        {
            // implement throwing a gun.
        }

        #region Initlalize & CleanUp

        private void OnEnable()
        {
            Pools.Instance.CreatePool(PoolType.Bullet, bulletPrefab, 10);
            EventManager.OnDeSpawn += OnDeSpawn;
        }

        private void OnDisable()
        {
            EventManager.OnDeSpawn -= OnDeSpawn;
        }

        #endregion
    }
}