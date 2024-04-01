using UnityEngine;

public class Wallet : MonoBehaviour
{
    private int _money;

    private void Awake()
    {
        _money = PlayerPrefs.GetInt("_money");
    }

    public int GetMoney()
    {
        return _money;
    }

    public void SetMoney(float value)
    {
        _money += (int)value;
        PlayerPrefs.SetInt("_money", _money);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            SetMoney(1000);
    }
}
