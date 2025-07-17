using System.Collections;
using Interfaces;
using ScriptableObjects;
using UnityEngine;
namespace Abstracts
{
    [RequireComponent(typeof(Rigidbody))]
    public class BaseAmmunition : MonoBehaviour, IPoolable
    {
        [Header("Bullet Settings")]
        [SerializeField] private AmmunitionDataSO ammunitionData;
        protected float speed;
        protected float damage;
        protected float lifeTime;
        protected Rigidbody _rigidbody;
        protected Coroutine _lifeTimerCoroutine;
        
        [Header("Properties")]
        public float Damage
        {
            set => damage = value;
        }

        protected virtual IEnumerator LifeTimer()
        {
            yield return new WaitForSeconds(lifeTime);
        }
        
        public void Spawn()
        {
            _lifeTimerCoroutine = StartCoroutine(LifeTimer());
        }
        public void Despawn()
        {
            if (_lifeTimerCoroutine is not null)
            {
                StopCoroutine(_lifeTimerCoroutine);
            }
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