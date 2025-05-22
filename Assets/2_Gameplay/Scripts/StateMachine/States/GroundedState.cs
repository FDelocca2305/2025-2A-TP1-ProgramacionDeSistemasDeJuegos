using UnityEngine;

namespace Gameplay.StateMachine.States
{
    public class GroundedState : IMovementState
    {
        private readonly PlayerController _controller;
        private Vector3 _moveDir;

        public GroundedState(PlayerController controller) => _controller = controller;
        public void Enter() { _moveDir = Vector3.zero; }
        public void Exit() { }

        public void HandleInput(InputData inputData)
        {
            _moveDir = inputData.MoveVector;
            if (inputData.JumpPressed)
                _controller.FireTrigger(MovementTrigger.Jump);
        }

        public void PhysicsUpdate()
        {
            _controller.Character.SetDirection(_moveDir);
        }

        public void OnCollisionEnter(Collision col) { }
        
        public MovementStateID? HandleTrigger(MovementTrigger trigger)
        {
            if (trigger == MovementTrigger.Jump)
                return MovementStateID.Jumping;
            return null;
        }
    }
}