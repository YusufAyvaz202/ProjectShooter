using Abstracts;
using Managers;
using UnityEngine;
namespace Player
{
    public class PlayerAttackController : MonoBehaviour
    {
        [Header("Attack Settings")]
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private BaseGun _currentGun;

        private void Attack()
        {
            _currentGun.Attack();
        }

        private void ThrowGun()
        {
            if (_currentGun is null) return;

            //TODO: Move this function to BaseGun class
            _currentGun.transform.parent = null;
            _currentGun.transform.rotation = _playerTransform.rotation;
            _currentGun.Rigidbody.isKinematic = false;
            _currentGun.Rigidbody.AddForce(5f * (_playerTransform.forward + transform.up), ForceMode.Impulse);
            _currentGun = null;

            EventManager.OnGunThrowPerformed?.Invoke();
        }

        #region Initialize & Cleanup

        void OnEnable()
        {
            EventManager.OnAttackPerformed += Attack;
            EventManager.ThrowActionPerformed += ThrowGun;
        }

        private void OnDisable()
        {
            EventManager.OnAttackPerformed -= Attack;
            EventManager.ThrowActionPerformed -= ThrowGun;
        }

        #endregion

    }
}