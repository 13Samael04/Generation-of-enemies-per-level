using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<SpawnPoint> _spawnPoints;

    private float _delay = 2f;

    private void Start()
    {
        StartCoroutine(nameof(SpawnEnemy), _delay);
    }

    private SpawnPoint GetSpawnPoint() => _spawnPoints[Random.Range(0, _spawnPoints.Count)];

    private IEnumerator SpawnEnemy(float delay)
    {
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (true)
        {
            GetSpawnPoint().SpawnEnemy();

            yield return wait;
        }
    }
}
