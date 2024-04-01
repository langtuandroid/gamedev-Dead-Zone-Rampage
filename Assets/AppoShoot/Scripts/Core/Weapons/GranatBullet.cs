using UnityEngine;

public class GranatBullet : MonoBehaviour
{
    public GameObject Explosion;
    private GameObject _clone;
    private Rigidbody _rigidbody;
    private bool _isexplosion;
     void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.AddForce(new Vector3(12, 2.5f, 0), ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isexplosion)
        {
            Destroy(gameObject);
            _clone = Instantiate(Explosion, transform.position, Quaternion.identity);
            Destroy(_clone, 2);
            _isexplosion = true;
        }
    
    }
}
