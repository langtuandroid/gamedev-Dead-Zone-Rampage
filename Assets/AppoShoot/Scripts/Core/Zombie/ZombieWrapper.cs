using UnityEngine;

public class ZombieWrapper : MonoBehaviour
{
    public Zombie _zombie;
    [SerializeField] private int _zombieSkinId;
    public bool randomZombie;
    public bool eatZombie;
    [SerializeField] private GameObject[] _zombieSkins;

    private void Awake()
    {
        if (randomZombie)
            _zombieSkins[Random.Range(0,_zombieSkins.Length)].SetActive(true);
        else
            _zombieSkins[_zombieSkinId].SetActive(true);

        if (eatZombie)
            _zombie.GetComponent<Animator>().SetTrigger("eat");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            _zombie.PlayerDetected();
        }
    }
}
