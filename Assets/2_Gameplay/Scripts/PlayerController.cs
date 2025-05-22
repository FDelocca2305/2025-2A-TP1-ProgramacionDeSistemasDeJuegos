using Gameplay.StateMachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    [RequireComponent(typeof(Character))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private InputActionReference moveInput;
        [SerializeField] private InputActionReference jumpInput;
        [SerializeField] private float airborneSpeedMultiplier = .5f;
        [SerializeField] private int maxJumps = 2;
        private Character _character;
        
        private MovementStateMachine _movementStateMachine;
        
        private Vector3 _moveValue;
        private bool _jumpPressed;
        
        public Character Character => _character;
        public float AirborneSpeedMultiplier => airborneSpeedMultiplier;
        public int MaxJumps => maxJumps;
        
        private void Awake()
        {
            _character = GetComponent<Character>();
            _movementStateMachine = new MovementStateMachine(this);
            _movementStateMachine.Initialize(MovementStateID.Grounded);
        }

        private void OnEnable()
        {
            if (moveInput?.action != null)
            {
                moveInput.action.started += HandleMoveInput;
                moveInput.action.performed += HandleMoveInput;
                moveInput.action.canceled += HandleMoveInput;
            }
            if (jumpInput?.action != null)
                jumpInput.action.performed += HandleJumpInput;
        }
        
        private void OnDisable()
        {
            if (moveInput?.action != null)
            {
                moveInput.action.started   -= HandleMoveInput;
                moveInput.action.performed -= HandleMoveInput;
                moveInput.action.canceled -= HandleMoveInput;
            }
            if (jumpInput?.action != null)
                jumpInput.action.performed -= HandleJumpInput;
        }

        private void HandleMoveInput(InputAction.CallbackContext ctx)
        {
            _moveValue = ctx.ReadValue<Vector2>().ToHorizontalPlane();
        }

        private void HandleJumpInput(InputAction.CallbackContext ctx)
        {
            _jumpPressed = ctx.performed;
        }
        
        private void Update() {
            var data = new InputData {
                MoveVector  = _moveValue,
                JumpPressed = _jumpPressed
            };
            _movementStateMachine.HandleInput(data);
            _jumpPressed = false;
        }
        
        private void FixedUpdate() {
            _movementStateMachine.PhysicsUpdate();
        }

        private void OnCollisionEnter(Collision col)
            => _movementStateMachine.OnCollisionEnter(col);
        
        public void FireTrigger(MovementTrigger trigger) => _movementStateMachine.FireTrigger(trigger);
        
    }
}