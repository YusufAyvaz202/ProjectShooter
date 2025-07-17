using Abstracts;
using UnityEngine;
namespace Ammunition
{
    public class Fireball : BaseAmmunition
    {
        private void FixedUpdate()
        {
            Vector3 newPosition = transform.position + transform.forward * (speed * Time.fixedDeltaTime);
            _rigidbody.MovePosition(newPosition);
        }
    }
}