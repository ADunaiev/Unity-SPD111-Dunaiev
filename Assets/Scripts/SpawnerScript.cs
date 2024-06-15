using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject pipePrefab;

    private float spawnPeriod = 3f;   // кожні 3 секунди
    private float timeLeft;
    void Start()
    {
        timeLeft = 0f;
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            timeLeft = spawnPeriod;
            SpawnPipe();
        }
    }

    private void SpawnPipe()
    {
        var pipe = GameObject.Instantiate(pipePrefab);
        pipe.transform.position = this.transform.position + 
            Vector3.up * Random.Range(-2f, 2f);
    }
}
