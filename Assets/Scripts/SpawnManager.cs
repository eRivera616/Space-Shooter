using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] _powerups;
    private bool _stopSpawning = false;
    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }
    public void StartSpawning()
    {
        StartCoroutine(spawnEnemyRoutine());
        StartCoroutine(spawnPowerupRoutine());
    }
    IEnumerator spawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3f);
        while(_stopSpawning == false )
        {
            Vector3 posToSpawnEnemy = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawnEnemy, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5);
        }
    }
    IEnumerator spawnPowerupRoutine()
    {
        while(_stopSpawning == false)
        {
            Vector3 posToSpawnPowerup = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int randomPowerup = Random.Range(0, 3);
            Instantiate(_powerups[randomPowerup], posToSpawnPowerup, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 10));
        }
    }
    public void onPlayerDeath()
    {
        _stopSpawning = true;
    }

}
