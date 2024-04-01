using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private static PlayerData _instance;

    public static PlayerData Instance { get { return _instance; } }

    private int _level;
    private int _idPistol;
    private int _idWeapon;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        _level = PlayerPrefs.GetInt("_level");
        DontDestroyOnLoad(gameObject);

        _idPistol = PlayerPrefs.GetInt("_idPistol");
        _idWeapon = PlayerPrefs.GetInt("_idWeapon");

        if (!PlayerPrefs.HasKey("firstStart"))
        {
            _idWeapon = 5;         
        }
    }

    public int GetLevel()
    {
        return _level;
    }

    public int GetIdPistol()
    {
        return _idPistol;
    }

    public int GetIdWeapon()
    {
        return _idWeapon;
    }

    public void SetLevel()
    {
        _level++;
        PlayerPrefs.SetInt("_level", _level);
    }

    public void SetIdPistol(int id)
    {
        _idPistol = id;
        PlayerPrefs.SetInt("_idPistol", _idPistol);
    }

    public void SetIdWeapon(int id)
    {
        _idWeapon = id;
        PlayerPrefs.SetInt("_idWeapon", _idWeapon);
    }
}
