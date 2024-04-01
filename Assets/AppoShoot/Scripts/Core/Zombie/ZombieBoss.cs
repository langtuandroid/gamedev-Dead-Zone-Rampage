using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class ZombieBoss : MonoBehaviour
{
    public int health;
    public float spawnTimer = 3.5f;
    public GameObject ZombiePrefab;
    [HideInInspector] public bool _startBattle;
    private Animator _zombieAnimator;
    [SerializeField] private NavMeshAgent _zombieNavAgent;
    [SerializeField] private Transform[] _points;
    [SerializeField] private GameObject _deadParticle;
    private Player _player;
    private float _timer;
    private bool isdead;
    private GameObject _z_clone;
    private LevelManager _levelManager;

    private GameObject _clone;
    private GameUI _gameUI;
    private int _baseHp;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _gameUI = FindObjectOfType<GameUI>();
        _levelManager = FindObjectOfType<LevelManager>();
        _zombieAnimator = _zombieNavAgent.GetComponent<Animator>();
        _baseHp = health;
    }

    void Update()
    {
        if (!isdead && _startBattle)
        {
            if (_timer <= 0)
            {
                _zombieAnimator.SetTrigger("spawn");
                _z_clone = Instantiate(ZombiePrefab, _zombieNavAgent.transform.position, Quaternion.identity);
                _z_clone.GetComponent<ZombieWrapper>().eatZombie = true;
                _timer = spawnTimer;
                ChangeDirection();
            }
            else
            {
                _timer -= Time.deltaTime;
            }
        }
    }

    private void ChangeDirection()
    {
        _zombieNavAgent.SetDestination(_points[Random.Range(0, _points.Length)].position);
    }

    IEnumerator NewDirection()
    {
        yield return new WaitForSeconds(1.4f);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        _gameUI.UpdateZombieBossHealth(health, _baseHp);
        _zombieAnimator.SetTrigger("hit");
        _clone = Instantiate(_deadParticle, new Vector3(_zombieNavAgent.transform.position.x, _zombieNavAgent.transform.position.y + 2f, _zombieNavAgent.transform.position.z), Quaternion.Euler(0, 90, 0));
        Destroy(_clone, 1.4f);

        if (health <= 0 && !isdead)
        {
            _zombieNavAgent.Stop();
            _zombieNavAgent.GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            _zombieAnimator.SetTrigger("dead");
            isdead = true;
            _player._zombieCounterScene.UpdateZombieCount(10);
            _player.ContinueMove();
            _levelManager.checkValue("boss");
        }
    }
}
