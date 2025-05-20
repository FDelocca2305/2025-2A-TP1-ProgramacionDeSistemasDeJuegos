using UnityEngine;

namespace Gameplay.StateMachine.States
{
    public class JumpingState : IMovementState
    {
        private readonly PlayerController _controller;
        private Vector3 _moveDir;
        private int _jumpsLeft;

        public JumpingState(PlayerController controller)
        {
            _controller = controller;
            _jumpsLeft = controller.MaxJumps - 1;
        }

        public void Enter()
        {
            _controller.Character.Jump();
            _controller.Character.OnLand += HandleLand;
        }

        public void Exit()
        {
            _controller.Character.OnLand -= HandleLand;
        }

        public void HandleInput(Vector3 moveInput, bool jumpPressed)
        {
            _controller.Character.SetDirection(moveInput * _controller.AirborneSpeedMultiplier);

            if (jumpPressed && _jumpsLeft > 0)
            {
                _controller.Character.Jump();
                _jumpsLeft--;
            }
        }

        public void PhysicsUpdate() { }

        public void OnCollisionEnter(Collision col)
        {
            foreach (var contact in col.contacts)
            {
                if (Vector3.Angle(contact.normal, Vector3.up) < 5)
                {
                    HandleLand();
                    break;
                }
            }
        }
        
        private void HandleLand()
        {
            _controller.ChangeState(new GroundedState(_controller));
        }

    }
}