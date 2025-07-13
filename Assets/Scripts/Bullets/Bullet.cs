using System.Collections;
using Interfaces;
using Managers;
using UnityEngine;
namespace Bullets
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour, IPoolable
    {
        [Header("Bullet Settings")]
        [SerializeField] private float bulletSpeed;
        [SerializeField] private float bulletLifeTime = 3f;
        private Rigidbody _rigidbody;
        private Coroutine _lifeTimerCoroutine;

        private void FixedUpdate()
        {
            Vector3 newPosition = transform.position + transform.forward * (bulletSpeed * Time.fixedDeltaTime);
            _rigidbody.MovePosition(newPosition);
        }

        IEnumerator LifeTimer()
        {
            yield return new WaitForSeconds(bulletLifeTime);
            EventManager.OnDeSpawn(this);
        }

        public void Spawn()
        {
            _lifeTimerCoroutine = StartCoroutine(LifeTimer());
        }
        public void Despawn()
        {
            StopCoroutine(_lifeTimerCoroutine);
        }

        #region Initialize & Cleanup

        private void OnEnable()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        #endregion
    }
}