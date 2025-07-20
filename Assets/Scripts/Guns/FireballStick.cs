using Abstracts;
using Ammunition;
using Misc;
using Object_Pooling;
using UnityEngine;
namespace Guns
{
    public class FireballStick : BaseGun
    {
        [Header("Settings")]
        private Transform _targetTransform;

        private void Start()
        {
            Initialize();
            _targetTransform = GetComponentInParent<BaseEnemy>().TargetTransform;
        }

        public override void Attack()
        {
            var fireball = Pools.Instance.GetPool<Fireball>(PoolType.Fireball).Get();
            fireball.transform.position = ammunitionSpawnTransform.position;
            fireball.transform.rotation = ammunitionSpawnTransform.rotation;

            // TODO:  Parameter is just the for testing it will change later.
            fireball.AttackToTarget(_targetTransform.position);
        }

        #region Initialize & Cleanup

        private void Initialize()
        {
            Pools.Instance.CreatePool(PoolType.Fireball, ammunitionPrefab.GetComponent<Fireball>(), initialSize);
        }

        #endregion
    }
}