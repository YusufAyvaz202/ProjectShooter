using System;
using Abstracts;
using Ammunition;
using Misc;
using Object_Pooling;
namespace Guns
{
    public class FireballStick : BaseGun
    {

        private void Start()
        {
            Initialize();
        }

        public override void Attack()
        {
            base.Attack();
            var bullet = Pools.Instance.GetPool<Fireball>(PoolType.MageFireball).Get();
            bullet.transform.position = ammunitionSpawnTransform.position;
            bullet.transform.rotation = ammunitionSpawnTransform.rotation;
        }

        #region Initialize & Cleanup

        private void Initialize()
        {
            Pools.Instance.CreatePool(PoolType.MageFireball, ammunitionPrefab.GetComponent<Fireball>(), initialSize);
        }

        #endregion
    }
}