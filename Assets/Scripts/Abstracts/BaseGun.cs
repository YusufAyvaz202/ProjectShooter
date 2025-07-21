using Interfaces;
using Managers;
using ScriptableObjects;
using UnityEngine;

namespace Abstracts
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class BaseGun : MonoBehaviour, IAttacker
    {
        [Header("References")]
        [SerializeField] private GunDataSO gunData;
        [SerializeField] protected Transform ammunitionSpawnTransform;
        
        [Header("Gun Settings")]
        protected GameObject ammunitionPrefab;
        private Rigidbody _rigidbody;
        protected int initialSize;
        
        [Header("Properties")]
        public  Rigidbody Rigidbody => _rigidbody;

        public abstract void Attack();
        
        private void OnGunThrow()
        {
            if (_rigidbody.isKinematic || transform.parent != null) return;
            
            _rigidbody.useGravity = true;
        }
        
        private void OnGunThrowPerformed()
        {
            Invoke(nameof(OnGunThrow), .75f);
        }

        #region Initialize & Cleanup

        private void OnEnable()
        {
            ammunitionPrefab = gunData.ammunitionPrefab;
            initialSize = gunData.initialSize;
            
            _rigidbody = GetComponent<Rigidbody>();
            EventManager.OnGunThrowPerformed += OnGunThrowPerformed;       
        }

        private void OnDisable()
        {
            EventManager.OnGunThrowPerformed -= OnGunThrowPerformed;
        }

        #endregion
    }
}