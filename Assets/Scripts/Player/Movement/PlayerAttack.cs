using Abstracts;
using Managers;
using UnityEngine;
namespace Player.Movement
{
    public class PlayerAttack : MonoBehaviour
    {
        public BaseGun BaseGun;
        private void Awake()
        {
            EventManager.OnAttackPerformed += Attack;
        }
        private void Attack()
        {
            Debug.Log("Attack.");
            BaseGun.Attack();
        }

    }
}