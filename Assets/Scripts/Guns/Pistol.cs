using Abstracts;
using UnityEngine;

namespace Guns
{
    public class Pistol : BaseGun
    {
        public override void Attack()
        {
            base.Attack();
            Debug.Log("Pistol Attack");
        }
    }
}