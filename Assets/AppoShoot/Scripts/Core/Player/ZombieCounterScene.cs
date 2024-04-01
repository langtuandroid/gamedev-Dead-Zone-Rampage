using UnityEngine;

public class ZombieCounterScene : MonoBehaviour
{
    public int _zombieCount;

    public void UpdateZombieCount(int count)
    {
        _zombieCount += count;
    }
}
