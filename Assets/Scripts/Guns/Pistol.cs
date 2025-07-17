using Abstracts;
using Bullets;
using Misc;
using Object_Pooling;
namespace Guns
{
    public class Pistol : BaseGun
    {
        private void Start()
        {
            Initialize();
        }
        
        public override void Attack()
        {
            base.Attack();
            var bullet = Pools.Instance.GetPool<Bullet>(PoolType.Bullet).Get();
            bullet.transform.position = ammunitionSpawnTransform.position;
            bullet.transform.rotation = ammunitionSpawnTransform.rotation;
        }

        #region Initlalize & CleanUp

        private void Initialize()
        {
            Pools.Instance.CreatePool(PoolType.Bullet, ammunitionPrefab.GetComponent<Bullet>(), initialSize);
        }

        #endregion
    }
}