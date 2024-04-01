using UnityEngine;
//using GameAnalyticsSDK;

public class LevelManager : MonoBehaviour
{
    public Level[] LevelItem;
    private PlayerData _playerData;
    [HideInInspector]public int currentValue;
    private GameUI _gameUI;

    private void Start()
    {
        _playerData = FindObjectOfType<PlayerData>();
        _gameUI = FindObjectOfType<GameUI>();
        Instantiate(LevelItem[_playerData.GetLevel()].MapPrefab, Vector3.zero, Quaternion.identity);
        currentValue = 0;
       // GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Level", _playerData.GetLevel());
    }

    public void checkValue(string tag)
    {
        if (tag == LevelItem[_playerData.GetLevel()].TagObject)
        {
            if (currentValue < LevelItem[_playerData.GetLevel()].NeedValueObject)
                currentValue++;
            else
                currentValue = LevelItem[_playerData.GetLevel()].NeedValueObject;
        }

        _gameUI.CheckQuestUI();
    }
}
