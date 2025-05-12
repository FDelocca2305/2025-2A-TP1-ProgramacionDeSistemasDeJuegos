using System;
using System.Collections;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(Rigidbody))]
    public class Character : MonoBehaviour
    {
        [SerializeField] private float acceleration = 10;
        [SerializeField] private float speed = 3;
        [SerializeField] private float jumpForce = 10;
        private Vector3 _direction = Vector3.zero;
        private Rigidbody _rigidbody;
        public event Action OnLand;

        private void Awake()
            => _rigidbody = GetComponent<Rigidbody>();

        private void FixedUpdate()
        {
            var scaledDirection = _direction * acceleration;
            if (_rigidbody.linearVelocity.IgnoreY().magnitude < speed)
                _rigidbody.AddForce(scaledDirection, ForceMode.Force);
        }

        public void SetDirection(Vector3 direction)
            => _direction = direction;

        public void Jump()
        {
            var linearVelocity = _rigidbody.linearVelocity;
            linearVelocity = new Vector3(linearVelocity.x, 0, linearVelocity.z);
            _rigidbody.linearVelocity = linearVelocity;
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        
        private void OnCollisionEnter(Collision col)
        {
            foreach (var c in col.contacts)
            {
                if (c.normal.y > 0.9f)
                {
                    OnLand?.Invoke();
                    break;
                }
            }
        }
    }
}