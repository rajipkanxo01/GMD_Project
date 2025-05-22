using UnityEngine;

public class ArenaController : MonoBehaviour
{
    [SerializeField] private GameObject[] arenaWalls;

    public void LockArena()
    {
        foreach (var wall in arenaWalls)
            wall.SetActive(true);
    }

    public void UnlockArena()
    {
        foreach (var wall in arenaWalls)
            wall.SetActive(false);
    }
}
