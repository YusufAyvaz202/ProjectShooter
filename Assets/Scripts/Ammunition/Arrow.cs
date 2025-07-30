using Abstracts;
using Interfaces;
using Misc;
using Object_Pooling;
using UnityEngine;
namespace Ammunition
{
    public class Arrow : BaseAmmunition
    {
        [Header("Settings")]
        [SerializeField] private float _moveSpeed;
        
        public void AttackToTarget()
        {
            _rigidbody.AddForce((transform.forward) * _moveSpeed, ForceMode.Impulse);
        }
        
        protected override void LifeTimer()
        {
            ReturnToPool();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<IAttackable>(out var attackable))
            {
                attackable.TakeDamage(damage);
                ReturnToPool();
            }
        }
        
        private void ReturnToPool()
        {
            Pools.Instance.GetPool<Arrow>(PoolType.Arrow).ReturnToPool(this);
        }
    }
}