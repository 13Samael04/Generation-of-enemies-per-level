using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Vector3 _direction;

    public event Action<Enemy> Died;

    private void Update()
    {
        Move();
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Platform platform))
        {
            Died?.Invoke(this);
        }
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }

    private void Move()
    {
        transform.Translate(_direction * _speed * Time.deltaTime, Space.World);
    }

    public void RotateToDirection()
    {
        transform.rotation = Quaternion.LookRotation(_direction);
    }
}
