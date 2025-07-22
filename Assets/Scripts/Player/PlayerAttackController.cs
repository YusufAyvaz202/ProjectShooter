using Abstracts;
using Managers;
using UnityEngine;
namespace Player
{
    public class PlayerAttackController : MonoBehaviour
    {
        [Header("Attack Settings")]
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Transform _gunParentTransform;
        [SerializeField] private BaseGun _currentGun;

        private void Attack()
        {
            if (_currentGun is null) return;
            _currentGun.Attack();
        }

        private void ThrowGun()
        {
            if (_currentGun is null) return;

            //TODO: Move this function to BaseGun class
            _currentGun.transform.SetParent(null);
            _currentGun.transform.rotation = _playerTransform.rotation;
            _currentGun.Rigidbody.isKinematic = false;
            _currentGun.Rigidbody.AddForce(5f * (_playerTransform.forward + transform.up), ForceMode.Impulse);
            _currentGun = null;

            EventManager.OnGunThrowPerformed?.Invoke();
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
                _currentGun.Rigidbody.isKinematic = true;
                _currentGun.Rigidbody.useGravity = false;
                _currentGun.transform.SetParent(_gunParentTransform);
                _currentGun.transform.localPosition = Vector3.zero; 
                _currentGun.transform.localRotation = Quaternion.identity; 
            }
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