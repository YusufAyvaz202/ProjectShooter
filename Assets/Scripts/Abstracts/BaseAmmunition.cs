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
        protected float speed;
        protected float damage;
        protected float lifeTime;
        protected Rigidbody _rigidbody;

        protected abstract void LifeTimer();
        
        public void Spawn()
        {
            Invoke(nameof(LifeTimer), lifeTime);
        }
        public void Despawn()
        {
            
        }

        #region Initialize & Cleanup

        private void OnEnable()
        {
            speed = ammunitionData.speed;
            damage = ammunitionData.damage;
            lifeTime = ammunitionData.lifeTime;
            
            _rigidbody = GetComponent<Rigidbody>();
        }

        #endregion
        
    }
}