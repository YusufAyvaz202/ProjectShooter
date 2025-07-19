using Abstracts;
using DG.Tweening;
using Interfaces;
using Misc;
using Object_Pooling;
using UnityEngine;
namespace Ammunition
{
    public class Fireball : BaseAmmunition
    {
        public void AttackToTarget(Vector3 targetPosition)
        {
            transform.DOMove(targetPosition, 2f).SetEase(Ease.InOutCirc);
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
            Pools.Instance.GetPool<Fireball>(PoolType.Fireball).ReturnToPool(this);
        }
    }
}