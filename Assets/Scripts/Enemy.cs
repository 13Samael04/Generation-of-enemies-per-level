using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Transform _target;

    public event Action<Enemy> Died;

    private void Update()
    {
        Move();
    }

    public void SetDirection(Vector3 direction)
    {
        transform.Rotate(direction);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Platform>(out Platform platform))
        {
            Died?.Invoke(this);
        }
    }

    private void Move()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }
}
