using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _speed = 5f;
    [SerializeField] float _smooth = 10f;
    [SerializeField] private float _rotationSpeed = 5f;

    private Rigidbody _rb;
    private float horizontal, vertical = 0f;

    private PlayerShooterController _shooter;
    private Camera _cam;

    private Vector3 move = Vector3.zero;

    private void Awake()
    {
        if (_rb == null) _rb = GetComponent<Rigidbody>();

        _shooter = GetComponent<PlayerShooterController>();
        _cam = Camera.main;
    }

    void Update()
    {
        CheckInput();
        CheckFire();
    }

    private void FixedUpdate()
    {
        Move();
        Rotation();
    }

    private void CheckInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        Vector3 inputMove = new Vector3(horizontal, 0, vertical);

        if (inputMove.sqrMagnitude > 1f) inputMove.Normalize();

        move = Vector3.Lerp(move, inputMove, _smooth * Time.deltaTime);

        if (move.sqrMagnitude < 0.0001f) move = Vector3.zero;
    }

    //private void Move()
    //{
    //    _rb.MovePosition(transform.position + move * (_speed * Time.fixedDeltaTime));
    //}
    private void Move()
    {
        Vector3 current = _rb.velocity;
        Vector3 desired = move * _speed;
        _rb.velocity = new Vector3(desired.x, current.y, desired.z);
    }

    private void Rotation()
    {
        if (move != Vector3.zero)
        {
            Quaternion _rotation = Quaternion.LookRotation(move, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, _rotation, Time.deltaTime * _rotationSpeed);
        }
    }

    private void CheckFire()
    {
        if (_shooter != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mouseScreenPosition = Input.mousePosition;
                mouseScreenPosition.z = -_cam.transform.position.z; // distanza tra camera e piano XY
                Vector3 mouseWorldPosition = _cam.ScreenToWorldPoint(mouseScreenPosition);
                Vector3 shootDirection = mouseWorldPosition - transform.position;

                if (shootDirection != Vector3.zero) shootDirection.Normalize();

                _shooter.TryToShoot(shootDirection);
            }
        }
    }

}

