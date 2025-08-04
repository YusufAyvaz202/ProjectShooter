using Interfaces;
using ScriptableObjects;
using UnityEngine;

namespace Abstracts
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class BaseGun : MonoBehaviour, IAttacker
    {
        [Header("References")]
        [SerializeField] private GunDataSO gunData;
        [SerializeField] protected Transform _combatLookAtTransform;

        [Header("Gun Settings")]
        protected GameObject ammunitionPrefab;
        private Rigidbody _rigidbody;
        private Collider _collider;
        protected int initialSize;
        protected readonly float _maxDistance = 100f;
        private bool _isTaken;
        
        [Header("Properties")]
        public bool IsTaken => _isTaken;

        public abstract void Attack();

        public void ThrowedGun(Transform _playerTransform,float ThrowForce = 5f)
        {
            transform.SetParent(null);
            _rigidbody.isKinematic = false;
            _rigidbody.useGravity = true;
            _collider.enabled = true;
            _isTaken = false;
            
            if (_playerTransform != null)
            {
                transform.rotation = _playerTransform.rotation;
                _rigidbody.AddForce(ThrowForce * (_playerTransform.forward + transform.up), ForceMode.Impulse);
            }
        }

        public void TakedGun(Transform _gunParentTransform)
        {
            _rigidbody.isKinematic = true;
            _rigidbody.useGravity = false;
            transform.SetParent(_gunParentTransform);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            _isTaken = true;

            _collider.enabled = false;
        }

        public void SetCombatLookAtTransform(Transform combatLookAtTransform)
        {
            _combatLookAtTransform = combatLookAtTransform;
        }

        #region Initialize & Cleanup

        private void OnEnable()
        {
            ammunitionPrefab = gunData.ammunitionPrefab;
            initialSize = gunData.initialSize;

            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
        }

        #endregion
    }
}