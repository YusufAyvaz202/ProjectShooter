using Abstracts;
using Interfaces;
using Misc;
using Object_Pooling;
using UnityEngine;
namespace Ammunition
{
    public class Bullet : BaseAmmunition
    {
        private void FixedUpdate()
        {
            Vector3 newPosition = transform.position + transform.forward * (speed * Time.fixedDeltaTime);
            _rigidbody.MovePosition(newPosition);
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
            Pools.Instance.GetPool<Bullet>(PoolType.Bullet).ReturnToPool(this);
        }

    }
}