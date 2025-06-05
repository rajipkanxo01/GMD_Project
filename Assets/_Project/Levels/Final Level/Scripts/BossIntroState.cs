using UnityEngine;

public class BossIntroState : IBossState
{
    private BossController boss;
    private float timer = 0f;
    private float introDuration = 2f;

    public BossIntroState(BossController boss)
    {
        this.boss = boss;
    }

    public void Enter()
    {
        boss.Animator.SetTrigger("intro");
        boss.SetWalking(false);
        timer = 0f;

        // Freeze position entirely (not just X)
        boss.transform.position = new Vector3(
            Mathf.Round(boss.transform.position.x),
            boss.transform.position.y, // Keep Y fixed
            boss.transform.position.z
        );
    }

    public void Update()
    {
        timer += Time.deltaTime;
        if (timer >= introDuration)
        {
            boss.StateMachine.ChangeState(new BossAttackState(boss));
        }
    }

    public void Exit() { }
}
