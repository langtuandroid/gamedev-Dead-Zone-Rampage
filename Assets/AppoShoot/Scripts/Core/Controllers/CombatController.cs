using UnityEngine;
using System.Collections;

public class CombatController : MonoBehaviour
{
    /// <summary>
    /// 0 - postol
    /// </summary>
    public int _typeOfWeapon;
    private int _cartridges;
    [SerializeField] private GameObject[] _weapons;
    [SerializeField] private ParticleSystem[] _shootParticle;
    [SerializeField] private GameObject[] _granateBullet;
    [SerializeField] private AudioClip[] _audioClips;
    private AudioSource _audioSource;
    public Animator _anim;
    private float timer;
    private GameUI _gameUI;
    private PlayerData _playerData;
    private GameObject _clone;
    private bool _reload;
    private int _reloadCounter;
    private float _reloadTImer;
    private Camera _camera;

    private void Start()
    {
        _cartridges = 10;
        _gameUI = FindObjectOfType<GameUI>();
        _playerData = FindObjectOfType<PlayerData>();
        _audioSource = GetComponent<AudioSource>();
        _camera = Camera.main;
        CheckDataWeapon();
        CheckWeapon();

        if (_typeOfWeapon == 6)
        {
            _anim.SetTrigger("granadeLaunch");
        }

        _reloadCounter = 0;
    }

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && !GameUI.isStop && !_reload)
        {

            if (timer <= 0)
            {

                switch (_typeOfWeapon)
                {
                    case 0:
                        _anim.SetTrigger("pistol");
                        //  timer = 1.2f;
                        ReloadCounter();
                        ParticleActiveShoot(1);
                        break;

                    case 1:
                        _anim.SetTrigger("pistol");
                        //  timer = 1.2f;
                        ReloadCounter();
                        ParticleActiveShoot(1);
                        break;

                    case 2:
                        _anim.SetTrigger("pistol");
                        //  timer = 1.2f;
                        ReloadCounter();
                        ParticleActiveShoot(2);
                        break;

                    case 3:
                        _anim.SetTrigger("1_ak");
                        CheckCartridgesUI();
                        ParticleActiveShoot(2);
                        //  timer = .4f;
                        break;

                    case 4:
                        _anim.SetTrigger("1_ak");
                        CheckCartridgesUI();
                        ParticleActiveShoot(3);
                        // timer = .2f;
                        break;

                    case 5:
                        _anim.SetTrigger("1_ak");
                        CheckCartridgesUI();
                        ParticleActiveShoot(4);
                        //timer = 1f;
                        break;

                    case 6:
                        _anim.SetTrigger("1_ak");
                        CheckCartridgesUI();
                        ParticleActiveShoot(6);
                        //timer = 1f;
                        break;

                    case 7:
                        _anim.SetTrigger("1_ak");
                        CheckCartridgesUI();
                        ParticleActiveShoot(7);
                        //timer = 1f;
                        break;

                        //case 6:
                        //    _anim.SetTrigger("gL_Shoot");
                        //    Instantiate(_granateBullet[0], transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
                        //    break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
            Instantiate(_granateBullet[0], transform.position + Vector3.forward * 3, Quaternion.identity);

        if (timer > 0)
            timer -= Time.deltaTime;

        if (_reload)
        {
            if (_reloadTImer > 0)
            {
                _reloadTImer -= Time.deltaTime;
            }
            else
            {
                _reload = false;
                _audioSource.clip = _audioClips[0];
            }
        }
    }

    private void ReloadCounter()
    {
        _reloadCounter++;

        if (_reloadCounter >= 10)
        {
            _reloadCounter = 0;
            _reload = true;
            _anim.SetTrigger("reload");
            _reloadTImer = 1.8f;
            _audioSource.clip = _audioClips[1];
            _audioSource.Play();
        }
    }

    private void ParticleActiveShoot(int _damage)
    {
        _shootParticle[_typeOfWeapon].Play();
        _shootParticle[_typeOfWeapon].GetComponent<AudioSource>().Play();
        RayHit(_damage);
    }
    private void CheckCartridgesUI()
    {
        _cartridges--;
        _gameUI.UpdateCartridgesProgress(_cartridges);

        if (_cartridges <= 0)
        {
            _typeOfWeapon = _playerData.GetIdPistol();
            _anim.SetTrigger("change");
            StartCoroutine(ChangeWeapon());

            if (!PlayerPrefs.HasKey("firstStart"))
            {
                _playerData.SetIdWeapon(0);
                PlayerPrefs.SetInt("firstStart", 1);
            }
        }
    }

    private void CheckDataWeapon()
    {
        if (_playerData.GetIdWeapon() > 0)
            _typeOfWeapon = _playerData.GetIdWeapon();
        else
            _typeOfWeapon = _playerData.GetIdPistol();
    }

    private void CheckWeapon()
    {
        foreach (GameObject _item in _weapons)
            _item.SetActive(false);

        _weapons[_typeOfWeapon].SetActive(true);
    }

    public void CartridgesFull()
    {
        _cartridges = 10;
        CheckDataWeapon();
        _anim.SetTrigger("change");
        _gameUI.UpdateCartridgesProgress(_cartridges);
        StartCoroutine(ChangeWeapon());
    }

    IEnumerator ChangeWeapon()
    {
        yield return new WaitForSeconds(.3f);
        CheckWeapon();
    }

    private void RayHit(int damage)
    {
        Vector3 rayOrigin = _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

        RaycastHit hit;


        if (Physics.Raycast(rayOrigin, _camera.transform.forward, out hit, 100))
        {
            if (hit.collider.GetComponent<Zombie>())
                hit.collider.GetComponent<Zombie>().TakeDamage(damage, _typeOfWeapon);

            if (hit.collider.GetComponent<ZombieBossInner>())
                hit.collider.GetComponent<ZombieBossInner>().TakeDamage(damage);
        }


            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1.6f, transform.position.z), transform.TransformDirection(Vector3.right), out hit, 100))
        {
            //Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 1.6f, transform.position.z), transform.TransformDirection(Vector3.right));

           
        }
    }
}
