using UnityEngine;
using UnityEngine.Pool;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Transform _target;

    private int _initialSizePool = 5;
    private int _maxSizePool = 15;

    private ObjectPool<Enemy> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Enemy>(
            createFunc: () => Instantiate(_enemyPrefab),
            actionOnGet: (obj) => ActiveEnemy(obj),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: _initialSizePool,
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
        enemy.SetDirection(GenerateDirection());
        enemy.gameObject.SetActive(true);
    }

    private Vector3 GenerateDirection()
    {
        return new Vector3(0f, Random.Range(0, 360), 0f);
    }
}
