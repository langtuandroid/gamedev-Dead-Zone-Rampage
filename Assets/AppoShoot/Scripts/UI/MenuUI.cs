using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuUI : MonoBehaviour
{
    private PlayerData _playerData;
    private Wallet _wallet;
    [SerializeField] private Animator _gunWindowAnimator;
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private TextMeshProUGUI _levelDisplayText;

    private bool _isGunShow;

    private void Start()
    {
        _playerData = FindObjectOfType<PlayerData>();
        _wallet = FindObjectOfType<Wallet>();
      
        _levelDisplayText.text = "Level " + (_playerData.GetLevel()+1);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
     
    private void Update()
    {
        _moneyText.text = _wallet.GetMoney().ToString();

        if (Input.GetKeyDown(KeyCode.Tab))
            PlayerPrefs.DeleteAll();
    }

    public void ShowYourGunWindow()
    {
        _isGunShow = !_isGunShow;

        if (_isGunShow)
            _gunWindowAnimator.SetTrigger("show");
        else
            _gunWindowAnimator.SetTrigger("hide");
    }

    public void DeleteAllSaves()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }
}
