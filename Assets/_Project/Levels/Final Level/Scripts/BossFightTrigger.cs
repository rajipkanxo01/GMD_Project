using UnityEngine;

public class BossFightTrigger : MonoBehaviour
{
    [SerializeField] private BossController boss;
    [SerializeField] private ArenaController arena;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        boss.StartBossFight();
        arena.LockArena();
        gameObject.SetActive(false); // optional: disable after triggered
    }
}
