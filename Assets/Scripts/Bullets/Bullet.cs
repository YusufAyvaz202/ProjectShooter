using System.Collections;
using Interfaces;
using Managers;
using UnityEngine;
namespace Bullets
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour, IPoolabe
    {
        [SerializeField] private float bulletSpeed;
        private Rigidbody _rigidbody;

        private void OnEnable()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            _rigidbody.MovePosition(transform.position + transform.forward * (bulletSpeed * Time.fixedDeltaTime));
        }

        IEnumerator LifeTimer()
        {
            yield return new WaitForSeconds(3f);
            EventManager.OnDeSpawn(this);
        } 
        
        public void Spawn()
        {
            StartCoroutine(LifeTimer());
        }
        public void Despawn()
        {
            
        }
    }
}