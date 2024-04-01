using UnityEngine.AI;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public int Health;
    private NavMeshAgent _agent;
    public Transform _target;
    private Player _player;
    [SerializeField] private GameObject _parent;
    [SerializeField] private GameObject[] _deadParticle;
    private Animator _animator;
    private GameObject _clone;
    private bool isdead;
    private Wallet _wallet;
    private LevelManager _levelManager;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _player = FindObjectOfType<Player>();
        _levelManager = FindObjectOfType<LevelManager>();
        _target = _player.transform;
        _wallet = _player.wallet;
    }

    public void PlayerDetected()
    {
        _agent.SetDestination(_target.position);
        _animator.SetBool("run", true);
    }

    public void TakeDamage(int damage, int weaponType)
    {
        Health -= damage;

        if (Health <= 0 && !isdead)
        {
            _agent.Stop();            
            GetComponent<CapsuleCollider>().enabled = false;

            switch (weaponType)
            {
                case 5:
                    _clone = Instantiate(_deadParticle[1], new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z), Quaternion.Euler(0, 90, 0));
                    Destroy(_parent);
                    break;

                default:
                    _clone = Instantiate(_deadParticle[0], new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z), Quaternion.Euler(0, 90, 0));
                    _animator.SetTrigger("death");
                    break;
            }

            _levelManager.checkValue("zombie");
            Destroy(_clone, 1.4f);
            _player._zombieCounterScene.UpdateZombieCount(1);
            isdead = true;


        }
    }
}
