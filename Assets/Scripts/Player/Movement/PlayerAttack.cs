using System;
using Abstracts;
using Managers;
using UnityEngine;
namespace Player.Movement
{
    public class PlayerAttack : MonoBehaviour
    {
        [Header("Attack Settings")]
        public BaseGun BaseGun;

        private void Attack()
        {
            Debug.Log("Attack.");
            BaseGun.Attack();
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