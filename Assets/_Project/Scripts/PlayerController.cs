using _Project.Scripts.Health;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        #region Components

        private Rigidbody2D _rb;
        private Animator _animator;

        #endregion

        #region Movement

        [Header("Movement Settings")]
        [SerializeField] private float speed = 5f;
      
        private float _horizontalInput;
        private bool _isFacingRight = true;

        #endregion

        #region Jump
        [Header("Jump Settings")]
        [SerializeField] private float fallMultiplier = 2.5f;
        [SerializeField] private float jumpingPower = 16f;
        [SerializeField] private float jumpTime;
        [SerializeField] private float jumpMultiplier;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private LayerMask groundLayer;
        
        private bool _isGrounded;
        private Vector2 _gravityMultiplier;
        private bool _isJumping;
        private float _jumpCounter;
        private Vector2 _vecGravity;
        #endregion

        #region Health
        [Header("Health Settings")]
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private float invincibleTime = 2f;
        [SerializeField] private HealthBar healthBar;
        private int _currentHealth;
        private bool _isInvincible;
        private float _invincibilityCooldown;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _gravityMultiplier = new Vector2(0, -Physics2D.gravity.y);
        }

        private void Start()
        {
            _currentHealth = maxHealth;
            _vecGravity = new Vector2(0, -Physics2D.gravity.y);
            healthBar.SetMaxHealthLevel(maxHealth);
        }

        private void Update()
        {
            HandleInvincibility();
            ApplyFallMultiplier();

            // Handle held jump (variable jump height)
            if (_isJumping && _rb.linearVelocity.y > 0)
            {
                _jumpCounter += Time.deltaTime;

                if (_jumpCounter < jumpTime)
                {
                    _rb.linearVelocity += _vecGravity * (jumpMultiplier * Time.deltaTime);
                }
                else
                {
                    _isJumping = false;
                }
            }
        }

        private void FixedUpdate()
        {
            MovePlayer();
            UpdateAnimationStates();
        }

        #endregion

        #region Input Methods

        public void Move(InputAction.CallbackContext context)
        {
            _horizontalInput = context.ReadValue<Vector2>().x;
        }

        public void Jump(InputAction.CallbackContext context)
        {
            if (context.started && _isGrounded)
            {
                _isJumping = true;
                _jumpCounter = 0f;

                _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0f);
                _rb.AddForce(Vector2.up * jumpingPower, ForceMode2D.Impulse);
                _animator.SetTrigger("Jump");
            }

            if (context.canceled)
            {
                _isJumping = false;
            }
        }


        #endregion

        #region Movement Helpers

        private void MovePlayer()
        {
            _rb.linearVelocity = new Vector2(_horizontalInput * speed, _rb.linearVelocity.y);

            if ((_isFacingRight && _horizontalInput < 0f) || (!_isFacingRight && _horizontalInput > 0f))
            {
                Flip();
            }

            _isGrounded = IsGrounded();
        }

        private void Flip()
        {
            _isFacingRight = !_isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1f;
            transform.localScale = scale;
        }

        private void ApplyFallMultiplier()
        {
            if (_rb.linearVelocity.y < 0)
            {
                _rb.linearVelocity -= _vecGravity * (fallMultiplier * Time.deltaTime);
            }
        }

        private bool IsGrounded()
        {
            return Physics2D.OverlapCircle(groundCheck.position, 0.02f, groundLayer);
        }

        private void UpdateAnimationStates()
        {
            _animator.SetBool("grounded", _isGrounded);
            _animator.SetBool("move", _horizontalInput != 0);
        }

        #endregion

        #region Invincibility and Health

        private void HandleInvincibility()
        {
            if (!_isInvincible) return;

            _invincibilityCooldown -= Time.deltaTime;
            if (_invincibilityCooldown <= 0)
            {
                _isInvincible = false;
            }
        }

        public void TakeObstacleDamage(int amount)
        {
            if (_isInvincible) return;

            _isInvincible = true;
            _invincibilityCooldown = invincibleTime;

            ApplyDamage(amount);
        }

        public void DecreaseHealth(int amount)
        {
            ApplyDamage(amount);
        }

        public void AddPlayerHealth(int amount)
        {
            _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, maxHealth);
            healthBar.SetHealthLevel(_currentHealth);
        }

        private void ApplyDamage(int amount)
        {
            _currentHealth = Mathf.Clamp(_currentHealth - amount, 0, maxHealth);
            healthBar.SetHealthLevel(_currentHealth);
        }

        #endregion

        #region Properties

        public int CurrentHealth => _currentHealth;
        public int MaxHealth => maxHealth;

        #endregion
    }
}
