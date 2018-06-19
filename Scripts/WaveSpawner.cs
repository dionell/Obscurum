using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    public GameObject[] enemies;
    public enum SpawnState { spawning, waiting, counting };

    [System.Serializable] //allows us to change the value in this class
    public class Wave
    {
        public string name; //name of the wave
        public Transform Insectoid;
        public int enemyCount;
        public float spawnRate;
        public int multiplier;
    }
    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    private float waveCountdown;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.counting;


    void Start()
    {
        waveCountdown = timeBetweenWaves;
    }

    void Update()
    {
        if(state == SpawnState.waiting)
        {
            //check if enemies are still alive
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if(waveCountdown <= 0)
        {
            if(state != SpawnState.spawning)
            {
                //start spawning wave
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        state = SpawnState.counting;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length -1)
        {
            nextWave = 0;

            //add stat multiplier here
        }
        else
        {
            nextWave++;
        }

    }

    public void statMultiplier(float amount)
    {
        
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if(searchCountdown <= 0)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave:" + _wave.name);

        state = SpawnState.spawning;
        //spawn
        for(int i = 0; i <_wave.enemyCount; i++)
        {
            SpawnEnemy(_wave.Insectoid);
            yield return new WaitForSeconds(2f / _wave.spawnRate);
        }
        state = SpawnState.waiting;
        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {

        if(spawnPoints.Length == 0)
        {
            Debug.Log("Error");
        }
        
        SpawnRandom();
    }

    public void SpawnRandom()
    { 
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemies[UnityEngine.Random.Range(0, enemies.Length - 1)], _sp.position, _sp.rotation);
    }

}
