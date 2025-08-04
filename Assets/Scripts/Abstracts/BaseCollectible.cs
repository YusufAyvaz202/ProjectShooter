using System;
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

        public void Collect(Action<CollectibleType> onCollect)
        {
            onCollect?.Invoke(_collectibleType);
        }

        // TODO: This function for test found a better way to handle the collectible despawn.
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerInteractionManager playerInteractionManager))
            {
                if (playerInteractionManager is null) return;
                CollectibleSpawnManager.Instance.DespawnCollectible(this, _collectibleType);
            }
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