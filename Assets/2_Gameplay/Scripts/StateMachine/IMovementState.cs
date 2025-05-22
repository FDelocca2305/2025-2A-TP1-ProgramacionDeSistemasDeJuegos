using UnityEngine;

namespace Gameplay.StateMachine
{
    public interface IMovementState
    {
        void Enter();
        void HandleInput(InputData inputData);
        void PhysicsUpdate();
        void OnCollisionEnter(Collision collision);
        void Exit();
        MovementStateID? HandleTrigger(MovementTrigger trigger);
    }
}