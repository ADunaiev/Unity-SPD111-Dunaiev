using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject pipePrefab;
    [SerializeField]
    private GameObject bonusPrefab;

    private float spawnPeriod = 3f;   // кожні 3 секунди
    private float timeLeft;
    private float spawnBonusPeriod;
    private float bonusTimeLeft;

    void Start()
    {
        timeLeft = 0f;
        bonusTimeLeft = spawnPeriod * 1.2f;
        spawnBonusPeriod = spawnPeriod * 6.5f;
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        bonusTimeLeft -= Time.deltaTime;

        if (timeLeft < 0)
        {
            timeLeft = spawnPeriod;
            SpawnPipe();
        }

        if (bonusTimeLeft < spawnPeriod / 2)
        {
            bonusTimeLeft = spawnBonusPeriod;
            SpawnBonus();
        }
    }

    private void SpawnBonus()
    {
        var bonus = GameObject.Instantiate(bonusPrefab);
        bonus.transform.position = this.transform.position +
            Vector3.up * Random.Range(-2.5f, 2.5f);
    }

    private void SpawnPipe()
    {
        var pipe = GameObject.Instantiate(pipePrefab);
        pipe.transform.position = this.transform.position + 
            Vector3.up * Random.Range(-2f, 2f);
    }
}
