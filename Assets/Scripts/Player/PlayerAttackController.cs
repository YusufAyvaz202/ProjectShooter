using Abstracts;
using Managers;
using UnityEngine;
namespace Player
{
    public class PlayerAttackController : MonoBehaviour
    {
        [Header("Attack References")]
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Transform _gunParentTransform;
        [SerializeField] private Transform _combatLookAtTransform;
        [SerializeField] private BaseGun _currentGun;
        
        [Header("Other References")]
        private PlayerAnimationController _playerAnimationController;

        private void Attack()
        {
            if (_currentGun is null) return;
            _currentGun.Attack();
            
            _playerAnimationController.PlayAttackAnimation(true);
        }

        private void ThrowGun()
        {
            if (_currentGun is null) return;
            
            _currentGun.ThrowedGun(_playerTransform);
            _currentGun = null;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (_currentGun != null) return;
            TakeGun(other);
        }
        private void TakeGun(Collision gun)
        {
            if (gun.gameObject.TryGetComponent(out BaseGun baseGun))
            {
                _currentGun = baseGun;
                _currentGun.TakedGun(_gunParentTransform);
                
                _currentGun.SetCombatLookAtTransform(_combatLookAtTransform);
            }
            
        }

        #region Initialize & Cleanup

        void OnEnable()
        {
            EventManager.OnAttackPerformed += Attack;
            EventManager.ThrowActionPerformed += ThrowGun;
            
            _playerAnimationController = GetComponentInChildren<PlayerAnimationController>();
        }

        private void OnDisable()
        {
            EventManager.OnAttackPerformed -= Attack;
            EventManager.ThrowActionPerformed -= ThrowGun;
        }

        #endregion

    }
}