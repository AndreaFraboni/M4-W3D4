using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage = 10;
    [SerializeField] private float _lifeSpan = 5f;
    [SerializeField] private float _speed = 10f;

    private Rigidbody _rb;

    private Vector3 _movedir;

    private void Awake()
    {
        if (_rb == null) _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Destroy(gameObject, _lifeSpan);
    }

    private void FixedUpdate()
    {
        if (_movedir != Vector3.zero)
        {
            _rb.MovePosition(transform.position + _movedir * (_speed * Time.fixedDeltaTime));
        }
    }

    public void Shoot(Vector3 dir)
    {       
        if (dir.sqrMagnitude > 1f) dir.Normalize();

        _movedir = dir;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject.TryGetComponent<LifeController>(out LifeController _lifeController))
            {
                _lifeController.TakeDamage(_damage);
                Destroy(gameObject);
            }
        }
    }

}

