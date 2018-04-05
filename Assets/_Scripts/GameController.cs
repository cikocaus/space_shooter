using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GameController : MonoBehaviour 
{

    public GameObject hazard;
    public Vector3 spawnValues;
    public int hazardCountMax;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    IEnumerable <int> hazardCountRange;
    readonly int hazardCountMin = 0;

	void Awake()
	{
        hazardCountRange = Enumerable.Range(hazardCountMin, hazardCountMax);
	}

	void Start()
	{
        StartCoroutine (SpawnWaves());
	}

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            foreach (int count in hazardCountRange)
            {
                SpawnHazard();
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
    }

    void SpawnHazard()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
        Instantiate(hazard, spawnPosition, Quaternion.identity);
    }
}
