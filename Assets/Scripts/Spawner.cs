using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject[] dangerPrefabs;
    public GameObject rescuePrefab;

    public float spawnDistanceFromCamera = 15f; // Kamera üstüne ne kadar uzaklýkta spawn edilsin
    public float minYSpacing = 1f; // Obje arasý min mesafe (y)
    public float maxYSpacing = 3f; // Obje arasý max mesafe (y)
    public float minX = -4f, maxX = 4f; // X ekseni sýnýrlarý

    private float nextSpawnY;
    private float currentDangerFrequency = 0.2f; // Baþlangýç sýklýðý (20%)

    private List<GameObject> spawnedObjects = new List<GameObject>();

    void Start()
    {
        nextSpawnY = mainCamera.transform.position.y + spawnDistanceFromCamera;
    }

    void Update()
    {
        float camY = mainCamera.transform.position.y;

        while (nextSpawnY < camY + spawnDistanceFromCamera)
        {
            UpdateDangerFrequency(nextSpawnY);

            // Zarar verici obje spawn etme kontrolü
            if (Random.value < currentDangerFrequency)
            {
                SpawnObject(dangerPrefabs[Random.Range(0, dangerPrefabs.Length)], nextSpawnY);
            }

            // Kurtarýlacak insan için %40 ihtimal
            if (Random.value < 0.4f)
            {
                Vector2 rescuePos;
                int tries = 0;
                do
                {
                    rescuePos = new Vector2(GetSpreadX(minX, maxX), nextSpawnY + Random.Range(-1f, 1f));
                    tries++;
                }
                while (IsNearDanger(rescuePos) && tries < 10);

                GameObject rescue = Instantiate(rescuePrefab, rescuePos, Quaternion.identity);
                spawnedObjects.Add(rescue);
            }

            // Bir sonraki spawn yüksekliði
            nextSpawnY += Random.Range(minYSpacing, maxYSpacing);
        }
        CleanupObjects();
    }

    void SpawnObject(GameObject prefab, float y)
    {
        Vector2 spawnPos;
        int tries = 0;

        do
        {
            float x = GetSpreadX(minX, maxX);
            spawnPos = new Vector2(x, y + Random.Range(-0.5f, 0.5f));
            tries++;
        }
        while (IsNearExisting(spawnPos) && tries < 10);

        GameObject obj = Instantiate(prefab, spawnPos, Quaternion.identity);
        spawnedObjects.Add(obj);
    }

    void UpdateDangerFrequency(float y)
    {
        if (y < 100f)
        {
            currentDangerFrequency = 0.2f; // %20
        }
        else if (y < 500f)
        {
            currentDangerFrequency = 0.4f; // %40
        }
        else
        {
            float increase = ((y - 500f) / 200f) * 0.05f;
            currentDangerFrequency = Mathf.Clamp(0.4f + increase, 0.4f, 0.9f); // Max %90
        }
    }
    void CleanupObjects()
    {
        float cleanupY = mainCamera.transform.position.y - 20f; // 20 birimden fazla aþaðýdaysa sil

        for (int i = spawnedObjects.Count - 1; i >= 0; i--)
        {
            GameObject obj = spawnedObjects[i];
            if (obj == null)
            {
                spawnedObjects.RemoveAt(i);
                continue;
            }

            if (obj.transform.position.y < cleanupY)
            {
                Destroy(obj);
                spawnedObjects.RemoveAt(i);
            }
        }
    }

    bool IsNearExisting(Vector2 pos)
    {
        foreach (GameObject obj in spawnedObjects)
        {
            if (obj == null) continue;
            if (Vector2.Distance(obj.transform.position, pos) < 1f)
                return true;
        }
        return false;
    }

    bool IsNearDanger(Vector2 pos)
    {
        foreach (GameObject obj in spawnedObjects)
        {
            if (obj == null) continue;
            if (obj.CompareTag("Danger") && Vector2.Distance(obj.transform.position, pos) < 2f)
                return true;
        }
        return false;
    }

    float GetSpreadX(float min, float max)
    {
        float mid = (min + max) / 2f;
        float value = Random.Range(min, max);

        // Orta noktadan uzaklaþtýrmak için ekstra rastgele kaydýrma
        if (value > mid)
            value += Random.Range(0.5f, 1.5f);
        else
            value -= Random.Range(0.5f, 1.5f);

        return Mathf.Clamp(value, min, max);
    }

}
