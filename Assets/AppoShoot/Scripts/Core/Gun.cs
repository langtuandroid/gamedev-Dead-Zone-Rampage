using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gun : MonoBehaviour
{
    public int id;
    public string name;
    public int price;
    [SerializeField] private Image _avatarImage;
    [SerializeField] private GameObject _btnBuy;
    [SerializeField] private TextMeshProUGUI _priceDisplayText;
    [HideInInspector] public int bought;

    private void Awake()
    {
        if(id == 0)
        {
            bought = 1;
            PlayerPrefs.SetInt("bought" + id, 1);
        }

        bought = PlayerPrefs.GetInt("bought" + id);
        CheckWeap();

        _priceDisplayText.text = "$" + price;
    }

    public void CheckWeap()
    {
        if (bought > 0)
        {
            _avatarImage.color = new Color(1, 1, 1, 1);
            _btnBuy.SetActive(false);
        }
    }
}
