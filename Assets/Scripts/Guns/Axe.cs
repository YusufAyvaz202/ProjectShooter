using Abstracts;
using Interfaces;
using UnityEngine;
namespace Guns
{
    public class Axe : BaseGun
    {
        [Header("Axe Properties")]
        private bool _isAttacking;

        public override void Attack()
        {
            _isAttacking = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_isAttacking) return;
            if (other.gameObject.TryGetComponent(out IAttackable attackable))
            {
                attackable.TakeDamage(25);
                // TODO: can be added some visual or sound effects here
                Debug.Log($"Axe hit {other.name} for {25} damage.");
            }
            else
            {
                Debug.Log($"Axe hit {other.name}, but it is not attackable.");
            }
            _isAttacking = false;
        }
    }
}