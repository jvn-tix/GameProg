using UnityEngine;
using UnityEngine.InputSystem;

namespace Apps.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        // attributes
        public float JumpForce = 5f;
        public Transform groundCheck;
        public bool IsGrounded { get; private set; }
        public LayerMask groundLayer;

        private Rigidbody rb;
        private JumpAction jump;
        private MovementAction _movementAction;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            jump = new JumpAction();
            _movementAction = GetComponent<MovementAction>();
        }

        void Update()
        {
            IsGrounded = Physics.CheckSphere(
            groundCheck.position,
            0.2f,
            groundLayer
            );
        }
        public void OnMove(InputAction.CallbackContext ctx)
        {
            if (_movementAction is null)
            {
                Debug.LogWarning("Component MovementAction is not attached to the object", this);
                return;
            }
        
            if (ctx.performed || ctx.canceled)
            {
                _movementAction.Execute(ctx.ReadValue<Vector2>());
            }
        }
        public void OnJump(InputAction.CallbackContext ctx)
        {
            if (ctx.phase != InputActionPhase.Started) return;

            jump.Execute(new JumpInfo
            {
                rb = rb,
                JumpForce = JumpForce,
                IsGrounded = IsGrounded
            });
        }
    }
}
