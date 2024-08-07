using System;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] float _speed;

    private Transform _target;

    public event Action<Enemy> Died;

    private void Update()
    {
        Move();
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<Target>(out Target target))
        {
            Died?.Invoke(this);
        }
    }

    private void Move()
    {
        transform.LookAt(_target);
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
    }
}
