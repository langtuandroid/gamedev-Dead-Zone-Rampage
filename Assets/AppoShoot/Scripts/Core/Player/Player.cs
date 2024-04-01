using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator _animInner;
    [HideInInspector] public Wallet wallet;
    [HideInInspector] public ZombieCounterScene _zombieCounterScene;
    private GameUI _gameUI;
    private Controller _controller;
    private Animator _animator;
    private AudioSource _audioSource;
    private CombatController _combatController;
    private LevelManager _levelManager;
    private PlayerData _playerData;

    private void Start()
    {
        _gameUI = FindObjectOfType<GameUI>();
        _controller = FindObjectOfType<Controller>();
        wallet = FindObjectOfType<Wallet>();
        _combatController = GetComponent<CombatController>();
        _audioSource = GetComponent<AudioSource>();
        _animator = FindObjectOfType<CombatController>()._anim;
        _levelManager = FindObjectOfType<LevelManager>();
        _playerData = FindObjectOfType<PlayerData>();
        _zombieCounterScene = GetComponent<ZombieCounterScene>();
    }

    private void Update()
    {
        if (GameUI.isStop)
            _animInner.SetBool("stop", true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Zombie>())
        {
            _animator.SetTrigger("death");
            _controller.enabled = false;
            _gameUI.PlayerDead();
            GameUI.isStop = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
        {
            _controller.enabled = false;

            if (_levelManager.currentValue < _levelManager.LevelItem[_playerData.GetLevel()].NeedValueObject)
                _gameUI.PlayerDead();
            else
                _gameUI.PlayerFinished();
        }

        if (other.tag == "cartridges")
        {
            _audioSource.Play();
            _gameUI.ReloadWeaponAnim();
            Destroy(other.gameObject);
            _combatController.CartridgesFull();
        }

        if (other.tag == "star")
        {
            Destroy(other.gameObject);
            _levelManager.checkValue("star");
            _gameUI.StarCollect();
        }

        if (other.GetComponent<ZombieBoss>())
        {
            _gameUI.ZombieUIActivate(1);
            _controller.VariableSpeed = 0;
            _animInner.SetBool("stop", true);
            other.GetComponent<ZombieBoss>()._startBattle = true;
        }
    }

    public void ContinueMove()
    {
        _gameUI.ZombieUIActivate(0);
        _controller.VariableSpeed = 1;
        _animInner.SetBool("stop", false);
    }
}
