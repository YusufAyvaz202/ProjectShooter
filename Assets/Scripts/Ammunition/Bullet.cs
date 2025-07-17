using System.Collections;
using Abstracts;
using Misc;
using Object_Pooling;
using Player;
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

        protected override IEnumerator LifeTimer()
        {
            yield return new WaitForSeconds(lifeTime);
            Pools.Instance.GetPool<Bullet>(PoolType.Bullet).ReturnToPool(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<BaseEnemy>().TakeDamage(damage);
                Pools.Instance.GetPool<Bullet>(PoolType.Bullet).ReturnToPool(this);
            }
            else if (other.CompareTag("Player"))
            {
                other.GetComponent<PlayerHealthController>().TakeDamage(damage);
                Pools.Instance.GetPool<Bullet>(PoolType.Bullet).ReturnToPool(this);
            }
        }

    }
}