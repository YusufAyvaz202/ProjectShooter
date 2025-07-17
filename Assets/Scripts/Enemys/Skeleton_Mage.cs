using Abstracts;
using Guns;
using UnityEngine;
namespace Enemys
{
    public class Skeleton_Mage : BaseEnemy
    {
        [Header("Mage Properties")]
        [SerializeField] private FireballStick _fireballStick;
        
        #region Initialize & Cleanup

        public override void Attack()
        {
            base.Attack();
            if (_navMeshAgent == null || _targetTransform == null) return;
            if (_attackCooldown <= 0f)
            {
                _fireballStick.Attack();

                // Reset the attack cooldown
                _attackCooldown = myEnemyData.attackCooldown;
            }
            else
            {
                // Decrease the cooldown timer
                _attackCooldown -= Time.deltaTime;
            }
        }

        #endregion
    }
}