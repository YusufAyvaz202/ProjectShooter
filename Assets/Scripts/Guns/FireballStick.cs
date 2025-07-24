using Abstracts;
using Ammunition;
using Misc;
using Object_Pooling;
using UnityEngine;
namespace Guns
{
    public class FireballStick : BaseGun
    {
        [Header("Settings")]
        private Transform _targetTransform;

        private void Start()
        {
            Initialize();
            _targetTransform = GetComponentInParent<BaseEnemy>()?.TargetTransform;
        }

        public override void Attack()
        {
            var fireball = Pools.Instance.GetPool<Fireball>(PoolType.Fireball).Get();
            
            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(new Vector3(Screen.width/2f, Screen.height/2f, 0));
            Vector3 targetPoint = ray.GetPoint(5f);
            
            fireball.transform.position = targetPoint;
            fireball.transform.rotation = UnityEngine.Camera.main.transform.rotation;

            fireball.AttackToTarget(Vector3.one);
        }

        #region Initialize & Cleanup

        private void Initialize()
        {
            Pools.Instance.CreatePool(PoolType.Fireball, ammunitionPrefab.GetComponent<Fireball>(), initialSize);
        }

        #endregion
    }
}