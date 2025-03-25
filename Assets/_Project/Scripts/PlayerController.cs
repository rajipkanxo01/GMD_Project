using _Project.Scripts.Health;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts {
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody2D rb;
        public Transform groundCheck;
        public LayerMask groundLayer;
        
        private float horizontalInput;
        private bool isFacingRight = true;
        private Animator animator;
        private bool isGrounded;
        
        [Header("Movement")]
        [SerializeField] private float speed = 5f;
        [SerializeField] private float jumpingPower = 16f;
        [SerializeField] private PlayerHealthBar playerHealthBar;
        private void Awake() {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        void FixedUpdate()
        {
            rb.linearVelocity = new Vector2 (horizontalInput*speed, rb.linearVelocity.y);
            if(!isFacingRight && horizontalInput > 0f) 
            {
                Flip();
            }
            else if (isFacingRight && horizontalInput < 0f)
            {
                Flip();
            }
            isGrounded = IsGrounded();
            animator.SetBool("grounded", isGrounded);
            animator.SetBool("move",horizontalInput!=0);
        }

        public void Move(InputAction.CallbackContext context) 
        {
            Vector2 input = context.ReadValue<Vector2>();
            horizontalInput = input.x;
        }
        private bool IsGrounded()
        {
            return Physics2D.OverlapCircle(groundCheck.position,0.2f,groundLayer);
        }

        private void Flip() 
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }

        public void Jump(InputAction.CallbackContext context)
        {
            if (context.performed && IsGrounded())
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
                animator.SetTrigger("Jump");
            }
        }
    }
}
