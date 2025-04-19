using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public List<GameObject> waveList = new List<GameObject>();
    public float spawnY = 10f;

    private int waveIndex = 0;
    private float nextSpawnTime;

    // S�kl�k aral�klar�
    private float mediumMin = 8f, mediumMax = 10f;
    private float highMin = 3f, highMax = 5f;

    public List<GameObject> spawnedWaves = new List<GameObject>();

    void Update()
    {
        
        float cameraY = Camera.main.transform.position.y;
        spawnY = cameraY + 10;

        waveIndex= Random.Range(0, waveList.Count); 

        // Kamera 100'e ula�madan spawn ba�lama
        if (cameraY < 100f)
            return;

        // �u anki zaman, bir sonraki spawn zaman�n� ge�tiyse
        if (Time.time >= nextSpawnTime)
        {
            SpawnWave(waveIndex);
            waveIndex++;

            // Spawn zaman�n� ayarla
            if (cameraY >= 500f)
                nextSpawnTime = Time.time + Random.Range(highMin, highMax);
            else
                nextSpawnTime = Time.time + Random.Range(mediumMin, mediumMax);
        }
        CleanupWaves();
    }

    void SpawnWave(int index)
    {
        GameObject wavePrefab = waveList[index];

        float spawnX = 0f;

        if (index == 0)
        {
            spawnX = -2.3f;
        }
        else if (index == 1)
        {
            spawnX = 2.3f;
        }
        else if (index == 2)
        {
            spawnX = Random.Range(-4f, 4f);
        }
        else if (index == 3)
        {
            spawnX = Random.Range(-6f, 5f);
        }

        Vector3 spawnPos = new Vector3(spawnX, spawnY, 0);
        GameObject spawnedObject = Instantiate(wavePrefab, spawnPos, Quaternion.identity);
        spawnedObject.AddComponent<Wave>();
        spawnedWaves.Add(spawnedObject);
    }

    void CleanupWaves()
    {
        float cleanupY = Camera.main.transform.position.y - 40f; // 40 birimden fazla a�a��daysa sil

        for (int i = spawnedWaves.Count - 1; i >= 0; i--)
        {
            GameObject obj = spawnedWaves[i];
            if (obj == null)
            {
                spawnedWaves.RemoveAt(i);
                continue;
            }

            if (obj.transform.position.y < cleanupY)
            {
                Destroy(obj);
                spawnedWaves.RemoveAt(i);
            }
        }
    }
}
