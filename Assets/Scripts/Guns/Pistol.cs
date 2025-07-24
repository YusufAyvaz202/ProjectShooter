using Abstracts;
using Ammunition;
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
            var bullet = Pools.Instance.GetPool<Bullet>(PoolType.Bullet).Get();
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
        }

        #region Initialize & Cleanup

        private void Initialize()
        {
            Pools.Instance.CreatePool(PoolType.Bullet, ammunitionPrefab.GetComponent<Bullet>(), initialSize);
        }

        #endregion
    }
}