using Abstracts;
namespace Guns
{
    public class Pistol : BaseGun
    {
        public override void Attack()
        {
            base.Attack();
            //Instantiate(bulletPrefab, bulletSpawnTransform.position, bulletSpawnTransform.rotation);
        }
    }
}