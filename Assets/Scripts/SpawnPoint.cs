using UnityEngine;
using UnityEngine.Pool;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Transform _target;

    private int _InitialSizePool = 5;
    private int _maxSizePool = 15;

    private ObjectPool<Enemy> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Enemy>(
            createFunc: () => Instantiate(_enemy),
            actionOnGet: (obj) => ActiveEnemy(obj),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: _InitialSizePool,
            maxSize: _maxSizePool);
    }

    public void SpawnEnemy()
    {
        _pool.Get();
    }

    private void Release(Enemy enemy)
    {
        enemy.Died -= Release;
        _pool.Release(enemy);
    }

    private void ActiveEnemy(Enemy enemy)
    {
        enemy.Died += Release;

        enemy.transform.position = transform.position;
        enemy.SetTarget(_target);
        enemy.gameObject.SetActive(true);
    }
}
