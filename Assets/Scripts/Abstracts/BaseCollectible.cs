using Interfaces;
using Managers;
using Misc;
using Player;
using UnityEngine;
namespace Abstracts
{
    public abstract class BaseCollectible : MonoBehaviour, ICollectible, IPoolable
    {
        [Header("Collectible Settings")]
        [SerializeField] private CollectibleType _collectibleType;

        public virtual void Collect(PlayerInteractionManager playerInteractionManager)
        {
            CollectibleSpawnManager.Instance.DespawnCollectible(this, _collectibleType);
        }
        public void Spawn()
        {
            // TODO: Make Particle System for collectible spawn.
        }
        public void Despawn()
        {
            // TODO: Make Particle System for collectible Despawn.
        }
    }
}