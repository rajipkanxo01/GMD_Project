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
        
        [Header("Health")]
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private int currentHealth;
        [SerializeField] private HealthBar healthBar;
        [SerializeField] private float invincibleTime = 2f;
        private bool isInvincible;
        private float cooldownTime;
        
        
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            currentHealth = maxHealth;
        }

        void Update() {
            if (isInvincible) {
                cooldownTime -= Time.deltaTime;
                if (cooldownTime <= 0) {
                    isInvincible = false;
                }
            }
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

        //Decreases health. Also has cooldown time in scenarios where damage zone can continuously decrease player's health
        public void TakeDamage(int amount) {
            if (amount < 0) {
                if (isInvincible) {
                    return;
                }
                isInvincible = true;
                cooldownTime = invincibleTime;
            }
            currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
            healthBar.SetHealth(currentHealth);
        }

        //Adds health
        public void AddHealth(int amount) {
            currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
            healthBar.SetHealth(currentHealth);
        }
        
        public int Health => currentHealth;

        public int MaxHealth => maxHealth;

    }
}
