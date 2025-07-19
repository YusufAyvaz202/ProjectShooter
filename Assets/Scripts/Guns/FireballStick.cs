using Abstracts;
using Ammunition;
using Misc;
using Object_Pooling;
using Player;
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
            var fireball = Pools.Instance.GetPool<Fireball>(PoolType.Fireball).Get();
            fireball.transform.position = ammunitionSpawnTransform.position;
            fireball.transform.rotation = ammunitionSpawnTransform.rotation;
            
            // TODO:  Parameter is just the for testing it will change later.
            fireball.AttackToTarget(FindAnyObjectByType<PlayerMovementController>().transform.position);
        }

        #region Initialize & Cleanup

        private void Initialize()
        {
            Pools.Instance.CreatePool(PoolType.Fireball, ammunitionPrefab.GetComponent<Fireball>(), initialSize);
        }

        #endregion
    }
}