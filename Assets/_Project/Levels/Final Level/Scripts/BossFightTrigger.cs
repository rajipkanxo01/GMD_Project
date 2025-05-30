using UnityEngine;

public class BossFightTrigger : MonoBehaviour
{
    [SerializeField] private BossController boss;
    [SerializeField] private ArenaController arena;
    [SerializeField] private GameObject bossHealthBar;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        boss.StartBossFight();
        arena.LockArena();

        if (bossHealthBar != null)
            bossHealthBar.SetActive(true);

        gameObject.SetActive(false);
    }
}
