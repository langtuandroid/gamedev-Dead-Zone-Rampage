using UnityEngine;

public class Controller : MonoBehaviour
{
    public float VariableSpeed = 1;
    [SerializeField] private float _speed;
    private Rigidbody _rigidbody;
    private float _delay;
    private Player _player;
    private int direction;

    private void Awake()
    {
        VariableSpeed = 1;
    }

    private void Start()
    {
        _delay = 1.5f;
        _rigidbody = GetComponent<Rigidbody>();
        _player = GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");

        switch (direction)
        {
            case 0:
                transform.rotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0f, 0), Time.deltaTime * 8);
                break;

            case 1:
                transform.rotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, -90f, 0), Time.deltaTime * 8);
                break;

            case 2:
                transform.rotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 90f, 0), Time.deltaTime * 8);
                break;
        }


        switch (direction)
        {
            case 0:
                if (_delay <= 0)
                {
                    _rigidbody.velocity = new Vector3((_speed + 1) * VariableSpeed, -1f, 0);

                    if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
                    {
                        Touch touch = Input.touches[0];
                        h = touch.deltaPosition.x;

                        _rigidbody.velocity = new Vector3((_speed + 1) * VariableSpeed, -1f, -h / 2);
                    }
                }
                else
                {
                    _delay -= Time.deltaTime;
                }



                break;

            case 1:
                if (_delay <= 0)
                {
                    _rigidbody.velocity = new Vector3(0, -1f, (_speed + 1) * VariableSpeed);

                    if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
                    {
                        Touch touch = Input.touches[0];
                        h = touch.deltaPosition.x;

                        _rigidbody.velocity = new Vector3(h / 2, -1f, (_speed + 1) * VariableSpeed);
                    }
                }
                else
                {
                    _delay -= Time.deltaTime;
                }

                break;

            case 2:

                if (_delay <= 0)
                {
                    _rigidbody.velocity = new Vector3(0, -1f, -(_speed + 1) * VariableSpeed);

                    if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
                    {
                        Touch touch = Input.touches[0];
                        h = touch.deltaPosition.x;

                        _rigidbody.velocity = new Vector3(-h / 2, -1f, -(_speed + 1) * VariableSpeed);
                    }
                }

                else
                {
                    _delay -= Time.deltaTime;
                }



                break;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ChangeDirection>())
        {
            direction = other.GetComponent<ChangeDirection>().direction;
        }
    }
}
