using UnityEngine;
using UnityEngine.Pool;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;

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
        enemy.RotateToDirection();
        enemy.gameObject.SetActive(true);
    }

    private Vector3 GenerateDirection()
    {
        float angle = Random.Range(0f, Mathf.PI * 2);

        float x = Mathf.Cos(angle);
        float z = Mathf.Sin(angle);

        return new Vector3(x, 0, z);
    }
    }
