using Abstracts;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;
namespace Collectibles
{
    public class HealCollectible : BaseCollectible
    {
        [Header("Settings")]
        [SerializeField] private int _healAmount;
        public override void Collect(PlayerInteractionManager playerInteractionManager)
        {
            playerInteractionManager.MakeHealthPickup(_healAmount);
            base.Collect(playerInteractionManager);
        }

        #region Initialize & Cleanup

        private void OnEnable()
        {
            _healAmount = Random.Range(5, 20);
        }

        #endregion
    }
}