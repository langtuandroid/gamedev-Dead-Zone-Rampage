using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class MenuMapUI : MonoBehaviour
{
    [SerializeField] private Image[] _cityIcons;
    [SerializeField] private Image[] _levelItems;
    [SerializeField] private Sprite[] _citySprites;
    [SerializeField] private string[] _cityName;
    [SerializeField] private TextMeshProUGUI _cityDisplayText;
    private Animator[] _levelItemsAnimator = new Animator[9];
    private int _cityLevel;
    public static int _currentLevelItem;

    private void Awake()
    {
        _cityLevel = PlayerPrefs.GetInt("_cityLevel");
        _currentLevelItem = PlayerPrefs.GetInt("_currentLevelItem");
    }

    void Start()
    {
        for (int i = 0; i < _levelItems.Length; i++)
            _levelItemsAnimator[i] = _levelItems[i].GetComponent<Animator>();

        CheckLevelItems();
    }

    private void CheckLevelItems()
    {
        if(_currentLevelItem > 8)
        {
            _currentLevelItem = 0;
            _cityLevel++;
            PlayerPrefs.SetInt("_cityLevel", _cityLevel);       
        }

        _cityIcons[0].sprite = _citySprites[_cityLevel];
        _cityIcons[1].sprite = _citySprites[(_cityLevel + 1)];

        for (int i = 0; i < _currentLevelItem; i++)
            _levelItemsAnimator[i].SetTrigger("done");

        _levelItemsAnimator[_currentLevelItem].SetTrigger("current");

        _cityDisplayText.text = _cityName[_cityLevel];
    }
}
