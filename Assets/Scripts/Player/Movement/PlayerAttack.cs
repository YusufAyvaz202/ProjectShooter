using Abstracts;
using Managers;
using UnityEngine;
namespace Player.Movement
{
    public class PlayerAttack : MonoBehaviour
    {
        [Header("Attack Settings")]
        public BaseGun _currentGun;

        private void Attack()
        {
            _currentGun.Attack();
        }

        #region Initialize & Cleanup

        void OnEnable()
        {
            EventManager.OnAttackPerformed += Attack;
        }

        private void OnDisable()
        {
            EventManager.OnAttackPerformed -= Attack;
        }

        #endregion

    }
}