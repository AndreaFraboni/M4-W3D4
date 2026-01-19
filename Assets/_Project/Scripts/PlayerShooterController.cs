using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooterController : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private float _fireInterval = 0.5f;
    [SerializeField] private float _offset = 1.5f;

    private float _lastShootTime;

    public void Shoot(Vector3 direction)
    {
        _lastShootTime = Time.time;

        Bullet clonedBullet = Instantiate(_bulletPrefab);
        clonedBullet.transform.position = transform.position + new Vector3(direction.x, direction.y, 0) * _offset;
        clonedBullet.Shoot(direction);
    }

    public bool CanShootNow()
    {
        return Time.time - _lastShootTime > _fireInterval;
    }

    public void TryToShoot(Vector3 direction)
    {
        if (CanShootNow())
        {
            Shoot(direction);
        }
    }

}
