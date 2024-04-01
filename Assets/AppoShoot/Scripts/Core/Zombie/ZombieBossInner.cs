using UnityEngine;

public class ZombieBossInner : MonoBehaviour
{
    [SerializeField] private ZombieBoss _zombieBoss;

    public void TakeDamage(int damage)
    {
        _zombieBoss.TakeDamage(damage);
    }
}
