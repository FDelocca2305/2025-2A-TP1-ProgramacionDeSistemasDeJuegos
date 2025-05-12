using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.StateMachine.States
{
    public class GroundedState : IMovementState
    {
        private readonly PlayerController _controller;
        private Vector3 _moveDir;

        public GroundedState(PlayerController controller) => _controller = controller;
        public void Enter() { _moveDir = Vector3.zero; }
        public void Exit() { }

        public void HandleInput(Vector3 moveInput, bool jumpPressed)
        {
            _moveDir = moveInput;
            if (jumpPressed)
                _controller.ChangeState(new JumpingState(_controller));
        }

        public void PhysicsUpdate()
        {
            _controller.Character.SetDirection(_moveDir);
        }

        public void OnCollisionEnter(Collision col) { }
    }
}