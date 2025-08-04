using Interfaces;
using ScriptableObjects;
using UnityEngine;
namespace Abstracts
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class BaseAmmunition : MonoBehaviour, IPoolable
    {
        [Header("Bullet Settings")]
        [SerializeField] private AmmunitionDataSO ammunitionData;

        protected float damage;
        protected float lifeTime;
        protected Rigidbody _rigidbody;

        protected abstract void LifeTimer();

        public void Spawn()
        {
            //For ammunition bug.
            _rigidbody.linearVelocity = Vector3.zero;
            
            Invoke(nameof(LifeTimer), lifeTime);
        }
        public void Despawn()
        {
            CancelInvoke(nameof(LifeTimer));
        }

        #region Initialize & Cleanup

        private void OnEnable()
        {
            damage = ammunitionData.damage;
            lifeTime = ammunitionData.lifeTime;

            _rigidbody = GetComponent<Rigidbody>();
        }

        #endregion

    }
}