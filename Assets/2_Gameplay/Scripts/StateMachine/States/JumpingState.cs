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

        public void HandleInput(InputData inputData)
        {
            _controller.Character.SetDirection(inputData.MoveVector * _controller.AirborneSpeedMultiplier);

            if (inputData.JumpPressed && _jumpsLeft > 0)
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
        
        public MovementStateID? HandleTrigger(MovementTrigger trigger)
        {
            if (trigger == MovementTrigger.Land)
                return MovementStateID.Grounded;
            return null;
        }
        
        private void HandleLand()
        {
            _controller.FireTrigger(MovementTrigger.Land);
        }

    }
}