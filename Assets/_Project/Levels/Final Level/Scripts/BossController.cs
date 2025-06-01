using _Project.Scripts.Health;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private int attackDamage = 10;
    [SerializeField] private float attackRange = 0.9f;
    [SerializeField] private float attackDecisionRange = 1.5f;
    [SerializeField] private float chaseSpeed = 1f;

    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform lowAttackOrigin;
    [SerializeField] private Transform midAttackOrigin;
    [SerializeField] private Transform groundAttackOrigin;
    [SerializeField] private Transform decisionRangeOrigin;
    [SerializeField] private BossHealthUI bossHealthUI;
    [SerializeField] private LayerMask playerLayer;

    private int currentHealth;
    private float cooldownTimer;
    private bool hasStarted;
    private bool isDead = false;

    private Animator animator;
    private BossStateMachine stateMachine;
    private BossAudioController audioController;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        stateMachine = new BossStateMachine();
        currentHealth = maxHealth;
        audioController=GetComponent<BossAudioController>();
    }

    private void Update()
    {
        if (!hasStarted || isDead) return;

        cooldownTimer += Time.deltaTime;

        if (IsInTaggedState("hit") || IsInTaggedState("death"))
        {
            SetWalking(false);
            return;
        }

        stateMachine.Update();
    }

    public void StartBossFight()
    {
        if (hasStarted) return;
        hasStarted = true;
        if (bossHealthUI != null)
            bossHealthUI.SetMaxHealth(maxHealth);

        animator.SetTrigger("intro");
        stateMachine.ChangeState(new BossIntroState(this));
    }

    public void TryAttack()
    {
        if (cooldownTimer < attackCooldown || isDead || IsInTaggedState("hit")) return;

        cooldownTimer = 0f;

        string[] attacks = { "1_atk", "2_atk", "3_atk" };
        string selected = attacks[Random.Range(0, attacks.Length)];

        Transform origin = selected switch
        {
            "1_atk" => lowAttackOrigin,
            "2_atk" => midAttackOrigin,
            "3_atk" => groundAttackOrigin,
            _ => midAttackOrigin
        };

        animator.SetTrigger(selected);
        audioController.PlayAttackSound();

        Collider2D hit = Physics2D.OverlapCircle(origin.position, attackRange, playerLayer);
        if (hit && hit.CompareTag("Player"))
        {
            var health = hit.GetComponent<PlayerHealthController>();
            if (health != null)
            {
                health.DecreaseHealth(attackDamage);
            }
        }
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        Debug.Log($"Boss took {amount} damage. Current health: {currentHealth}");
        animator.SetTrigger("take_hit");
        audioController.PlayHurtSound();
        if (bossHealthUI != null)
            bossHealthUI.UpdateHealth(currentHealth);
        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        isDead = true;
        animator.SetTrigger("death");
        audioController.PlayDeathSound();
        Destroy(gameObject, 2f);
    }

    public void ResetAttackCooldown() => cooldownTimer = 0f;
    public bool IsAttackCooldownReady() => cooldownTimer >= attackCooldown;

    public Transform Player => player;
    public float ChaseSpeed => chaseSpeed;
    public float AttackRange => attackRange;
    public float AttackDecisionRange => attackDecisionRange;
    public LayerMask PlayerLayer => playerLayer;
    public Animator Animator => animator;
    public BossStateMachine StateMachine => stateMachine;

    public void FacePlayer()
    {
        Vector3 scale = transform.localScale;
        scale.x = player.position.x < transform.position.x ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        transform.localScale = scale;
    }

    public void SetWalking(bool isWalking)
    {
        if (!isDead && !IsInTaggedState("hit") && !IsInTaggedState("attack"))
            animator.SetBool("isWalking", isWalking);
        else
            animator.SetBool("isWalking", false);
    }

    private bool IsInTaggedState(string tag)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsTag(tag);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (lowAttackOrigin != null)
            Gizmos.DrawWireSphere(lowAttackOrigin.position, attackRange);
        Gizmos.color = Color.yellow;
        if (midAttackOrigin != null)
            Gizmos.DrawWireSphere(midAttackOrigin.position, attackRange);
        Gizmos.color = Color.cyan;
        if (groundAttackOrigin != null)
            Gizmos.DrawWireSphere(groundAttackOrigin.position, attackRange);

        Gizmos.color = Color.magenta;
        if (decisionRangeOrigin != null)
            Gizmos.DrawWireSphere(decisionRangeOrigin.position, attackDecisionRange);
    }
}