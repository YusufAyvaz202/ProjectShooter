using Abstracts;
namespace Guns
{
    public class FireballStick : BaseGun
    {
        public override void Attack()
        {
            // Temporarily solution for testing. In the future, this should be replaced with a proper object pooling system.
            Instantiate(ammunitionPrefab, ammunitionSpawnTransform.position, ammunitionSpawnTransform.rotation);
        }
    }
}