using UnityEngine;

namespace Gameplay.StateMachine
{
    public interface IMovementState
    {
        void Enter();
        void HandleInput(Vector3 moveInput, bool jumpPressed);
        void PhysicsUpdate();
        void OnCollisionEnter(Collision collision);
        void Exit();
    }
}