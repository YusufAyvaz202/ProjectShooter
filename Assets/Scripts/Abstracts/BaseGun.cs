using Interfaces;
using ScriptableObjects;
using UnityEngine;

namespace Abstracts
{
    public abstract class BaseGun : MonoBehaviour, IAttacker
    {
        [Header("Gun Properties")]
        [SerializeField] private GunDataSO gunData;
        [SerializeField] protected Transform ammunitionSpawnTransform;
        protected GameObject ammunitionPrefab;
        protected int initialSize;
        //protected GunType gunType;

        public virtual void Attack()
        {

        }

        #region Initlalize & CleanUp

        private void OnEnable()
        {
            ammunitionPrefab = gunData.ammunitionPrefab;
            initialSize = gunData.initialSize;
        }

        #endregion
    }
}