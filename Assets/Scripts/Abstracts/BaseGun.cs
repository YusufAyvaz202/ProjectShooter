using Interfaces;
using Misc;
using UnityEngine;

namespace Abstracts
{
    public abstract class BaseGun : MonoBehaviour, AttackType
    {
        protected GunType gunType;
        public virtual void Attack()
        {
            
        }
    }
}