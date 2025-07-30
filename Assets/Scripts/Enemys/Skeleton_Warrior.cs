using Abstracts;
using Guns;
using UnityEngine;
namespace Enemys
{
    public class Skeleton_Warrior : BaseEnemy
    {
        [Header("Warrior Properties")]
        [SerializeField] private Axe _axe;
        
        public override void Attack()
        {
            if (_navMeshAgent == null || _targetTransform == null) return;
            if (_attackCooldown <= 0f)
            {
                _axe.Attack();
                //_rigidbody.AddForce((transform.forward + transform.up) * 2, ForceMode.Impulse);
                // Reset the attack cooldown
                _attackCooldown = myEnemyData.attackCooldown;
            }
            else
            {
                // Decrease the cooldown timer
                _attackCooldown -= Time.fixedDeltaTime;
            }
        }
    }
}