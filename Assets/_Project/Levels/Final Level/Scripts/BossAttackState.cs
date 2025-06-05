using UnityEngine;

public class BossAttackState : IBossState
{
    private readonly BossController boss;

    public BossAttackState(BossController boss)
    {
        this.boss = boss;
    }

    public void Enter()
    {
        boss.SetWalking(false); // start idle
    }

    public void Exit()
    {
        boss.SetWalking(false); // reset
    }

    public void Update()
    {
        // If already in attack, hit, or death — don’t move
        var anim = boss.Animator;
        var state = anim.GetCurrentAnimatorStateInfo(0);
        if (state.IsTag("attack") || state.IsTag("hit") || state.IsTag("death") || state.IsTag("intro"))
        {
            boss.SetWalking(false);
            return;
        }

        boss.FacePlayer();

        float distance = Vector2.Distance(boss.transform.position, boss.Player.position);

        if (distance <= boss.AttackDecisionRange)
        {
            boss.SetWalking(false);
            if (boss.IsAttackCooldownReady())
                boss.TryAttack();
        }
        else
        {
            boss.SetWalking(true);

            // Chase only on X axis
            Vector3 target = boss.Player.position;
            target.y = boss.transform.position.y;
            boss.transform.position = Vector3.MoveTowards(
                boss.transform.position,
                target,
                boss.ChaseSpeed * Time.deltaTime
            );
        }
    }
}
