using System.Collections;
using Abstracts;
using Interfaces;
using Misc;
using Object_Pooling;
using Player;
using UnityEngine;
namespace Bullets
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour, IPoolable
    {
        [Header("Bullet Settings")]
        [SerializeField] private float bulletSpeed;
        [SerializeField] private float bulletDamage;
        [SerializeField] private float bulletLifeTime = 3f;
        private Rigidbody _rigidbody;
        private Coroutine _lifeTimerCoroutine;

        [Header("Properties")]
        public float BulletDamage
        {
            set => bulletDamage = value;
        }


        private void FixedUpdate()
        {
            Vector3 newPosition = transform.position + transform.forward * (bulletSpeed * Time.fixedDeltaTime);
            _rigidbody.MovePosition(newPosition);
        }

        IEnumerator LifeTimer()
        {
            yield return new WaitForSeconds(bulletLifeTime);
            Pools.Instance.GetPool<Bullet>(PoolType.Bullet).ReturnToPool(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<BaseEnemy>().TakeDamage(bulletDamage);
                Pools.Instance.GetPool<Bullet>(PoolType.Bullet).ReturnToPool(this);
            }
            else if (other.CompareTag("Player"))
            {
                other.GetComponent<PlayerHealthController>().TakeDamage(bulletDamage);
                Pools.Instance.GetPool<Bullet>(PoolType.Bullet).ReturnToPool(this);
            }
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
            _rigidbody = GetComponent<Rigidbody>();
        }

        #endregion
    }
}