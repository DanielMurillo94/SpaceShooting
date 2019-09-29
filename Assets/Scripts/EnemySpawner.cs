using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;


    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        } while (looping);
    }

    private IEnumerator SpawnAllWaves()
    {
        int waveConfigsLength = waveConfigs.Count;
        for (int wavesCounter = startingWave; wavesCounter < waveConfigsLength; wavesCounter++)
        {
            yield return StartCoroutine(SpawnAllEnemiesInWave(waveConfigs[wavesCounter]));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig wave)
    {
        int numberOfEnemies = wave.GetNumberOfenemies();
        for (int index = 0; index < numberOfEnemies; index++)
        {
            var newEnemy = Instantiate(wave.GetEnemyPrefab(), wave.GetWayPoints()[0].transform.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().setWaveConfig(wave);
            yield return new WaitForSeconds(wave.GetTimeBetweenSpawns());
        }
    }
}
