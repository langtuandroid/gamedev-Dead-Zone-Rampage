using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GunManagerUI : MonoBehaviour
{
    private GunManager _gunManager;
    private PlayerData _playerData;
    private Wallet _wallet;
    [SerializeField] private GameObject[] _guns;
    [SerializeField] private GameObject[] _weapons;

    public GameObject BuyWindow;
    [SerializeField] TextMeshProUGUI _btnDisplayText;
    [SerializeField] TextMeshProUGUI _titleDisplayText;

    //purchase data "BuyWindow"
    private int _id;
    private int _price;
    private int _type;

    void Start()
    {
        _gunManager = FindObjectOfType<GunManager>();
        _playerData = FindObjectOfType<PlayerData>();
        _wallet = FindObjectOfType<Wallet>();

        CheckGun();
        CheckWeapon();
    }

    public void ChooseGun(int id)
    {
        for (int i = 0; i < _guns.Length; i++)
        {
            if (_guns[i].GetComponent<Gun>().id == id)
            {
                if (_guns[i].GetComponent<Gun>().bought > 0)
                {
                    foreach (GameObject gun in _guns)
                    {
                        gun.GetComponent<Image>().color = new Color32(94, 94, 94, 255);
                    }
                    _playerData.SetIdPistol(id);
                    CheckGun();
                }
                else
                {
                    _id = _guns[i].GetComponent<Gun>().id;
                    _price = _guns[i].GetComponent<Gun>().price;
                    _type = 0;

                    BuyWindow.SetActive(true);
                    _titleDisplayText.text = "Want to buy a '" + _guns[i].GetComponent<Gun>().name + "' ?";
                    _btnDisplayText.text = "BUY $" + _guns[i].GetComponent<Gun>().price;
                }
            }
        }
    }

    public void ChooseWeapon(int id)
    {
        for (int i = 0; i < _weapons.Length; i++)
        {
            if (_weapons[i].GetComponent<Gun>().id == id)
            {
                if (_weapons[i].GetComponent<Gun>().bought > 0)
                {
                    foreach (GameObject weapon in _weapons)
                    {
                        weapon.GetComponent<Image>().color = new Color32(94, 94, 94, 255);
                    }

                    _playerData.SetIdWeapon(id);
                    CheckWeapon();
                }
                else
                {
                    _id = _weapons[i].GetComponent<Gun>().id;
                    _price = _weapons[i].GetComponent<Gun>().price;
                    _type = 1;

                    BuyWindow.SetActive(true);
                    _titleDisplayText.text = "Want to buy a '" + _weapons[i].GetComponent<Gun>().name + "' ?";
                    _btnDisplayText.text = "BUY $" + _weapons[i].GetComponent<Gun>().price;
                }
            }
        }

    }

    private void CheckGun()
    {
        for (int i = 0; i < _guns.Length; i++)
        {
            if (_guns[i].GetComponent<Gun>().id == _playerData.GetIdPistol())
                _guns[i].GetComponent<Image>().color = new Color32(131, 255, 30, 255);
        }
    }

    private void CheckWeapon()
    {
        for (int i = 0; i < _weapons.Length; i++)
        {
            if (_weapons[i].GetComponent<Gun>().id == _playerData.GetIdWeapon())
                _weapons[i].GetComponent<Image>().color = new Color32(131, 255, 30, 255);
        }
    }

    public void BuyGunWeaponButton()
    {
        if (_wallet.GetMoney() >= _price)
        {
            _wallet.SetMoney(-_price);

            if (_type > 0)
            {
                for (int i = 0; i < _weapons.Length; i++)
                {
                    if (_weapons[i].GetComponent<Gun>().id == _id)
                    {
                        _weapons[i].GetComponent<Gun>().bought = 1;
                        _weapons[i].GetComponent<Gun>().CheckWeap();
                        PlayerPrefs.SetInt("bought" + _weapons[i].GetComponent<Gun>().id, 1);
                        BuyWindow.SetActive(false);
                    }
                }
            }
            else
            {
                for (int i = 0; i < _guns.Length; i++)
                {
                    if (_guns[i].GetComponent<Gun>().id == _id)
                    {
                        _guns[i].GetComponent<Gun>().bought = 1;
                        _guns[i].GetComponent<Gun>().CheckWeap();
                        PlayerPrefs.SetInt("bought" + _guns[i].GetComponent<Gun>().id, 1);
                        BuyWindow.SetActive(false);
                    }
                }
            }
        }
    }

    public void CloseBuyGunWeaponWindow()
    {
        BuyWindow.SetActive(false);
    }
}
