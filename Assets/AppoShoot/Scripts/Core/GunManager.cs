using UnityEngine;

public class GunManager : MonoBehaviour
{
    private PlayerData _playerData;
    private int[] _weapons = new int [20];

    void Start()
    {
        for(int i=0; i<_weapons.Length; i++)
        {
            _weapons[i] = PlayerPrefs.GetInt("_weapons" + i);
        }
    }

    public void SetWeapon(int id)
    {
        _weapons[id] = 1;
        PlayerPrefs.SetInt("_weapons" + id, 1);
    }
}
