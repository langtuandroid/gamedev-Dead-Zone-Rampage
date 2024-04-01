using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Animator _weaponReloadAnim;
    [SerializeField] private Animator _starCollect;
    [SerializeField] private Image _cartridgesProgress;
    [SerializeField] private Animator _zombieBossTextShowAnimator;
    [SerializeField] private GameObject _zombieBossHealthUI;
    [SerializeField] private Image _zombieBosshealthProgressImage;
    public GameObject DeadWindow;
    public GameObject FinishWindow;
    public static bool isStop;
    [SerializeField] private TextMeshProUGUI _finalLevelDisplayText;
    [SerializeField] private TextMeshProUGUI _zombieCountDisplayText;
    [SerializeField] private TextMeshProUGUI _zombieMoneyDisplayText;
    [SerializeField] private TextMeshProUGUI _questDescription;
    [SerializeField] private TextMeshProUGUI _questDescriptionDeadWindow;
    [SerializeField] private Image _questProgress;
    private PlayerData _playerData;
    private LevelManager _levelManager;
    private ZombieCounterScene _zombieCounterScene;
    private Wallet _wallet;
    private float _zombieCount;
    private float _zombieMoney;
    private bool _startCounter;

    private void Awake()
    {
        isStop = false;
        _playerData = FindObjectOfType<PlayerData>();
        _wallet = FindObjectOfType<Wallet>();
    }

    private void Start()
    {
        _zombieCounterScene = FindObjectOfType<ZombieCounterScene>();
        _levelManager = FindObjectOfType<LevelManager>();

        _questDescription.text = _levelManager.LevelItem[_playerData.GetLevel()].QuestText + " " + _levelManager.LevelItem[_playerData.GetLevel()].NeedValueObject;
    }

    private void Update()
    {
        if (_startCounter)
        {
            if (_zombieCount < _zombieCounterScene._zombieCount)
            {
                _zombieCount += Time.deltaTime * 10;
                _zombieCountDisplayText.text = "X " + _zombieCount.ToString("f0");
            }

            if (_zombieMoney < _zombieCounterScene._zombieCount * 3.5f)
            {
                _zombieMoney += Time.deltaTime * 18;
                _zombieMoneyDisplayText.text = "+ " + _zombieMoney.ToString("f0");
            }
        }
    }

    public void ReloadWeaponAnim()
    {
        _weaponReloadAnim.SetTrigger("in");
    }

    public void StarCollect()
    {
        _starCollect.SetTrigger("in");
    }

    public void CheckQuestUI()
    {
        _questProgress.fillAmount = (float)_levelManager.currentValue / _levelManager.LevelItem[_playerData.GetLevel()].NeedValueObject;
        _questDescription.text = _levelManager.LevelItem[_playerData.GetLevel()].QuestText + " " + (_levelManager.LevelItem[_playerData.GetLevel()].NeedValueObject - _levelManager.currentValue);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevelButton()
    {
        SceneManager.LoadScene(0);
    }

    public void PlayerDead()
    {
        _questDescriptionDeadWindow.text = _levelManager.LevelItem[_playerData.GetLevel()].QuestText + " " + _levelManager.LevelItem[_playerData.GetLevel()].NeedValueObject;
        DeadWindow.SetActive(true);
    }

    public void PlayerFinished()
    {
        isStop = true;
        MenuMapUI._currentLevelItem++;
        _finalLevelDisplayText.text = "Level" + (_playerData.GetLevel() + 1);
        _playerData.SetLevel();
        PlayerPrefs.SetInt("_currentLevelItem", MenuMapUI._currentLevelItem);
        _wallet.SetMoney((_zombieCounterScene._zombieCount * 3.5f));
        StartCoroutine(StartCounter());
        FinishWindow.SetActive(true);
    }

    public void UpdateCartridgesProgress(int value)
    {
        _cartridgesProgress.fillAmount = (float)value / 10;
    }

    IEnumerator StartCounter()
    {
        yield return new WaitForSeconds(0.6f);
        _startCounter = true;
    }

    public void ZombieUIActivate(int active)
    {
        if (active == 0)
        {
            _zombieBossHealthUI.SetActive(false);
        }
        else
        {
            _zombieBossHealthUI.SetActive(true);
            _zombieBossTextShowAnimator.SetTrigger("show");
        }

    }

    public void UpdateZombieBossHealth(int current, int baseHp)
    {
        _zombieBosshealthProgressImage.fillAmount = (float)current / baseHp;
    }
}
