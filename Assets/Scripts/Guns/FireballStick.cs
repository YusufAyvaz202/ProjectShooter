using Abstracts;
using UnityEngine;
namespace Guns
{
    public class FireballStick : BaseGun
    {
        public override void Attack()
        {
            // Implement the attack logic for the FireballStick here.
            // This could involve instantiating a fireball projectile, playing an animation, etc.
            Debug.Log("FireballStick attacks with a fireball!");
        }
    }
}