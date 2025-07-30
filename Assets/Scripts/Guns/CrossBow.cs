using Abstracts;
using Ammunition;
using Misc;
using Object_Pooling;
using UnityEngine;
namespace Guns
{
    public class CrossBow : BaseGun
    {
        [Header("Settings")]
        private Vector3 targetPoint;

        [Header("References")]
        private UnityEngine.Camera _camera;

        private void Start()
        {
            Initialize();
        }

        public override void Attack()
        {
            var arrow = Pools.Instance.GetPool<Arrow>(PoolType.Arrow).Get();

            Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            targetPoint = Physics.Raycast(ray, out RaycastHit hit) ? hit.point : ray.GetPoint(100);

            Vector3 direction = (targetPoint - _combatLookAtTransform.position).normalized;

            arrow.transform.position = _combatLookAtTransform.position;
            arrow.transform.rotation = Quaternion.LookRotation(direction);
            arrow.AttackToTarget();
        }

        #region Initialize & Cleanup

        private void Initialize()
        {
            Pools.Instance.CreatePool(PoolType.Arrow, ammunitionPrefab.GetComponent<Arrow>(), initialSize);
            
            _camera = UnityEngine.Camera.main;
        }

        #endregion
    }
}