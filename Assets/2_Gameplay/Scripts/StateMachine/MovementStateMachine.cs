using System.Collections.Generic;
using Gameplay.StateMachine.States;
using UnityEngine;

namespace Gameplay.StateMachine
{
    public class MovementStateMachine
    {
        private readonly Dictionary<MovementStateID, IMovementState> _states;
        
        private MovementStateID _currentID;
        private IMovementState _currentState;
        
        public MovementStateMachine(PlayerController _controller)
        {
            _states = new Dictionary<MovementStateID, IMovementState>
            {
                { MovementStateID.Grounded, new GroundedState(_controller) },
                { MovementStateID.Jumping,  new JumpingState(_controller)  },
            };
        }
        
        public void Initialize(MovementStateID startID)
        {
            _currentID    = startID;
            _currentState = _states[startID];
            _currentState.Enter();
        }

        public void FireTrigger(MovementTrigger trigger)
        {
            var nextID = _currentState.HandleTrigger(trigger);
            if (nextID.HasValue && nextID.Value != _currentID)
            {
                _currentState.Exit();
                _currentID    = nextID.Value;
                _currentState = _states[_currentID];
                _currentState.Enter();
            }
        }
        
        public void HandleInput(InputData inputData)
            => _currentState.HandleInput(inputData);
        public void PhysicsUpdate() => _currentState.PhysicsUpdate();
        public void OnCollisionEnter(Collision col) => _currentState.OnCollisionEnter(col);
    }
    
    public enum MovementStateID
    {
        Grounded,
        Jumping,
    }

    public enum MovementTrigger
    {
        Jump,
        Land,
    }
}