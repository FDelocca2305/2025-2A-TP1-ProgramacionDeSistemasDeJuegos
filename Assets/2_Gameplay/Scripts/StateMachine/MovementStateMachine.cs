using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.StateMachine
{
    public class MovementStateMachine
    {
        private IMovementState _current;
        public void Initialize(IMovementState startState)
        {
            _current = startState;
            _current.Enter();
        }
        public void ChangeState(IMovementState next)
        {
            _current.Exit();
            _current = next;
            _current.Enter();
        }
        public void HandleInput(Vector3 moveInput, bool jumpPressed)
            => _current.HandleInput(moveInput, jumpPressed);
        public void PhysicsUpdate() => _current.PhysicsUpdate();
        public void OnCollisionEnter(Collision col) => _current.OnCollisionEnter(col);
    }
}